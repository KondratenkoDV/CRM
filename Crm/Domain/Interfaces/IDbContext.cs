using System;
using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces
{
    public interface IDbContext
    {
        DbSet<Client> Clients { get; set; }

        DbSet<Contract> Contracts { get; set; }

        DbSet<Employee> Employees { get; set; }

        DbSet<Position> Positions { get; set; }

        DbSet<WorkPlan> WorkPlans { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
