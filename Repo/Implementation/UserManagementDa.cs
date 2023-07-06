using Domain.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repo.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.Implementation
{
    public class UserManagementDa : IUserManagementDa
    {
        private static ILogger<UserManagementDa> _logger;
        private readonly ApplicationDbContext _db;
        public UserManagementDa(ILogger<UserManagementDa> logger, ApplicationDbContext db)
        {
                _db = db;
            _logger = logger;
        }
        public async Task<List<UserDto>> GetAllUsers()
        {
            try
            {
                var listUsers = new List<UserDto>();
                var usersFromDb = await _db.Users.ToListAsync();
                foreach (var user in usersFromDb)
                {
                    var userRole = await _db.UserRoles.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
                    var role = new IdentityRole();
                    if (userRole != null)
                    {
                         role = await _db.Roles.Where(x => x.Id == userRole.RoleId).FirstOrDefaultAsync();
                    }

                    listUsers.Add(new UserDto
                    {
                        UserId = user.Id,
                        Email = user.Email.ToString(),
                        Username = user.UserName.ToString(),
                        Role = role.Name
                    });
                }

                return listUsers;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
        public async Task<IdentityUser> GetUserById(string userId)
        {
            try
            {
                return await _db.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
