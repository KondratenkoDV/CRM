using Microsoft.AspNetCore.Mvc;
using Application.Services.Client;
using API.DTOs.Client;
using FluentValidation;
using FluentValidation.Results;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ClientService _clientService;

        private readonly IValidator<CreateClientDto> _createValidator;

        private readonly IValidator<UpdateClientDto> _updateValidator;

        public ClientController(
            ClientService clientService,
            IValidator<CreateClientDto> createValidator,
            IValidator<UpdateClientDto> updateValidator)
        {
            _clientService = clientService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateNewClient(
            CreateClientDto createClientDto,
            CancellationToken cancellationToken)
        {
            ValidationResult result = await _createValidator.ValidateAsync(createClientDto);

            if (!result.IsValid)
            {
                return NotFound();
            }
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
            ValidationResult result = await _updateValidator.ValidateAsync(updateClientDto);

            if (!result.IsValid)
            {
                return NotFound();
            }

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
