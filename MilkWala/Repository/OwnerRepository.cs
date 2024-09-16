using Microsoft.EntityFrameworkCore;
using MilkWala.IRepository;
using MilkWala.Models;
using MilkWala.RequestModels;

namespace MilkWala.Repository
{
    public class OwnerRepository : IOwnerRespository
    {
        private readonly MWDBContext _context;

        public OwnerRepository(MWDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MWOwner>> GetOwnersAsync()
        {
            try
            {
                var allOwners = await _context.MWOwners.Where(x => x.IsActive).ToListAsync();
                return allOwners;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddOrUpdateOwner(OwnerReqViewModel owners)
        {
            try
            {
                if (owners.Id != null)
                {
                    MWOwner? existingOwners = _context.MWOwners.Find(owners.Id);
                    if (existingOwners != null)
                    {
                        existingOwners.Name = owners.Name;
                        existingOwners.Email = owners.Email;
                        existingOwners.Phone = owners.Phone;
                        _context.MWOwners.Update(existingOwners);
                        await _context.SaveChangesAsync();
                        return true;
                    };
                    return false;
                }
                else
                {
                    var newOwners = new MWOwner
                    {
                        Id = Guid.NewGuid(),
                        Name = owners.Name,
                        Email = owners.Email,
                        Phone = owners.Phone,
                        IsActive = true
                    };

                    await _context.MWOwners.AddAsync(newOwners);
                    await _context.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteOwner(Guid Id)
        {
            try
            {
                var owner = _context.MWOwners.Find(Id);
                if (owner != null)
                {
                    owner.IsActive = false;
                    _context.MWOwners.Update(owner);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<MWOwner> GetOwnerByPhoneAsync(string phone)
        {
            return await _context.MWOwners.SingleOrDefaultAsync(o => o.Phone == phone);
        }

        public async Task UpdateOwnerAsync(MWOwner owner)
        {
            _context.MWOwners.Update(owner);
            await _context.SaveChangesAsync();
        }
    }
}
