using Microsoft.EntityFrameworkCore;
using MilkWala.Models;
using MilkWala.Repositories;
using MilkWala.RequestModels;

namespace MilkWala.Repository
{
    public class DeliveryBoyRepository : IDeliveryBoysRepository
    {
        private readonly MWDBContext _context;

        public DeliveryBoyRepository(MWDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MWDeliveryBoy>> GetDeliveryBoysAsync()
        {
            try
            {
                var alldeliveryBoys = await _context.MWDeliveryBoys.Where(x => x.IsActive).ToListAsync();
                return alldeliveryBoys;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> AddOrUpdateDeliveryBoy(DeliveryBoysReqViewModel dBoys)
        {
            try
            {

                if (dBoys.Id != null)
                {
                    MWDeliveryBoy? existingdBoys = _context.MWDeliveryBoys.Find(dBoys.Id);
                    if (existingdBoys != null)
                    {
                        existingdBoys.Name = dBoys.Name;
                        existingdBoys.Email = dBoys.Email;
                        existingdBoys.Phone = dBoys.Phone;
                        _context.MWDeliveryBoys.Update(existingdBoys);
                        await _context.SaveChangesAsync();
                        return true;
                    };
                    return false;
                }
                else
                {
                    var newdeliveryBoy = new MWDeliveryBoy
                    {
                        Id = Guid.NewGuid(),
                        Name = dBoys.Name,
                        Email = dBoys.Email,
                        Phone = dBoys.Phone,
                        IsActive = true
                    };

                    await _context.MWDeliveryBoys.AddAsync(newdeliveryBoy);
                    await _context.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteDeliveryBoy(Guid Id)
        {
            try
            {
                var deliveryBoy = _context.MWDeliveryBoys.Find(Id);
                if (deliveryBoy != null)
                {
                    deliveryBoy.IsActive = false;
                    _context.MWDeliveryBoys.Update(deliveryBoy);
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

    }
}
