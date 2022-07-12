using System;
using Crm.Domain.Interfaces;
using Crm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Crm.Application.Crud.ClientCrud
{
    public class ReadClient
    {
        private readonly IDbContext _dbContext;

        public ReadClient(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<SelectedClient> SelectingClientFromTheDb(int id)
        {
            var client = await _dbContext.Clients.SingleAsync(c => c.Id == id);

            return new SelectedClient() {Id = client.Id, Name = client.Name, PhonNumber = client.PhonNumber };
        }
    }
}
