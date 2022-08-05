using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using API.DTOs.WorkPlan;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkPlanController : ControllerBase
    {
        private readonly IWorkPlanService _workPlanService;

        public WorkPlanController(IWorkPlanService workPlanService)
        {
            _workPlanService = workPlanService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateNewWorkPlan(
            CreateWorkPlanDto createWorkPlanDto,
            CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _workPlanService.AddAsync(
                    createWorkPlanDto.DateStart,
                    createWorkPlanDto.DateFinish,
                    createWorkPlanDto.ContractId,
                    cancellationToken));
            }
            catch
            {
                return StatusCodes.Status500InternalServerError;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SelectingWorkPlanDto>> SelectingWorkPlan(int id)
        {
            try
            {
                var workPlan = await _workPlanService.SelectingAsync(id);

                return Ok(new SelectingWorkPlanDto()
                {
                    Id = workPlan.Id,
                    DateStart = workPlan.DateStart,
                    DateFinish = workPlan.DateFinish,
                    ContractId = workPlan.ContractId
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkPlan(
            UpdateWorkPlanDto updateWorkPlanDto,
            int id,
            CancellationToken cancellationToken)
        {
            try
            {
                var workPlan = await _workPlanService.SelectingAsync(id);

                await _workPlanService.UpdateAsync(
                    workPlan,
                    updateWorkPlanDto.NewDateStart,
                    updateWorkPlanDto.NewDateFinish,
                    updateWorkPlanDto.NewContractId,
                    cancellationToken);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkPlan(int id, CancellationToken cancellationToken)
        {
            try
            {
                var workPlan = await _workPlanService.SelectingAsync(id);

                await _workPlanService.DeleteAsync(workPlan, cancellationToken);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
