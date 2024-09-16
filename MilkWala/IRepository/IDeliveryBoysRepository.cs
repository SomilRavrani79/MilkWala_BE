using MilkWala.Models;
using MilkWala.RequestModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilkWala.Repositories
{
    public interface IDeliveryBoysRepository
    {
        Task<IEnumerable<MWDeliveryBoy>> GetDeliveryBoysAsync();
        Task<bool> AddOrUpdateDeliveryBoy(DeliveryBoysReqViewModel dBoys);
        Task<bool> DeleteDeliveryBoy(Guid id);
    }
}
