using System;

namespace Domain.Interfaces
{
    public interface IPositionService
    {
        Task<int> AddAsync(string name, CancellationToken cancellationToken);

        Task<Domain.Position> SelectingAsync(int id);

        Task UpdateAsync(
            Domain.Position position,
            string newName,
            CancellationToken cancellationToken);

        Task DeleteAsync(Domain.Position position, CancellationToken cancellationToken);
    }
}
