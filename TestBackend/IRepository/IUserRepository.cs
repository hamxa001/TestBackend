using HWebAPI.DTOs.UserDTOs;
using HWebAPI.Models;

namespace HWebAPI.IRepository
{
    public interface IUserRepository
    {
        public Task<ServiceResponse<UserModel>> AddUser(AddUserDto Users);
    }
}
