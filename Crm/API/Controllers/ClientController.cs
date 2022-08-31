using Microsoft.AspNetCore.Mvc;
using API.DTOs.Client;
using Domain.Interfaces;
using Domain.Enum;

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
                if(int.TryParse(createClientDto.СodeOfTheCountry, out int result) ||
                    Enum.IsDefined(typeof(CodeOfTheCountry), result))
                {
                    CodeOfTheCountry codeOfTheCountry = (CodeOfTheCountry)Enum
                        .Parse(typeof(CodeOfTheCountry), createClientDto.СodeOfTheCountry);

                    return Ok(await _clientService.AddAsync(
                    createClientDto.Name,
                    codeOfTheCountry,
                    createClientDto.RegionCode,
                    createClientDto.SubscriberNumber,
                    cancellationToken));
                }

                return BadRequest();
            }
            catch
            {
                return StatusCodes.Status500InternalServerError;
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
                    Name = client.Name,
                    СodeOfTheCountry = ((int)client.СodeOfTheCountry).ToString(),
                    RegionCode = client.RegionCode,
                    SubscriberNumber = client.SubscriberNumber
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

                if (Enum.IsDefined(typeof(CodeOfTheCountry), updateClientDto.NewСodeOfTheCountry))
                {
                    CodeOfTheCountry newCodeOfTheCountry = (CodeOfTheCountry)Enum
                        .Parse(typeof(CodeOfTheCountry), updateClientDto.NewСodeOfTheCountry);

                    await _clientService.UpdateAsync(
                    client,
                    updateClientDto.NewName,
                    newCodeOfTheCountry,
                    updateClientDto.NewRegionCode,
                    updateClientDto.NewSubscriberNumber,
                    cancellationToken);

                    return Ok();
                }

                return BadRequest();
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
                        Name = client.Name,
                        СodeOfTheCountry = ((int)client.СodeOfTheCountry).ToString(),
                        RegionCode = client.RegionCode,
                        SubscriberNumber = client.SubscriberNumber
                    });
                }

                return Ok(clientsDto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
