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

        private readonly IValidator<CreatePositionDto> _createValidator;

        private readonly IValidator<UpdatePositionDto> _updateValidator;

        public PositionController(
            IPositionService positionService,
            IValidator<CreatePositionDto> createValidator,
            IValidator<UpdatePositionDto> updateValidator)
        {
            _positionService = positionService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateNewPosition(
            CreatePositionDto createPositionDto,
            CancellationToken cancellationToken)
        {
            ValidationResult result = await _createValidator.ValidateAsync(createPositionDto);

            if (!result.IsValid)
            {
                return NotFound();
            }

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
            ValidationResult result = await _updateValidator.ValidateAsync(updatePositionDto);

            if (!result.IsValid)
            {
                return NotFound();
            }

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
