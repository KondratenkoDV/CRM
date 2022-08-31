using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UI.Models.Client;

namespace UI.Controllers
{
    public class ClientController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private string _clientUrl = "https://localhost:44352/api/Client";

        public async Task<ActionResult> CreateClient(Client client)
        {
            var clientInfo = new Client();

            var httpResponseMessage = await _httpClient.PostAsJsonAsync(_clientUrl, client);

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                var clientResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                clientInfo.Id = JsonConvert.DeserializeObject<int>(clientResponse);
            }

            return View(clientInfo.Id);
        }

        public async Task<ActionResult> AllClients()
        {
            List<Client> clientInfo = new List<Client>();

            var httpResponseMessage = await _httpClient.GetAsync(_clientUrl);

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                var clientResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                clientInfo = JsonConvert.DeserializeObject<List<Client>>(clientResponse);
            }

            return View(clientInfo);
        }

        public async Task<ActionResult> SelectClient(int id)
        {
            var clientInfo = new Client();

            var httpResponseMessage = await _httpClient.GetAsync($"{_clientUrl}/{id}");

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                var clientResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                clientInfo  = JsonConvert.DeserializeObject<Client>(clientResponse);
            }

            return View(clientInfo);
        }

        public async Task<ActionResult> UpdateClient(int id)
        {
            var clientInfo = new Client();

            var httpResponseMessage = await _httpClient.GetAsync($"{_clientUrl}/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var clientResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                clientInfo = JsonConvert.DeserializeObject<Client>(clientResponse);
            }

            return View(clientInfo);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateClient(Client client, int id)
        {
            var httpResponseMessage = await _httpClient.PutAsJsonAsync($"{_clientUrl}/{id}", client);

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Сhoice");
            }

            return View(client);
        }

        public async Task<ActionResult> DeleteClient(int id)
        {
            var httpResponseMessage = await _httpClient.DeleteAsync($"{_clientUrl}/{id}");

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Сhoice");
            }

            return View(httpResponseMessage.StatusCode);
        }

        public IActionResult Сhoice()
        {
            return View();
        }

        public IActionResult SelectingClientView()
        {
            return View();
        }

        public IActionResult CreateFormClientView()
        {
            return View();
        }
    }
}
