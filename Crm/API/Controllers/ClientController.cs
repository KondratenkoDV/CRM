using Microsoft.AspNetCore.Mvc;
using Application.Services.Client;
using API.Helpers;
using API.Models;

namespace API.Controllers
{
    public class ClientController : Controller
    {
        [HttpPost]
        public async Task<int> CreateNewClient(
            ClientModel clientModel,
            CancellationToken cancellationToken)
        {
            var clientService = new ClientService(CompoundDb.Compound());

            return await clientService.AddAsync(
                clientModel.Name,
                clientModel.СodeOfTheCountry,
                clientModel.RegionCode,
                clientModel.SubscriberNumber,
                cancellationToken);
        }

        [HttpPost]
        public async Task<ClientModel> SelectingClient(int id)
        {
            var clientService = new ClientService(CompoundDb.Compound());

            var client = await clientService.SelectingAsync(id);

            return new ClientModel()
            {                
                Id = client.Id,
                Name = client.Name,
                СodeOfTheCountry = client.СodeOfTheCountry,
                RegionCode = client.RegionCode,
                SubscriberNumber = client.SubscriberNumber,
            };
        }

        [HttpPost]
        public async Task UpdateClient(
            ClientModel clientModel,
            int id,
            CancellationToken cancellationToken)
        {
            var clientService = new ClientService(CompoundDb.Compound());

            var client = await clientService.SelectingAsync(id);

            await clientService.UpdateAsync(
                client,
                clientModel.Name,
                clientModel.СodeOfTheCountry,
                clientModel.RegionCode,
                clientModel.SubscriberNumber,
                cancellationToken);
        }

        [HttpPost]
        public async Task DeleteClient(int id, CancellationToken cancellationToken)
        {
            var clientService = new ClientService(CompoundDb.Compound());

            var client = await clientService.SelectingAsync(id);

            await clientService.DeleteAsync(client, cancellationToken);
        }
    }
}
