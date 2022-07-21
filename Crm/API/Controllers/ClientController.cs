using Microsoft.AspNetCore.Mvc;
using Application.Services.Client;
using Domain.Interfaces;
using API.DTOs.Client;

namespace API.Controllers
{
    public class ClientController : Controller
    {
        private readonly ClientService _clientService;

        public ClientController(IDbContext dbContext)
        {
            _clientService = new ClientService(dbContext);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateNewClient(
            CreateClientDto createClientDto,
            CancellationToken cancellationToken)
        {
            return await _clientService.AddAsync(
                createClientDto.Name,
                createClientDto.СodeOfTheCountry,
                createClientDto.RegionCode,
                createClientDto.SubscriberNumber,
                cancellationToken);
        }

        [HttpGet]
        public async Task<ActionResult<SelectingClientDto>> SelectingClient(int id)
        {
            var client = await _clientService.SelectingAsync(id);

            return new SelectingClientDto()
            {
                Id = id,
                Name = client.Name,
                СodeOfTheCountry = client.СodeOfTheCountry,
                RegionCode = client.RegionCode,
                SubscriberNumber = client.SubscriberNumber
            };
        }

        [HttpPut]
        public async Task UpdateClient(
            UpdateClientDto updateClientDto,
            int id,
            CancellationToken cancellationToken)
        {
            var client = await _clientService.SelectingAsync(id);

            await _clientService.UpdateAsync(
                client,
                updateClientDto.NewName,
                updateClientDto.NewСodeOfTheCountry,
                updateClientDto.NewRegionCode,
                updateClientDto.NewSubscriberNumber,
                cancellationToken);
        }

        [HttpDelete]
        public async Task DeleteClient(int id, CancellationToken cancellationToken)
        {
            var client = await _clientService.SelectingAsync(id);

            await _clientService.DeleteAsync(client, cancellationToken);
        }
    }
}
