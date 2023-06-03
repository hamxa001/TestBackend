using HWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using TestBackend.DTOs.LeavesDTOs;
using TestBackend.IRepository;
using TestBackend.Repository;

namespace TestBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveRepository _leaveRepository;
        public LeaveController(ILeaveRepository leaveRepository)
        {
            _leaveRepository = leaveRepository;
        }

        [HttpGet("GetAllLeaves")]
        public async Task<ActionResult<ServiceResponse<IReadOnlyList<LeavesDto>>>> GetAll()
        {
            var result = await _leaveRepository.GetAllLeaves();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetLeavesByUser/{Id}")]
        public async Task<ActionResult<ServiceResponse<LeaveAggregates>>> Get(string Id)
        {
            var result = await _leaveRepository.GetLeavesCount(Id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
