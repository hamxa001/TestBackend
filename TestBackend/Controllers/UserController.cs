using HWebAPI.DTOs.UserDTOs;
using HWebAPI.IRepository;
using HWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository UserRepo)
        {
            _userRepo = UserRepo;
        }

        [HttpPost("adduser")]
        public async Task<ActionResult<ServiceResponse<UserModel>>> Adduser(AddUserDto Users)
        {
            var result = await _userRepo.AddUser(Users);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
