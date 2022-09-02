using Microsoft.AspNetCore.Mvc;
using API.DTOs.Position;
using FluentValidation;
using FluentValidation.Results;
using Domain.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateNewPosition(
            CreatePositionDto createPositionDto,
            CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _positionService.AddAsync(
                    createPositionDto.Name,
                    cancellationToken));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SelectingPositionDto>> SelectingPosition(int id)
        {
            try
            {
                var position = await _positionService.SelectingAsync(id);

                return Ok(new SelectingPositionDto()
                {
                    Id = position.Id,
                    Name = position.Name
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePosition(
            UpdatePositionDto updatePositionDto,
            int id,
            CancellationToken cancellationToken)
        {
            try
            {
                var position = await _positionService.SelectingAsync(id);

                await _positionService.UpdateAsync(
                    position,
                    updatePositionDto.NewName,
                    cancellationToken);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosition(int id, CancellationToken cancellationToken)
        {
            try
            {
                var position = await _positionService.SelectingAsync(id);

                await _positionService.DeleteAsync(position, cancellationToken);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
