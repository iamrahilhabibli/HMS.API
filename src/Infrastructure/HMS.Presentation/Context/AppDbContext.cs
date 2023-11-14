using HMS.Domain.Entities;
using HMS.Domain.Entities.Common;
using HMS.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HMS.Persistence.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) { }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder.Properties<TimeOnly>()
                .HaveConversion<TimeOnlyConverter>();
        }
        public DbSet<HotelManager> HotelManagers { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Hotel> Hotels { get; set; }    
        public DbSet<HotelPolicy> HotelPolicies { get; set; }   
        public DbSet<Amenity> Amenities { get; set; }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                switch (data.State)
                {
                    case EntityState.Added:
                        data.Entity.DateCreated = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        data.Entity.DateModified = DateTime.UtcNow;
                        break;
                    default:
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
