using System;
using Microsoft.EntityFrameworkCore;

namespace Crm.Domain
{
    public interface IDbContext
    {
        public DbSet<Client> Clients { get; set; }

        public DbSet<Contract> Contracts { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<WorkPlan> WorkPlans { get; set; }
    }
}
