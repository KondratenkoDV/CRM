using System;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Contract
{
    public class ContractService : IContractService
    {
        private readonly IDbContext _dbContext;

        public ContractService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(
            string subject,
            string address,
            decimal price,
            int clientId,
            CancellationToken cancellationToken)
        {
            var contract = new Domain.Contract(
                subject,
                address,
                price,
                clientId);

            await _dbContext.Contracts.AddAsync(contract);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return contract.Id;
        }

        public async Task<Domain.Contract> SelectingAsync(int id)
        {
            return await _dbContext.Contracts.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(
            Domain.Contract contract,
            string newSubject,
            string newAddress,
            decimal newPrice,
            int newClientId, 
            CancellationToken cancellationToken)
        {
            var expected = await _dbContext.Contracts.AnyAsync( c => c.Id == contract.Id);

            if (expected == true)
            {
                contract.ChangeSubject(newSubject);
                contract.ChangeAddress(newAddress);
                contract.ChangePrice(newPrice);
                contract.ChangeClientId(newClientId);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteAsync(Domain.Contract contract, CancellationToken cancellationToken)
        {
            var expected = await _dbContext.Contracts.AnyAsync(c => c.Id == contract.Id);

            if (expected == true)
            {
                _dbContext.Contracts.Remove(contract);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<IEnumerable<Domain.Contract>> AllAsync()
        {
            return await _dbContext.Contracts.ToListAsync();
        }
    }
}
