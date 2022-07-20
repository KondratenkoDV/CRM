using Microsoft.AspNetCore.Mvc;
using Application.Services.Contract;
using API.Helpers;
using API.Models;

namespace API.Controllers
{
    public class ContractController : Controller
    {
        [HttpPost]
        public async Task<int> CreateNewContract(
            ContractModel contractModel,
            CancellationToken cancellationToken)
        {
            var contractService = new ContractService(CompoundDb.Compound());

            return await contractService.AddAsync(
                contractModel.Subject,
                contractModel.Address,
                contractModel.Price,
                contractModel.ClientId,
                cancellationToken);
        }

        [HttpPost]
        public async Task<ContractModel> SelectingContract(int id)
        {
            var contractService = new ContractService(CompoundDb.Compound());

            var contract = await contractService.SelectingAsync(id);

            return new ContractModel()
            {
                Id = contract.Id,
                Subject = contract.Subject,
                Address = contract.Address,
                Price = contract.Price,
                ClientId = contract.ClientId
            };
        }

        [HttpPost]
        public async Task UpdateContract(
            ContractModel contractModel,
            int id,
            CancellationToken cancellationToken)
        {
            var contractService = new ContractService(CompoundDb.Compound());

            var contract = await contractService.SelectingAsync(id);

            await contractService.UpdateAsync(
                contract,
                contractModel.Subject,
                contractModel.Address,
                contractModel.Price,
                contractModel.ClientId,
                cancellationToken);
        }

        [HttpPost]
        public async Task DeleteContract(int id, CancellationToken cancellationToken)
        {
            var contractService = new ContractService(CompoundDb.Compound());

            var contract = await contractService.SelectingAsync(id);

            await contractService.DeleteAsync(contract, cancellationToken);
        }
    }
}
