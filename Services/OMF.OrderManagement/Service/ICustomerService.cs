using OMF.OrderManagement.Domain;
using System.Threading.Tasks;

namespace OMF.OrderManagement.Service
{
    public interface ICustomerService
    {
        Task AddCustomer(Customerdetails customer);
        Task UpdateCustomer(Customerdetails customer);
        Task<Customer> FindByAddressAsync(string shippingaddress, string userId);
        Task<Customer> FindByCustomerIdAsync(int customerId);
    }
}
