using Domain.DTO;
using Microsoft.AspNetCore.Identity;
using Repo.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserManagementDa _userManagementDa;
        public UserManagementService(IUserManagementDa userManagementDa)
        {
            _userManagementDa = userManagementDa;
        }
        public async Task<List<UserDto>> GetAllUsers()
        {
            return await _userManagementDa.GetAllUsers();
        }

        public async Task<IdentityUser> GetUserById(string userId)
        {
            return await _userManagementDa.GetUserById(userId);
        }

    }
}
