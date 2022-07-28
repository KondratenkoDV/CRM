using System;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.WorkPlan
{
    public class WorkPlanService
    {
        private readonly IDbContext _dbContext;

        public WorkPlanService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(
            DateTime dateStart,
            DateTime dateFinish,
            int contractId,
            CancellationToken cancellationToken)
        {
            var workPlan = new Domain.WorkPlan(
                dateStart,
                dateFinish,
                contractId);

            await _dbContext.WorkPlans.AddAsync(workPlan);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return workPlan.Id;
        }

        public async Task<Domain.WorkPlan> SelectingAsync(int id)
        {
            return await _dbContext.WorkPlans.SingleOrDefaultAsync(w => w.Id == id);
        }

        public async Task UpdateAsync(
            Domain.WorkPlan workPlan,
            DateTime newDateStart,
            DateTime newDateFinish,
            int newContractId,
            CancellationToken cancellationToken)
        {
            var expected = await _dbContext.WorkPlans.AnyAsync(c => c.Id == workPlan.Id);

            if (expected == true)
            {
                workPlan.ChangeDateStart(newDateStart);
                workPlan.ChangeDateFinish(newDateFinish);
                workPlan.ChangeContractId(newContractId);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteAsync(Domain.WorkPlan workPlan, CancellationToken cancellationToken)
        {
            var expected = await _dbContext.WorkPlans.AnyAsync(c => c.Id == workPlan.Id);

            if (expected == true)
            {
                _dbContext.WorkPlans.Remove(workPlan);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
