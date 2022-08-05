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

        public ContractController(IContractService сontractService)
        {
            _сontractService = сontractService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateNewContract(
            CreateContractDto createContractDto,
            CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _сontractService.AddAsync(
                    createContractDto.Subject,
                    createContractDto.Address,
                    createContractDto.Price,
                    createContractDto.ClientId,
                    cancellationToken));
            }
            catch
            {
                return StatusCodes.Status500InternalServerError;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SelectingContractDto>> SelectingContract(int id)
        {
            try
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
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContract(
            UpdateContractDto updateContractDto,
            int id,
            CancellationToken cancellationToken)
        {
            try
            {
                var contract = await _сontractService.SelectingAsync(id);

                await _сontractService.UpdateAsync(
                    contract,
                    updateContractDto.NewSubject,
                    updateContractDto.NewAddress,
                    updateContractDto.NewPrice,
                    updateContractDto.NewClientId,
                    cancellationToken);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContract(int id, CancellationToken cancellationToken)
        {
            try
            {
                var contract = await _сontractService.SelectingAsync(id);

                await _сontractService.DeleteAsync(contract, cancellationToken);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
