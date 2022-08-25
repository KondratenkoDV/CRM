using System;

namespace Domain.Interfaces
{
    public interface IContractService
    {
        Task<int> AddAsync(
            string subject,
            string address,
            decimal price,
            int clientId,
            CancellationToken cancellationToken);

        Task<Domain.Contract> SelectingAsync(int id);

        Task UpdateAsync(
            Domain.Contract contract,
            string newSubject,
            string newAddress,
            decimal newPrice,
            int newClientId,
            CancellationToken cancellationToken);

        Task DeleteAsync(Domain.Contract contract, CancellationToken cancellationToken);

        Task<IEnumerable<Domain.Contract>> AllAsync();
    }
}
