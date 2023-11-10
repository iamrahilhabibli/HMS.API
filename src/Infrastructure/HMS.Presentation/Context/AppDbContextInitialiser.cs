using HMS.Domain.Enums;
using HMS.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HMS.Persistence.Context
{
    public class AppDbContextInitialiser
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AppDbContextInitialiser(AppDbContext context,
                                       RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
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
                    await _roleManager.CreateAsync(new() { Name = role.ToString() });
                }
            }
        }
    }
}
