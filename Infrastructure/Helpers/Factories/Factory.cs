using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Contexts;

namespace Infrastructure.Helpers.Factories
{
    public class BloggingContextFactory : IDesignTimeDbContextFactory<UserContext>
    {
        public UserContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UserContext>();
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Marcuswix\\Documents\\LoacalDataBaseSiliconUser.mdf;Integrated Security=True;Connect Timeout=30");

            return new UserContext(optionsBuilder.Options);
        }
    }
}
