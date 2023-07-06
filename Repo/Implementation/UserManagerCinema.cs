using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Repo.Interfaces;
using Enums;

namespace Repo.Implementation
{
    public class UserManagerCinema: IUserManager
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ILogger<UserManagerCinema> _logger;
        private ApplicationDbContext _db;
        public UserManagerCinema(UserManager<IdentityUser> userManager,
                            RoleManager<IdentityRole> roleManager,
                            ILogger<UserManagerCinema> logger,
                            ApplicationDbContext db
                            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _db = db;
        }

        public async Task<int> AddIdentityRoles()
        {
            try
            {
                await _roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));
                await _roleManager.CreateAsync(new IdentityRole(Enums.Roles.Customer.ToString()));
                return 1;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }

        }
    }
}
