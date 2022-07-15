using System;
using Crm.Domain.Interfaces;
using Crm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Crm.Application.Crud.Contract
{
    public class ContractCrud
    {
        private Domain.Contract? _contract;

        private readonly IDbContext _dbContext;

        public ContractCrud(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddToDbAsync(ContractParameters contractParameter, CancellationToken cancellationToken)
        {
            var contract = new Domain.Contract(
                contractParameter.Subject,
                contractParameter.Address,
                contractParameter.Price,
                contractParameter.ClientId);

            await _dbContext.Contracts.AddAsync(contract);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return contract.Id;
        }

        public async Task<ContractParameters> SelectingFromTheDbAsync(int id)
        {
            _contract = await _dbContext.Contracts.SingleAsync(c => c.Id == id);

            return new ContractParameters()
            {
                Id = _contract.Id,
                Subject = _contract.Subject,
                Address = _contract.Address,
                Price = _contract.Price,
                ClientId = _contract.ClientId
            };
        }

        public async Task UpdateInTheDbAsync(ContractParameters contractParameter, int id, CancellationToken cancellationToken)
        {
            await SelectingFromTheDbAsync(id);

            if (_contract != null)
            {
                await Task.Run(() =>
                {
                    _contract.ChangeSubject(contractParameter.Subject);
                    _contract.ChangeAddress(contractParameter.Address);
                    _contract.ChangePrice(contractParameter.Price);
                    _contract.ChangeClientId(contractParameter.ClientId);
                });

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteInTheDbAsync(int id, CancellationToken cancellationToken)
        {
            await SelectingFromTheDbAsync(id);

            if (_contract != null)
            {
                _dbContext.Contracts.Remove(_contract);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
