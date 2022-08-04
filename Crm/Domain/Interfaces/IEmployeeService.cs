using System;

namespace Domain.Interfaces
{
    public interface IEmployeeService
    {
        Task<int> AddAsync(
            string firstName,
            string lastName,
            int positionId,
            CancellationToken cancellationToken);

        Task<Domain.Employee> SelectingAsync(int id);

        Task UpdateAsync(
            Domain.Employee employee,
            string newFirstName,
            string newLastName,
            int newPositionId,
            CancellationToken cancellationToken);

        Task DeleteAsync(Domain.Employee employee, CancellationToken cancellationToken);
    }
}
