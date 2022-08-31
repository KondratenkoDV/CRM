using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UI.Models.Client;

namespace UI.Controllers
{
    public class ClientController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private string _baseUrl = "https://localhost:44352/";

        public async Task<ActionResult> CreateClient(Client client)
        {
            var clientInfo = new Client();

            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.PostAsJsonAsync("api/Client/", client);

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

            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.GetAsync("api/Client/");

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

            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.GetAsync("api/Client/" + id);

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

            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.GetAsync("api/Client/" + id);

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
            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.PutAsJsonAsync("api/Client/" + id, client);

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Сhoice");
            }

            return View(client);
        }

        public async Task<ActionResult> DeleteClient(int id)
        {
            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.DeleteAsync("api/Client/" + id);

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
