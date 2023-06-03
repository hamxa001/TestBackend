using AutoMapper;
using HWebAPI.DTOs.UserDTOs;
using HWebAPI.Helpers;
using HWebAPI.IRepository;
using HWebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace HWebAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<UserModel>> AddUser(AddUserDto user)
        {
            var ServiceResponse = new ServiceResponse<UserModel>();
            UserModel Users = _mapper.Map<UserModel>(user);
            try
            {
                if (await UserExist(Users.UserName))
                {
                    ServiceResponse.Success = false;
                    ServiceResponse.Message = "User Already Exist";
                    return ServiceResponse;
                }
                CreatePasswordHash(Users.PasswordHash, out byte[] PasswordMade, out byte[] PasswordSalt);

                Users.Id = CommonCode.GuidID();
                Users.SecurityStamp = CommonCode.GuidID();
                Users.ConcurrencyStamp = CommonCode.GuidID();
                Users.PasswordHash = Convert.ToBase64String(PasswordMade);
                Users.PasswordSalt = Convert.ToBase64String(PasswordSalt);
                //add user using following code
                var AddUser = await _context.Users.AddAsync(Users);
                await _context.SaveChangesAsync();
                // now add user role using the following code
                var AssignRole = await _context.UserRoles.AddAsync(new IdentityUserRole<string> { UserId = Users.Id, RoleId = user.Userroleid });
                await _context.SaveChangesAsync();
                // ON success complete request using following code
                ServiceResponse.Data = await _context.Users.SingleAsync(x => x.Id == Users.Id);
                ServiceResponse.Success = true;
                ServiceResponse.Message = "User successfully added!";
            }
            catch (Exception ex)
            {
                ServiceResponse.Success = false;
                ServiceResponse.Message = ex.Message;
            }
            return ServiceResponse;
        }
        private void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));
            }
        }
        private async Task<bool> UserExist(string Username)
        {
            if (await _context.Users.AnyAsync(x => x.UserName.ToLower().Equals(Username.ToLower())))
            {
                return true;
            }
            return false;
        }
    }
}
