using MilkWala.Models;
using MilkWala.RequestModels;

namespace MilkWala.IRepository
{
    public interface IOwnerRespository
    {
        Task<IEnumerable<MWOwner>> GetOwnersAsync();
        Task<bool> AddOrUpdateOwner(OwnerReqViewModel dBoys);
        Task<bool> DeleteOwner(Guid id);

        Task<MWOwner> GetOwnerByPhoneAsync(string phone);
        Task UpdateOwnerAsync(MWOwner owner);
    }
}
