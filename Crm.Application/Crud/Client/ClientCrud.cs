using System;
using Crm.Domain.Interfaces;
using Crm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Crm.Application.Crud.Client
{
    public class ClientCrud
    {
        private Domain.Client? _client;

        private readonly IDbContext _dbContext;

        public ClientCrud(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddToDbAsync(ClientParameters clientParameter, CancellationToken cancellationToken)
        {
            var client = new Domain.Client(
                clientParameter.Name,
                clientParameter.СodeOfTheCountry,
                clientParameter.RegionCode,
                clientParameter.SubscriberNumber);

            await _dbContext.Clients.AddAsync(client);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return client.Id;
        }

        public async Task<ClientParameters> SelectingFromTheDbAsync(int id)
        {
            _client = await _dbContext.Clients.SingleAsync(c => c.Id == id);

            return new ClientParameters() 
            { 
                Id = _client.Id, 
                Name = _client.Name, 
                СodeOfTheCountry = _client.СodeOfTheCountry, 
                RegionCode = _client.RegionCode, 
                SubscriberNumber = _client.SubscriberNumber 
            };
        }

        public async Task UpdateInTheDbAsync(ClientParameters newSelectedClient, int id, CancellationToken cancellationToken)
        {
            await SelectingFromTheDbAsync(id);

            if (_client != null)
            {
                await Task.Run(() =>
                {
                    _client.ChangeName(newSelectedClient.Name);

                    _client.ChangePhoneNumber(
                    newSelectedClient.СodeOfTheCountry,
                    newSelectedClient.RegionCode,
                    newSelectedClient.SubscriberNumber);
                });                

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteInTheDbAsync(int id, CancellationToken cancellationToken)
        {
            await SelectingFromTheDbAsync(id);

            if (_client != null)
            {
                _dbContext.Clients.Remove(_client);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
