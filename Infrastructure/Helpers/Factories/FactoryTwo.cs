using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Contexts;

namespace Infrastructure.Helpers.Factories
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=B:\\ASP.NET\\SiliconMVC\\Infrastructure\\Data\\LocalDatabaseSilicon.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True");

            return new DataContext(optionsBuilder.Options);
        }
    }
}
