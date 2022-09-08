using Microsoft.AspNetCore.Mvc;
using API.DTOs.Client;
using Domain.Interfaces;
using API.DTOs.Enum;
using Domain.Enum;
using API.Helpers.Enum;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateNewClient(
            CreateClientDto createClientDto,
            CancellationToken cancellationToken)
        {
            try
            {
                return Ok(await _clientService.AddAsync(
                createClientDto.Name,
                createClientDto.СodeOfTheCountry,
                createClientDto.RegionCode,
                createClientDto.SubscriberNumber,
                cancellationToken));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SelectingClientDto>> SelectingClient(int id)
        {
            try
            {
                var client = await _clientService.SelectingAsync(id);

                return Ok(new SelectingClientDto()
                {
                    Id = client.Id,
                    SelectedName = client.Name,
                    SelectedСodeOfTheCountry = client.СodeOfTheCountry,
                    SelectedRegionCode = client.RegionCode,
                    SelectedSubscriberNumber = client.SubscriberNumber
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(
            UpdateClientDto updateClientDto,
            int id,
            CancellationToken cancellationToken)
        {
            try
            {
                var client = await _clientService.SelectingAsync(id);

                await _clientService.UpdateAsync(
                client,
                updateClientDto.NewName,
                updateClientDto.NewСodeOfTheCountry,
                updateClientDto.NewRegionCode,
                updateClientDto.NewSubscriberNumber,
                cancellationToken);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id, CancellationToken cancellationToken)
        {
            try
            {
                var client = await _clientService.SelectingAsync(id);

                await _clientService.DeleteAsync(client, cancellationToken);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SelectingClientDto>>> GetClients()
        {
            try
            {
                var clients = await _clientService.AllAsync();

                var clientsDto = new List<SelectingClientDto>();

                foreach(var client in clients)
                {
                    clientsDto.Add(new SelectingClientDto()
                    {
                        Id = client.Id,
                        SelectedName = client.Name,
                        SelectedСodeOfTheCountry = client.СodeOfTheCountry,
                        SelectedRegionCode = client.RegionCode,
                        SelectedSubscriberNumber = client.SubscriberNumber
                    });
                }

                return Ok(clientsDto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<EnumValueDto>> GetCodeOfTheCountry()
        {
            try
            {
                return Ok(EnumExtensions.GetValues<CodeOfTheCountry>());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
