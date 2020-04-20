using Newtonsoft.Json;
using OMF.Common.Exception;
using OMF.OrderManagement.Domain;
using OMF.OrderManagement.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OMF.OrderManagement.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task AddCustomer(Customerdetails customer)
        {
            var customerobj = await _repository.FindByUserIdAsync(customer.UserId);
            string shippingAddr = JsonConvert.SerializeObject(customer.ShippingAddress);
            if (customerobj.Count() > 0)
            {
                var customerdetails = customerobj.Where(c => c.Shippingaddress.Equals(shippingAddr)).Select(c => c).SingleOrDefault();
                // suvankar : 9/22/2019
                // we can check for ph or emil if modified and absed on that we can add a new record into DB
                if (customerdetails != null) return;
            }

            await _repository.Add(new Customer()
            {
                CustomerId = customer.CustomerId,
                UserId = customer.UserId,
                Email = customer.Email,
                Name = customer.Name,
                PhoneNumber = customer.PhoneNumber,
                Shippingaddress = JsonConvert.SerializeObject(customer.ShippingAddress)
            });
        }

        public async Task UpdateCustomer(Customerdetails customer)
        {
            try
            {
                var customerObj = await _repository.FindByCustomerIdAsync(customer.CustomerId);
                if (customerObj == null)
                    throw new OMFException("No customer found", $"No customer found for billing");
                customerObj.Email = customer.Email;
                customerObj.Name = customer.Name;
                customerObj.PhoneNumber = customer.PhoneNumber;
                customerObj.Shippingaddress = JsonConvert.SerializeObject(customer.ShippingAddress);

                await _repository.Update(customerObj);
            }
            catch
            {
                throw;
            }

        }

        public async Task<Customer> FindByAddressAsync(string shippingaddress, string userId)
        {
            try
            {
                var customerobj = await _repository.FindByUserIdAsync(userId);
                return customerobj.Where(c => c.Shippingaddress.Equals(shippingaddress)).Select(c => c).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        public async Task<Customer> FindByCustomerIdAsync(int customerId)
        {
            return await _repository.FindByCustomerIdAsync(customerId);           
        }
    }
}
