using Domain.Enum;
using System;

namespace Domain.Interfaces
{
    public interface IClientService
    {
        Task<int> AddAsync(
            string name,
            CodeOfTheCountry codeOfTheCountry,
            string regionCode,
            string subscriberNumber,
            CancellationToken cancellationToken);

        Task<Domain.Client> SelectingAsync(int id);

        Task UpdateAsync(
            Domain.Client client,
            string newName,
            CodeOfTheCountry newCodeOfTheCountry,
            string newRegionCode,
            string newSubscriberNumber,
            CancellationToken cancellationToken);

        Task DeleteAsync(Domain.Client сlient, CancellationToken cancellationToken);
    }
}
