using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MilkWala.IRepository;
using MilkWala.Models;
using MilkWala.Repositories;
using MilkWala.RequestModels;

namespace MilkWala.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerRespository _ownerRepository;

        public OwnerController(IOwnerRespository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        [HttpGet("GetOwners")]
        public async Task<ListApiResponse<List<MWOwner>>> GetOwners()
        {
            var owner = await _ownerRepository.GetOwnersAsync();
            return new ListApiResponse<List<MWOwner>>(owner == null ? 404 : 200, owner.ToList(), 0);
        }

        [HttpPost("AddOrUpdateOwner")]
        public async Task<ApiResponse> AddOrUpdateOwner(OwnerReqViewModel owner)
        {
            bool Owners = await _ownerRepository.AddOrUpdateOwner(owner);
            return new ApiResponse(Owners ? 200 : 404, Owners, owner.Id != null ? "Record Updated Successfully" : "Record Added Successfully");
        }

        [HttpGet("DeleteOwner")]
        public async Task<ApiResponse> DeleteOwner(Guid Id)
        {
            bool owner = await _ownerRepository.DeleteOwner(Id);
            return new ApiResponse(owner ? 200 : 404, owner, owner ? "Record Deleted Successfully" : "Resource not found");

        }
    }
}
