using System;
using Domain.Interfaces;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Client
{
    public class ClientService : IClientService
    {
        private readonly IDbContext _dbContext;

        public ClientService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(
            string name,
            CodeOfTheCountry codeOfTheCountry,
            string regionCode,
            string subscriberNumber,
            CancellationToken cancellationToken)
        {
            var client = new Domain.Client(
                name,
                codeOfTheCountry,
                regionCode,
                subscriberNumber);

            await _dbContext.Clients.AddAsync(client);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return client.Id;
        }

        public async Task<Domain.Client> SelectingAsync(int id)
        {
            return await _dbContext.Clients.SingleOrDefaultAsync(c => c.Id == id);                        
        }

        public async Task UpdateAsync(
            Domain.Client client, 
            string newName, 
            CodeOfTheCountry newCodeOfTheCountry,
            string newRegionCode,
            string newSubscriberNumber,
            CancellationToken cancellationToken)
        {
            var expected = await _dbContext.Clients.AnyAsync(c => c.Id == client.Id);

            if (expected == true)
            {
                client.ChangeName(newName);
                client.ChangePhoneNumber(newCodeOfTheCountry, newRegionCode, newSubscriberNumber);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteAsync(Domain.Client сlient, CancellationToken cancellationToken)
        {
            var expected = await _dbContext.Clients.AnyAsync(c => c.Id == сlient.Id);

            if (expected == true)
            {
                _dbContext.Clients.Remove(сlient);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
