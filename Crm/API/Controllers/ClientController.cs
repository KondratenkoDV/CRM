using Microsoft.AspNetCore.Mvc;
using Application.Services.Client;
using Domain.Interfaces;
using API.DTOs.Client;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
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
            return Ok(await _clientService.AddAsync(
                createClientDto.Name,
                createClientDto.СodeOfTheCountry,
                createClientDto.RegionCode,
                createClientDto.SubscriberNumber,
                cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SelectingClientDto>> SelectingClient(int id)
        {
            var client = await _clientService.SelectingAsync(id);

            return Ok(new SelectingClientDto()
            {
                Id = client.Id,
                Name = client.Name,
                СodeOfTheCountry = client.СodeOfTheCountry,
                RegionCode = client.RegionCode,
                SubscriberNumber = client.SubscriberNumber
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(
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

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id, CancellationToken cancellationToken)
        {
            var client = await _clientService.SelectingAsync(id);

            await _clientService.DeleteAsync(client, cancellationToken);

            return NoContent();
        }
    }
}
