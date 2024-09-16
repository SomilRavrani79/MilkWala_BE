using MilkWala.Models;
using MilkWala.RequestModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilkWala.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<MwCustomer>> GetCustomersAsync();
        Task<bool> AddOrUpdateCustomer(CustomerReqViewModel customer);
        Task<bool> DeleteCustomer(Guid id);
    }
}
