using System;
using Crm.Domain.Interfaces;
using Crm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Crm.Application.Crud.Employee
{
    public class EmployeeCrud
    {
        private Domain.Employee? _employee;

        private readonly IDbContext _dbContext;

        public EmployeeCrud(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddToDbAsync(EmployeeParameters employeeParameters, CancellationToken cancellationToken)
        {
            var employee = new Domain.Employee(
                employeeParameters.FirstName,
                employeeParameters.LastName,
                employeeParameters.PositionId);

            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return employee.Id;
        }

        public async Task<EmployeeParameters> SelectingFromTheDbAsync(int id)
        {
            _employee = await _dbContext.Employees.SingleAsync(e => e.Id == id);

            return new EmployeeParameters()
            {
                Id = _employee.Id,
                FirstName = _employee.FirstName,
                LastName = _employee.LastName,
                PositionId = _employee.PositionId
            };
        }

        public async Task UpdateInTheDbAsync(EmployeeParameters employeeParameters, int id, CancellationToken cancellationToken)
        {
            await SelectingFromTheDbAsync(id);

            if(_employee != null)
            {
                await Task.Run(() =>
                {
                    _employee.ChangeName(employeeParameters.LastName, employeeParameters.FirstName);
                    _employee.ChangePositionId(employeeParameters.PositionId);
                });
                
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteInTheDbAsync(int id, CancellationToken cancellationToken)
        {
            await SelectingFromTheDbAsync(id);

            if(_employee != null)
            {
                _dbContext.Employees.Remove(_employee);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
