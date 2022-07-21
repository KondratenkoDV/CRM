using Microsoft.AspNetCore.Mvc;
using Application.Services.Contract;
using Domain.Interfaces;
using API.DTOs.Contract;

namespace API.Controllers
{
    public class ContractController : Controller
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
            return await _contractService.AddAsync(
                createContractDto.Subject,
                createContractDto.Address,
                createContractDto.Price,
                createContractDto.ClientId,
                cancellationToken);
        }

        [HttpGet]
        public async Task<ActionResult<SelectingContractDto>> SelectingContract(int id)
        {
            var contract = await _contractService.SelectingAsync(id);

            return new SelectingContractDto()
            {
                Id = contract.Id,
                Subject = contract.Subject,
                Address = contract.Address,
                Price = contract.Price,
                ClientId = contract.ClientId
            };
        }

        [HttpPut]
        public async Task UpdateContract(
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
        }

        [HttpDelete]
        public async Task DeleteContract(int id, CancellationToken cancellationToken)
        {
            var contract = await _contractService.SelectingAsync(id);

            await _contractService.DeleteAsync(contract, cancellationToken);
        }
    }
}
