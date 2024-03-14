using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Infrastructure.Entities;

namespace Infrastructure.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<FeatureEntity> Features { get; set; }

        public DbSet<FeatureItemEntity> FeatureItems { get; set; }

        public DbSet<IntegrateItemEntity> IntegrateItem { get; set; }

        public DbSet<IntegrateEntity> Integrate {  get; set; }

        public DbSet<ManageItemEntity> ManageItems { get; set; }

        public DbSet<ManageEntity> Manage { get; set; }
    }
}
