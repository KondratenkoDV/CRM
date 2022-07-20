using Microsoft.EntityFrameworkCore;
using Persistence;
using Domain.Interfaces;

namespace API.Helpers
{
    public static class CompoundDb
    {
        public static IDbContext Compound()
        {
            var builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory());
            
            builder.AddJsonFile("appsettings.json");
            
            var config = builder.Build();
            
            string connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<CrmContext>();
            var options = optionsBuilder.UseSqlServer(connectionString).Options;

            return new CrmContext(options);
        }        
    }
}
