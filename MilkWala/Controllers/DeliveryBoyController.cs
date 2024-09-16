using Microsoft.AspNetCore.Mvc;
using MilkWala.Models;
using MilkWala.Repositories;
using MilkWala.RequestModels;

namespace MilkWala.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryBoyController : ControllerBase
    {
        private readonly IDeliveryBoysRepository _deliveryBoysRepository;

        public DeliveryBoyController(IDeliveryBoysRepository deliveryBoysRepository)
        {
            _deliveryBoysRepository = deliveryBoysRepository;
        }

        [HttpGet("GetDeliveryBoys")]
        public async Task<ListApiResponse<List<MWDeliveryBoy>>> GetDeliveryBoys()
        {
            var dBoys = await _deliveryBoysRepository.GetDeliveryBoysAsync();
            return new ListApiResponse<List<MWDeliveryBoy>>(dBoys == null ? 404 : 200, dBoys.ToList(), 0);
        }

        [HttpPost("AddOrUpdateDeliveryBoy")]
        public async Task<ApiResponse> AddOrUpdateDeliveryBoy(DeliveryBoysReqViewModel dBoys)
        {
            bool deliveryBoys = await _deliveryBoysRepository.AddOrUpdateDeliveryBoy(dBoys);
            return new ApiResponse(deliveryBoys ? 200 : 404, deliveryBoys, dBoys.Id != null ? "Record Updated Successfully" : "Record Added Successfully");
        }

        [HttpGet("DeleteDeliveryBoy")]
        public async Task<ApiResponse> DeleteDeliveryBoy(Guid Id)
        {
            bool dBoys = await _deliveryBoysRepository.DeleteDeliveryBoy(Id);
            return new ApiResponse(dBoys ? 200 : 404, dBoys, dBoys ? "Record Deleted Successfully" : "Resource not found");

        }
    }
}
