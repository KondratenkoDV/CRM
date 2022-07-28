using System;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Position
{
    public class PositionService
    {
        private readonly IDbContext _dbContext;

        public PositionService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(string name, CancellationToken cancellationToken)
        {
            var position = new Domain.Position(name);

            await _dbContext.Positions.AddAsync(position);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return position.Id;
        }

        public async Task<Domain.Position> SelectingAsync(int id)
        {
            return await _dbContext.Positions.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(
            Domain.Position position,
            string newName,
            CancellationToken cancellationToken)
        {
            var expected = await _dbContext.Positions.AnyAsync(c => c.Id == position.Id);

            if (expected == true)
            {
                position.ChangeName(newName);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteAsync(Domain.Position position, CancellationToken cancellationToken)
        {
            var expected = await _dbContext.Positions.AnyAsync(c => c.Id == position.Id);

            if (expected == true)
            {
                _dbContext.Positions.Remove(position);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
