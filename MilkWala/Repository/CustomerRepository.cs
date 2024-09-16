using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MilkWala.Models;
using MilkWala.RequestModels;

namespace MilkWala.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MWDBContext _context;

        public CustomerRepository(MWDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MwCustomer>> GetCustomersAsync()
        {
            try
            {
                var allCustomers = await _context.MwCustomers.Where(x => x.IsActive).ToListAsync();
                return allCustomers;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddOrUpdateCustomer(CustomerReqViewModel customer)
        {
            try
            {

                if (customer.Id != null)
                {
                    MwCustomer? existingCustomer = _context.MwCustomers.Find(customer.Id);
                    if (existingCustomer != null)
                    {
                        existingCustomer.Name = customer.Name;
                        existingCustomer.Email = customer.Email;
                        existingCustomer.Phone = customer.Phone;
                        _context.MwCustomers.Update(existingCustomer);
                        await _context.SaveChangesAsync();
                        return true;
                    };
                    return false;
                }
                else
                {
                    var newCustomer = new MwCustomer
                    {
                        Id = Guid.NewGuid(),
                        Name = customer.Name,
                        Email = customer.Email,
                        Phone = customer.Phone,
                        IsActive = true
                    };

                    await _context.MwCustomers.AddAsync(newCustomer);
                    await _context.SaveChangesAsync();
                    return true;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task<bool> DeleteCustomer(Guid Id)
        {
            try
            {
                var customer = _context.MwCustomers.Find(Id);
                if (customer != null)
                {
                    customer.IsActive = false;
                    _context.MwCustomers.Update(customer);
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
