using System;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Employee
{
    public class EmployeeService
    {
        private readonly IDbContext _dbContext;

        public EmployeeService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(
            string firstName,
            string lastName,
            int positionId,
            CancellationToken cancellationToken)
        {
            var employee = new Domain.Employee(
                firstName,
                lastName,
                positionId);

            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return employee.Id;
        }

        public async Task<Domain.Employee> SelectingAsync(int id)
        {
            return await _dbContext.Employees.SingleAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(
            Domain.Employee employee,
            string newFirstName,
            string newLastName,
            int newPositionId,
            CancellationToken cancellationToken)
        {
            var expected = await _dbContext.Employees.AnyAsync(c => c.Id == employee.Id);

            if (expected == true)
            {
                employee.ChangeFirstName(newFirstName);
                employee.ChangeLastName(newLastName);
                employee.ChangePositionId(newPositionId);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteAsync(Domain.Employee employee, CancellationToken cancellationToken)
        {
            var expected = await _dbContext.Employees.AnyAsync(c => c.Id == employee.Id);

            if (expected == true)
            {
                _dbContext.Employees.Remove(employee);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
