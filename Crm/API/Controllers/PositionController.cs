using Microsoft.AspNetCore.Mvc;
using Application.Services.Position;
using Domain.Interfaces;
using API.DTOs.Position;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PositionController : ControllerBase
    {
        private readonly PositionService _positionService;

        public PositionController(PositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateNewPosition(
            CreatePositionDto createPositionDto,
            CancellationToken cancellationToken)
        {
            return Ok(await _positionService.AddAsync(
                createPositionDto.Name,
                cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SelectingPositionDto>> SelectingPosition(int id)
        {
            var position = await _positionService.SelectingAsync(id);

            return Ok(new SelectingPositionDto()
            {
                Id = position.Id,
                Name = position.Name
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePosition(
            UpdatePositionDto updatePositionDto,
            int id,
            CancellationToken cancellationToken)
        {
            var position = await _positionService.SelectingAsync(id);

            await _positionService.UpdateAsync(
                position,
                updatePositionDto.NewName,
                cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosition(int id, CancellationToken cancellationToken)
        {
            var position = await _positionService.SelectingAsync(id);

            await _positionService.DeleteAsync(position, cancellationToken);

            return NoContent();
        }
    }
}
