using Domain.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUserManagementService
    {
        Task<List<UserDto>> GetAllUsers();
        Task<IdentityUser> GetUserById(string userId);
    }
}
