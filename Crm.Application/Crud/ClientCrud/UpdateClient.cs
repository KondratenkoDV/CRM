using System;
using Crm.Domain.Interfaces;
using Crm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Crm.Application.Crud.ClientCrud
{
    internal class UpdateClient
    {
        private readonly IDbContext _dbContext;

        public UpdateClient(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task UpdateClientDb(SelectedClient selectedClient)
        {
            var client = await _dbContext.Clients.SingleAsync(c => c.Id == selectedClient.Id);

            if (client != null)
            {

            }
        }
    }
}
