using HMS.Domain.Enums;
using HMS.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HMS.Persistence.Context
{
    public class AppDbContextInitialiser
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration; 
        private readonly UserManager<AppUser> _userManager;

        public AppDbContextInitialiser(AppDbContext context,
                                       RoleManager<IdentityRole> roleManager, IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task InitialiseAsync()
        {
            await _context.Database.MigrateAsync();
        }
        public async Task RoleSeedAsync()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
        }

        public async Task UserSeedAsync()
        {
            AppUser masterProfile = new()
            {
                UserName = _configuration["Master: UserName"],
                Email = _configuration["Master:Email"],
                PhoneNumber = _configuration["Master:PhoneNumber"]
            };
            await _userManager.CreateAsync(masterProfile, _configuration["Master:Password"]);
            await _userManager.AddToRoleAsync(masterProfile, Roles.Master.ToString());
        }
    }
}
