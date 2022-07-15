using Microsoft.EntityFrameworkCore;
using Crm.Domain;
using Crm.Persistence.Configurations;
using Crm.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Crm.Persistence
{
    public class CrmContext : DbContext, IDbContext
    {
        public DbSet<Client> Clients { get; set; } = null!;

        public DbSet<Contract> Contracts { get; set; } = null!;

        public DbSet<Employee> Employees { get; set; } = null!;

        public DbSet<Position> Positions { get; set; } = null!;

        public DbSet<WorkPlan> WorkPlans { get; set; } = null!;
        
        public CrmContext(DbContextOptions<CrmContext> options)
            : base(options) 
        {
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override EntityEntry Remove(object entity)
        {
            return base.Remove(entity);
        }

        ////protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        ////{
        ////    optionsBuilder.UseSqlServer("Server = DESKTOP-TCAD93P; Database = crmdb; Trusted_Connection = True;", b => b.MigrationsAssembly("Crm.Persistence"));
        ////}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new ContractConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new PositionConfiguration());
            modelBuilder.ApplyConfiguration(new WorkPlanConfiguration());
        }
    }
}
