using Microsoft.AspNetCore.Mvc;
using Application.Services.WorkPlan;
using Domain.Interfaces;
using API.DTOs.WorkPlan;

namespace API.Controllers
{
    public class WorkPlanController : Controller
    {
        private readonly WorkPlanService _workPlanService;

        public WorkPlanController(IDbContext dbContext)
        {
            _workPlanService = new WorkPlanService(dbContext);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateNewWorkPlan(
            CreateWorkPlanDto createWorkPlanDto,
            CancellationToken cancellationToken)
        {
            return await _workPlanService.AddAsync(
                createWorkPlanDto.DateStart,
                createWorkPlanDto.DateFinish,
                createWorkPlanDto.ContractId,
                cancellationToken);
        }

        [HttpGet]
        public async Task<ActionResult<SelectingWorkPlanDto>> SelectingWorkPlan(int id)
        {
            var workPlan = await _workPlanService.SelectingAsync(id);

            return new SelectingWorkPlanDto()
            {
                Id = workPlan.Id,
                DateStart = workPlan.DateStart,
                DateFinish = workPlan.DateFinish,
                ContractId = workPlan.ContractId
            };
        }

        [HttpPut]
        public async Task UpdateWorkPlan(
            UpdateWorkPlanDto updateWorkPlanDto,
            int id,
            CancellationToken cancellationToken)
        {
            var workPlan = await _workPlanService.SelectingAsync(id);

            await _workPlanService.UpdateAsync(
                workPlan,
                updateWorkPlanDto.NewDateStart,
                updateWorkPlanDto.NewDateFinish,
                updateWorkPlanDto.NewContractId,
                cancellationToken);
        }

        [HttpDelete]
        public async Task DeleteWorkPlan(int id, CancellationToken cancellationToken)
        {
            var workPlan = await _workPlanService.SelectingAsync(id);

            await _workPlanService.DeleteAsync(workPlan, cancellationToken);
        }
    }
}
