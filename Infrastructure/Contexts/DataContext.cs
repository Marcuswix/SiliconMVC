using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Infrastructure.Entities;

namespace Infrastructure.Contexts
{
    public class DataContext : IdentityDbContext<UserEntity>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AddressEntity> Addresses { get; set; }

        public DbSet<FeatureEntity> Features { get; set; }

        public DbSet<FeatureItemEntity> FeatureItems { get; set; }

        public DbSet<IntegrateItemEntity> IntegrateItem { get; set; }

        public DbSet<IntegrateEntity> Integrate {  get; set; }
    }
}
