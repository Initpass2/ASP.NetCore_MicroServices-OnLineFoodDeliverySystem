using Microsoft.AspNetCore.Mvc;
using OMF.Api.RequestModel.Order;
using System;
using System.Threading.Tasks;

namespace OMF.Api.Services
{
    public interface IOrderManagementApiClient
    {
        Task<IActionResult> CreateOrder(OrderManagemenDataModel order);
        Task<IActionResult> UpdateOrder(OrderManagemenDataModel order);
        Task<IActionResult> DeleteOrder(Guid orderId);
        Task<IActionResult> GetOrder(Guid orderId);
    }
}
