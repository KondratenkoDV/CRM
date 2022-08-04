using System;

namespace Domain.Interfaces
{
    public interface IWorkPlanService
    {
        Task<int> AddAsync(
            DateTime dateStart,
            DateTime dateFinish,
            int contractId,
            CancellationToken cancellationToken);

        Task<Domain.WorkPlan> SelectingAsync(int id);

        Task UpdateAsync(
            Domain.WorkPlan workPlan,
            DateTime newDateStart,
            DateTime newDateFinish,
            int newContractId,
            CancellationToken cancellationToken);

        Task DeleteAsync(Domain.WorkPlan workPlan, CancellationToken cancellationToken);
    }
}
