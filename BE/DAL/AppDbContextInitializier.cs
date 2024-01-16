using BE.Models;
using BE.Utilities.Enums;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BE.DAL
{
    public class AppDbContextInitializier
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _conf;

        public AppDbContextInitializier(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context, IConfiguration conf)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _conf = conf;
        }

        public async Task Migration()
        {
            await _context.Database.MigrateAsync();
        }
        public async Task CreateRoles()
        {
            foreach (var role in Enum.GetValues(typeof(UserRoles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
            }
        }
        public async Task CreateAdmin()
        {
            AppUser user = new AppUser
            {
                Name = "admin",
                Surname = "admin",
                Email = _conf["AdminSettings:Email"],
                UserName = _conf["AdminSettings:UserName"]
            };
            await _userManager.CreateAsync(user, _conf["AdminSettings:Password"]);
            await _userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());
        }

    }
}
