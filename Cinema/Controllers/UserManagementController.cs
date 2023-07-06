using ClosedXML.Excel;
using Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;

namespace Cinema.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserManagementController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly IUserManagementService _userManagementService;
        public UserManagementController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager,
             SignInManager<IdentityUser> signInManager,
             IUserStore<IdentityUser> userStore,
             IUserEmailStore<IdentityUser> emailStore,
             IUserManagementService userManagementService,
             RoleManager<IdentityRole> roleManager
             )
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _emailStore = emailStore;
            _userStore = userStore;
            _userManagementService = userManagementService;
            _roleManager = roleManager;
        }
        private IdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<IdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(IdentityUser)}'. " +
                    $"Ensure that '{nameof(IdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManagementService.GetAllUsers();
            return View(users);
           
        }

        public async Task<IActionResult> MakeAdmin(string userId)
        {
            var user = await _userManagementService.GetUserById(userId);
            await _userManager.RemoveFromRoleAsync(user, "Customer");
            await _userManager.AddToRoleAsync(user, "Admin");
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemoveAdmin(string userId)
        {
            var user = await _userManagementService.GetUserById(userId);
            await _userManager.RemoveFromRoleAsync(user, "Admin");
            await _userManager.AddToRoleAsync(user, "Customer");
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> ImportUsers(IFormFile excelUsers)
        {
            // Create the "Admin" role
            
            if (excelUsers != null && excelUsers.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    excelUsers.CopyTo(stream);

                    using (var workbook = new XLWorkbook(stream))
                    {
                        var worksheet = workbook.Worksheet(1);
                        var firstRowUsed = worksheet.FirstRowUsed();
                        var lastRowUsed = worksheet.LastRowUsed();
                        var rowCount = lastRowUsed.RowNumber() - firstRowUsed.RowNumber() + 1;

                        for (int i = 1; i <= rowCount; i++)
                        {
                            var row = worksheet.Row(i);
                            var email = row.Cell(1).Value.ToString();
                            var password = row.Cell(2).Value.ToString();
                            var role = row.Cell(3).Value.ToString();

                            var user = CreateUser();

                            await _userStore.SetUserNameAsync(user, email, CancellationToken.None);
                            await _emailStore.SetEmailAsync(user, email, CancellationToken.None);
                            var result = await _userManager.CreateAsync(user, password);

                            var userId = _userManager.GetUserIdAsync(user);
                            await _userManager.AddToRoleAsync(user, role);

                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }


    }
}
