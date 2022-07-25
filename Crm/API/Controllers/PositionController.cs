using Microsoft.AspNetCore.Mvc;
using Application.Services.Position;
using Domain.Interfaces;
using API.DTOs.Position;

namespace API.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class PositionController : ControllerBase
    {
        private readonly PositionService _positionService;

        public PositionController(IDbContext dbContext)
        {
            _positionService = new PositionService(dbContext);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateNewPosition(
            CreatePositionDto createPositionDto,
            CancellationToken cancellationToken)
        {
            return await _positionService.AddAsync(
                createPositionDto.Name,
                cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SelectingPositionDto>> SelectingPosition(int id)
        {
            var position = await _positionService.SelectingAsync(id);

            return new SelectingPositionDto()
            {
                Id = position.Id,
                Name = position.Name
            };
        }

        [HttpPut("{id}")]
        public async Task UpdatePosition(
            UpdatePositionDto updatePositionDto,
            int id,
            CancellationToken cancellationToken)
        {
            var position = await _positionService.SelectingAsync(id);

            await _positionService.UpdateAsync(
                position,
                updatePositionDto.NewName,
                cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task DeletePosition(int id, CancellationToken cancellationToken)
        {
            var position = await _positionService.SelectingAsync(id);

            await _positionService.DeleteAsync(position, cancellationToken);
        }
    }
}
