using System;
using Crm.Domain.Interfaces;
using Crm.Domain;

namespace Crm.Application.Crud.ClientCrud
{
    public class CreateClient
    {
        private readonly IDbContext _dbContext;

        public CreateClient(IDbContext dbContext)
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
            var client = new Client(name, codeOfTheCountry, regionCode, subscriberNumber);

            await _dbContext.Clients.AddAsync(client);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return client.Id;
        }
    }
}
