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

        public async Task<int> AddNewClientToDb(
            string name,
            CodeOfTheCountry codeOfTheCountry,
            string regionCode,
            string subscriberNumber,
            CancellationToken cancellationToken)
        {
            var client = new Domain.Client(name, codeOfTheCountry, regionCode, subscriberNumber);

            await _dbContext.Clients.AddAsync(client);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return client.Id;
        }

        public async Task<SelectedClient> SelectingClientFromTheDb(int id)
        {
            _client = await _dbContext.Clients.SingleAsync(c => c.Id == id);

            return new SelectedClient() { Id = _client.Id, Name = _client.Name, PhonNumber = _client.PhoneNumber };
        }

        public async Task UpdateClientDb(SelectedClient selectedClient)
        {
            _client.ChangeName(selectedClient.Name);
            _client.ChangePhoneNumber()
        }
    }
}
