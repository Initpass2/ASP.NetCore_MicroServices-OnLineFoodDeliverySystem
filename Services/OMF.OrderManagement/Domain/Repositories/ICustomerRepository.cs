using System.Collections.Generic;
using System.Threading.Tasks;

namespace OMF.OrderManagement.Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task Add(Customer customer);
        Task Update(Customer customer);
        Task<IEnumerable<Customer>> FindByUserIdAsync(string customerId);
        Task<Customer> FindByCustomerIdAsync(int Id);
    }
}
