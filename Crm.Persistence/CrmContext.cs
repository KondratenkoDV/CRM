using Microsoft.EntityFrameworkCore;
using Crm.Domain;

namespace Crm.Persistence
{
    public class CrmContext : DbContext
    {
        public DbSet<Contract>? Contracts { get; set; } = null!;

        public DbSet<Client> Clients { get; set; } = null!;

        public DbSet<Employee> Employees { get; set; } = null!;

        public DbSet<Position> Positions { get; set; } = null!;

        public DbSet<WorkPlan> WorkPlans { get; set; } = null!;

        public CrmContext()
        { 
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = DESKTOP-TCAD93P; Database = crmdb; Trusted_Connection = True; ");
        }
    }
}
