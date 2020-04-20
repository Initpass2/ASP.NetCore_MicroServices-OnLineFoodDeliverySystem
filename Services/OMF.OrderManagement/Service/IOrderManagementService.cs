using Newtonsoft.Json;
using OMF.Common.Events.Delivery;
using OMF.Common.Events.Order;
using OMF.Common.Events.Restaurant;
using OMF.OrderManagement.Domain.Repositories;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMF.OrderManagement.Service
{
    public interface IOrderManagementService
    {
        Task ProcessCreateOrderRequest(OrderManagementDataModel orderManagementInputModel);
        Task ProcessUpdateOrderRequest(OrderManagementDataModel orderManagementInputModel);
        Task<OrderManagementDataModel> ProcessGetOrderDetails(Guid orderId);
        Task ProcessCancelOrderRequest(Guid orderId);
    }

    public class OrderManagementService : IOrderManagementService
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        private readonly IPaymentService _paymentService;
        private readonly OMFOrderDbContext _dbContext;
        private readonly IBusClient _client;

        public OrderManagementService(ICustomerService customerService, IOrderService orderService,
            IPaymentService paymentService, OMFOrderDbContext oMFOrderDbContext, IBusClient client)
        {
            _customerService = customerService;
            _orderService = orderService;
            _paymentService = paymentService;
            _dbContext = oMFOrderDbContext;
            _client = client;
        }

        public async Task<OrderManagementDataModel> ProcessGetOrderDetails(Guid orderId)
        {
            var orderMangement = new OrderManagementDataModel();
            var order = await _orderService.GetOrderAsync(orderId);
            orderMangement.OrderDetils = new Orderdetils()
            {
                OrderDate = order.OrderDate,
                OrderId = order.OrderId,
                OrderItem = JsonConvert.DeserializeObject<Orderitem[]>(order.Orderitem),
                OrderStatus = Enum.Parse<OrderStatus>(order.Orderstatus),
                RestaurantId = order.RestaurentId,
                TotalPrice = order.TotalPrice
            };

            var paymentDetails = await _paymentService.FindByIdAsync(order.PaymentId);

            orderMangement.PaymentDetails = new Paymentdetails()
            {
                Alis = paymentDetails.Alis,
                CardHolderName = paymentDetails.CardHolderName,
                CardNumber = paymentDetails.CardNumber,
                CardType = Enum.Parse<CardType>(paymentDetails.Cardtype),
                Expiration = paymentDetails.Expiration,
                PaymentId = paymentDetails.Id
            };

            var customerDetails = await _customerService.FindByCustomerIdAsync(paymentDetails.CustomerId);

            orderMangement.CustomerDetails = new Customerdetails()
            {
                CustomerId = customerDetails.CustomerId,
                Email = customerDetails.Email,
                Name = customerDetails.Name,
                PhoneNumber = customerDetails.PhoneNumber,
                ShippingAddress = JsonConvert.DeserializeObject<Shippingaddress>(customerDetails.Shippingaddress),
                UserId = customerDetails.UserId
            };

            return orderMangement;
        }

        public async Task ProcessCancelOrderRequest(Guid orderId)
        {
            try
            {
                var menuItemStocks = new List<MenuItemStock>();
                var order = await _orderService.GetOrderAsync(orderId);
                var orderItems = JsonConvert.DeserializeObject<Orderitem[]>(order.Orderitem);
                foreach (var item in orderItems)
                {
                    menuItemStocks.Add(new MenuItemStock() { MenuId = item.MenuId, OldItemCount = item.Unit });
                }
                await _orderService.DeleteOrderAsync(orderId);
                await _client.PublishAsync(new OrderDeleted(order.RestaurentId, menuItemStocks));
                // notify to apigateway

                await _client.PublishAsync(new OrderCanceled(orderId));
            }
            catch
            {

                throw;
            }
        }

        public async Task ProcessCreateOrderRequest(OrderManagementDataModel orderManagementInputModel)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    await _customerService.AddCustomer(orderManagementInputModel.CustomerDetails);
                    var customer = await _customerService.FindByAddressAsync(JsonConvert.SerializeObject(orderManagementInputModel.CustomerDetails.ShippingAddress),
                        orderManagementInputModel.CustomerDetails.UserId);

                    await _paymentService.CreatePaymentAsync(orderManagementInputModel.PaymentDetails, customer.CustomerId);
                    var paymentInfo = await _paymentService.FindByCardNumberAsync(orderManagementInputModel.PaymentDetails.CardNumber);

                    orderManagementInputModel.OrderDetils.OrderId = Guid.NewGuid();
                    await _orderService.CreateOrderAsync(orderManagementInputModel.OrderDetils, paymentInfo.Id);

                    transaction.Commit();

                    // suvankar : we can create a asynchous method only for create order info and publish through rabbitmq

                    var menuItemStocks = new List<MenuItemStock>();
                    foreach (var item in orderManagementInputModel.OrderDetils.OrderItem)
                    {
                        menuItemStocks.Add(new MenuItemStock() { MenuId = item.MenuId, NewItemCount = item.Unit });
                    }

                    await _client.PublishAsync(new OrderCreated(orderManagementInputModel.OrderDetils.RestaurantId, menuItemStocks));

                    // suvankar : send to delivery team . Based on order staus we can send to deliverry team 
                    // For now just sending to delevery team

                    var orderDetails = orderManagementInputModel.OrderDetils;

                    // notify to delevery to team
                    await _client.PublishAsync(new NofifiedOrderUpdated(orderDetails.OrderId.Value, JsonConvert.SerializeObject(orderDetails.OrderItem),
                        orderDetails.TotalPrice, orderDetails.RestaurantId, orderDetails.OrderStatus.ToString(),
                       JsonConvert.SerializeObject(orderManagementInputModel.CustomerDetails.ShippingAddress), 
                       orderManagementInputModel.CustomerDetails.Name, orderManagementInputModel.CustomerDetails.PhoneNumber,
                       orderManagementInputModel.CustomerDetails.UserId, orderManagementInputModel.CustomerDetails.Email));

                    // notify to apigateway

                    await _client.PublishAsync(new OrderAdded(orderDetails.OrderId.Value, JsonConvert.SerializeObject(orderDetails.OrderItem),
                        orderDetails.TotalPrice, orderDetails.RestaurantId, orderDetails.OrderStatus.ToString(),
                       JsonConvert.SerializeObject(orderManagementInputModel.CustomerDetails.ShippingAddress),
                       orderManagementInputModel.CustomerDetails.Name, orderManagementInputModel.CustomerDetails.PhoneNumber,
                       orderManagementInputModel.CustomerDetails.UserId, orderManagementInputModel.CustomerDetails.Email));

                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task ProcessUpdateOrderRequest(OrderManagementDataModel orderManagementInputModel)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var currentOrderDetails = _orderService.GetOrderAsync(orderManagementInputModel.OrderDetils.OrderId.Value);

                    await _customerService.UpdateCustomer(orderManagementInputModel.CustomerDetails);
                    var customer = await _customerService.FindByAddressAsync(JsonConvert.SerializeObject(orderManagementInputModel.CustomerDetails.ShippingAddress),
                        orderManagementInputModel.CustomerDetails.UserId);

                    var paymentInfo = await _paymentService.FindByCardNumberAsync(orderManagementInputModel.PaymentDetails.CardNumber);

                    await _orderService.UpdateOrderAsync(orderManagementInputModel.OrderDetils, paymentInfo.Id);

                    transaction.Commit();

                    var menuItemStocks = new List<MenuItemStock>();
                    foreach (var newOrder in orderManagementInputModel.OrderDetils.OrderItem)
                    {
                        foreach (var oldOrder in orderManagementInputModel.OrderDetils.OrderItem)
                        {
                            if (newOrder.MenuId == oldOrder.MenuId)
                            {
                                menuItemStocks.Add(new MenuItemStock() { MenuId = newOrder.MenuId, NewItemCount = newOrder.Unit, OldItemCount = oldOrder.Unit });
                                break;
                            }
                        }
                    }

                    // Update stock
                    await _client.PublishAsync(new OrderUpdated(orderManagementInputModel.OrderDetils.RestaurantId, menuItemStocks));

                    // suvankar : send to delivery team . Based on order staus we can send to deliverry team 
                    // For now just sending to delevery team

                    var orderDetails = orderManagementInputModel.OrderDetils;

                    // notify to delevery to team
                    await _client.PublishAsync(new NofifiedOrderUpdated(orderDetails.OrderId.Value, JsonConvert.SerializeObject(orderDetails.OrderItem),
                        orderDetails.TotalPrice, orderDetails.RestaurantId, orderDetails.OrderStatus.ToString(),
                       JsonConvert.SerializeObject(orderManagementInputModel.CustomerDetails.ShippingAddress),
                       orderManagementInputModel.CustomerDetails.Name, orderManagementInputModel.CustomerDetails.PhoneNumber,
                       orderManagementInputModel.CustomerDetails.UserId, orderManagementInputModel.CustomerDetails.Email));

                    // notify to apigateway

                    await _client.PublishAsync(new OrderModified(orderDetails.OrderId.Value, JsonConvert.SerializeObject(orderDetails.OrderItem),
                        orderDetails.TotalPrice, orderDetails.RestaurantId, orderDetails.OrderStatus.ToString(),
                       JsonConvert.SerializeObject(orderManagementInputModel.CustomerDetails.ShippingAddress),
                       orderManagementInputModel.CustomerDetails.Name, orderManagementInputModel.CustomerDetails.PhoneNumber,
                       orderManagementInputModel.CustomerDetails.UserId, orderManagementInputModel.CustomerDetails.Email));

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
