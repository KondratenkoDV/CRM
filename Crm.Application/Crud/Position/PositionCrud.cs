using System;
using Crm.Domain.Interfaces;
using Crm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Crm.Application.Crud.Position
{
    public class PositionCrud
    {
        private Domain.Position? _position;

        private readonly IDbContext _dbContext;

        public PositionCrud(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddToDbAsync(PositionParameters positionParameters, CancellationToken cancellationToken)
        {
            var position = new Domain.Position(positionParameters.Name);

            await _dbContext.Positions.AddAsync(position);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return position.Id;
        }

        public async Task<PositionParameters> SelectingFromTheDbAsync(int id)
        {
            _position = await _dbContext.Positions.SingleAsync(p => p.Id == id);

            return new PositionParameters()
            {
                Id = _position.Id,
                Name = _position.Name                
            };
        }

        public async Task UpdateInTheDbAsync(PositionParameters positionParameters, int id, CancellationToken cancellationToken)
        {
            await SelectingFromTheDbAsync(id);

            if(_position != null)
            {
                await Task.Run(() =>
                {
                    _position.ChangeName(positionParameters.Name);
                });

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteInTheDbAsync(int id, CancellationToken cancellationToken)
        {
            await SelectingFromTheDbAsync(id);

            if(_position != null)
            {
                _dbContext.Positions.Remove(_position);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
