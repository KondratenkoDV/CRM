using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Crm.Domain;

namespace Crm.Persistence
{
    public class CrmContext : DbContext, IDbContext
    {
        public DbSet<Client> Clients { get; set; } = null!;

        public DbSet<Contract> Contracts { get; set; } = null!;

        public DbSet<Employee> Employees { get; set; } = null!;

        public DbSet<Position> Positions { get; set; } = null!;

        public DbSet<WorkPlan> WorkPlans { get; set; } = null!;

        public CrmContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = DESKTOP-TCAD93P; Database = crmdb; Trusted_Connection = True;", b => b.MigrationsAssembly("Crm.Persistence"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>().HasKey(c => c.Id);
            modelBuilder.Entity<Client>().HasIndex(c => new { c.Name }).IsUnique();
            modelBuilder.Entity<Client>().Property(c => c.Name).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<Client>().Property(c => c.PhonNumber).HasMaxLength(255).IsRequired();

            modelBuilder.Entity<Contract>().HasKey(c => c.Id);
            modelBuilder.Entity<Contract>().HasIndex(c => new { c.Subject, c.Address }).IsUnique();
            modelBuilder.Entity<Contract>().Property(c => c.Subject).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<Contract>().Property(c => c.Address).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<Contract>().Property(c => c.Price).IsRequired();
            modelBuilder.Entity<Contract>().HasOne(c => c.Client).WithMany(c => c.Contracts).HasForeignKey(c => c.ClientId);
            modelBuilder.Entity<Contract>().HasMany(c => c.Employees).WithMany(e => e.Contracts).UsingEntity(j => j.ToTable("EmployeesAndContracts"));


            modelBuilder.Entity<Employee>().HasKey(e => e.Id);
            modelBuilder.Entity<Employee>().HasIndex(e => new { e.Name }).IsUnique();
            modelBuilder.Entity<Employee>().Property(e => e.Name).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<Employee>().HasOne(e => e.Position).WithMany(p => p.Employees).HasForeignKey(e => e.PositionId);

            modelBuilder.Entity<Position>().HasKey(p => p.Id);
            modelBuilder.Entity<Position>().HasIndex(p => new { p.Name }).IsUnique();
            modelBuilder.Entity<Position>().Property(p => p.Name).HasMaxLength(255).IsRequired();

            modelBuilder.Entity<WorkPlan>().HasKey(w => w.Id);
            modelBuilder.Entity<WorkPlan>().HasIndex(w => new { w.DateStart, w.DateFinish }).IsUnique();
            modelBuilder.Entity<WorkPlan>().Property(w => w.DateStart).IsRequired();
            modelBuilder.Entity<WorkPlan>().Property(w => w.DateFinish).IsRequired();
            modelBuilder.Entity<WorkPlan>().HasOne(w => w.Contract).WithMany(c => c.WorkPlans).HasForeignKey(w => w.ContractId);
        }
    }
}
