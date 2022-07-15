using System;
using Crm.Domain.Interfaces;
using Crm.Domain;
using Microsoft.EntityFrameworkCore;

namespace Crm.Application.Crud.WorkPlan
{
    public class WorkPlanCrud
    {
        private Domain.WorkPlan? _position;

        private readonly IDbContext _dbContext;

        public WorkPlanCrud(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddToDbAsync(WorkPlanParameters workPlanParameters, CancellationToken cancellationToken)
        {
            var workPlan = new Domain.WorkPlan(
                workPlanParameters.DateStart,
                workPlanParameters.DateFinish,
                workPlanParameters.ContractId);

            await _dbContext.WorkPlans.AddAsync(workPlan);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return workPlan.Id;
        }

        public async Task<WorkPlanParameters> SelectingFromTheDbAsync(int id)
        {
            _position = await _dbContext.WorkPlans.SingleAsync(w => w.Id == id);

            return new WorkPlanParameters()
            {
                Id = _position.Id,
                DateStart = _position.DateStart,
                DateFinish = _position.DateFinish,
                ContractId = _position.ContractId
            };
        }

        public async Task UpdateInTheDbAsync(WorkPlanParameters workPlanParameters, int id, CancellationToken cancellationToken)
        {
            await SelectingFromTheDbAsync(id);

            if(_position != null)
            {
                await Task.Run(() =>
                {
                    _position.ChangeDateStart(workPlanParameters.DateStart);
                    _position.ChangeDateFinish(workPlanParameters.DateFinish);
                    _position.ChangeContractId(workPlanParameters.ContractId);
                });

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteInTheDbAsync(int id, CancellationToken cancellationToken)
        {
            await SelectingFromTheDbAsync(id);

            if(_position != null)
            {
                _dbContext.WorkPlans.Remove(_position);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
