using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using API.DTOs.Contract;
using FluentValidation.Results;
using Domain.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractController : ControllerBase
    {
        private readonly IContractService _сontractService;

        private readonly IValidator<CreateContractDto> _createValidator;

        private readonly IValidator<UpdateContractDto> _updateValidator;

        public ContractController(
            IContractService сontractService,
            IValidator<CreateContractDto> createValidator,
            IValidator<UpdateContractDto> updateValidator)
        {
            _сontractService = сontractService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateNewContract(
            CreateContractDto createContractDto,
            CancellationToken cancellationToken)
        {
            ValidationResult result = await _createValidator.ValidateAsync(createContractDto);

            if (!result.IsValid)
            {
                return NotFound();
            }

            return Ok(await _сontractService.AddAsync(
                createContractDto.Subject,
                createContractDto.Address,
                createContractDto.Price,
                createContractDto.ClientId,
                cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SelectingContractDto>> SelectingContract(int id)
        {
            var contract = await _сontractService.SelectingAsync(id);

            return Ok(new SelectingContractDto()
            {
                Id = contract.Id,
                Subject = contract.Subject,
                Address = contract.Address,
                Price = contract.Price,
                ClientId = contract.ClientId
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContract(
            UpdateContractDto updateContractDto,
            int id,
            CancellationToken cancellationToken)
        {
            ValidationResult result = await _updateValidator.ValidateAsync(updateContractDto);

            if (!result.IsValid)
            {
                return NotFound();
            }

            var contract = await _сontractService.SelectingAsync(id);

            await _сontractService.UpdateAsync(
                contract,
                updateContractDto.NewSubject,
                updateContractDto.NewAddress,
                updateContractDto.NewPrice,
                updateContractDto.NewClientId,
                cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContract(int id, CancellationToken cancellationToken)
        {
            var contract = await _сontractService.SelectingAsync(id);

            await _сontractService.DeleteAsync(contract, cancellationToken);

            return NoContent();
        }
    }
}
