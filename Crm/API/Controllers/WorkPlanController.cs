using Microsoft.AspNetCore.Mvc;
using Application.Services.WorkPlan;
using API.Helpers;
using API.Models;

namespace API.Controllers
{
    public class WorkPlanController : Controller
    {
        [HttpPost]
        public async Task<int> CreateNewWorkPlan(
            WorkPlanModel workPlanModel,
            CancellationToken cancellationToken)
        {
            var workPlanService = new WorkPlanService(CompoundDb.Compound());

            return await workPlanService.AddAsync(
                workPlanModel.DateStart,
                workPlanModel.DateFinish,
                workPlanModel.ContractId,
                cancellationToken);
        }

        [HttpPost]
        public async Task<WorkPlanModel> SelectingWorkPlan(int id)
        {
            var workPlanService = new WorkPlanService(CompoundDb.Compound());

            var workPlan = await workPlanService.SelectingAsync(id);

            return new WorkPlanModel()
            {
                Id = workPlan.Id,
                DateStart = workPlan.DateStart,
                DateFinish = workPlan.DateFinish,
                ContractId = workPlan.ContractId
            };
        }

        [HttpPost]
        public async Task UpdateWorkPlan(
            WorkPlanModel workPlanModel,
            int id,
            CancellationToken cancellationToken)
        {
            var workPlanService = new WorkPlanService(CompoundDb.Compound());

            var workPlan = await workPlanService.SelectingAsync(id);

            await workPlanService.UpdateAsync(
                workPlan,
                workPlanModel.DateStart,
                workPlanModel.DateFinish,
                workPlanModel.ContractId,
                cancellationToken);
        }

        [HttpPost]
        public async Task DeleteWorkPlan(int id, CancellationToken cancellationToken)
        {
            var workPlanService = new WorkPlanService(CompoundDb.Compound());

            var workPlan = await workPlanService.SelectingAsync(id);

            await workPlanService.DeleteAsync(workPlan, cancellationToken);
        }
    }
}
