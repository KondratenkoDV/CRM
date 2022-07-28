using Microsoft.AspNetCore.Mvc;
using Application.Services.Contract;
using Domain.Interfaces;
using API.DTOs.Contract;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractController : ControllerBase
    {
        private readonly ContractService _contractService;

        public ContractController(IDbContext dbContext)
        {
            _contractService = new ContractService(dbContext);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateNewContract(
            CreateContractDto createContractDto,
            CancellationToken cancellationToken)
        {
            return Ok(await _contractService.AddAsync(
                createContractDto.Subject,
                createContractDto.Address,
                createContractDto.Price,
                createContractDto.ClientId,
                cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SelectingContractDto>> SelectingContract(int id)
        {
            var contract = await _contractService.SelectingAsync(id);

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
            var contract = await _contractService.SelectingAsync(id);

            await _contractService.UpdateAsync(
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
            var contract = await _contractService.SelectingAsync(id);

            await _contractService.DeleteAsync(contract, cancellationToken);

            return NoContent();
        }
    }
}
