using Microsoft.AspNetCore.Mvc;
using Application.Services.WorkPlan;
using Domain.Interfaces;
using API.DTOs.WorkPlan;

namespace API.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class WorkPlanController : ControllerBase
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

        [HttpGet("{id}")]
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

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
        public async Task DeleteWorkPlan(int id, CancellationToken cancellationToken)
        {
            var workPlan = await _workPlanService.SelectingAsync(id);

            await _workPlanService.DeleteAsync(workPlan, cancellationToken);
        }
    }
}
