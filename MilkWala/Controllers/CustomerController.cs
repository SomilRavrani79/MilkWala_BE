using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MilkWala.Models;
using MilkWala.Repositories;
using MilkWala.RequestModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MilkWala.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet("GetCustomers")]
        //[JwtMiddleware]
        public async Task<ListApiResponse<List<MwCustomer>>> GetCustomers()
        {
            var customers = await _customerRepository.GetCustomersAsync();
            return new ListApiResponse<List<MwCustomer>>(customers == null ? 404 : 200, customers.ToList(), 0, "Records fetched successfully");
        }

        [HttpPost("AddOrUpdateCustomer")]
        public async Task<ApiResponse> AddOrUpdateCustomer(CustomerReqViewModel customer)
        {
            try
            {
                bool customers = await _customerRepository.AddOrUpdateCustomer(customer);
                return new ApiResponse(customers ? 200 : 404, customers, customer.Id != null ? "Record Updated Successfully" : "Record Added Successfully");
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
            }
        }

        [HttpGet("DeleteCustomer")]
        public async Task<ApiResponse> DeleteCustomer(Guid Id)
        {
            bool customers = await _customerRepository.DeleteCustomer(Id);
            return new ApiResponse(customers ? 200 : 404, customers, customers ? "Recored Deleted Successfully" : "Resource not found");

        }
    }
}
