using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using UI.Models.Client;

namespace UI.Controllers
{
    public class ClientController : Controller
    {
        string Baseurl = "https://localhost:44352/";

        public async Task<ActionResult> CreateClient(Client client)
        {
            Client ClientInfo = new Client();

            var httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(Baseurl);

            httpClient.DefaultRequestHeaders.Clear();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await httpClient.PostAsJsonAsync("api/Contract/", client);

            if (Res.IsSuccessStatusCode)
            {
                var ClientResponse = Res.Content.ReadAsStringAsync().Result;

                ClientInfo.Id = JsonConvert.DeserializeObject<int>(ClientResponse);
            }

            return View(ClientInfo.Id);
        }

        public async Task<ActionResult> AllClients()
        {
            List<Client> ClientInfo = new List<Client>();

            using(var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl);

                httpClient.DefaultRequestHeaders.Clear();

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await httpClient.GetAsync("api/Client/");

                if(Res.IsSuccessStatusCode)
                {
                    var ClientResponse = Res.Content.ReadAsStringAsync().Result;

                    ClientInfo = JsonConvert.DeserializeObject<List<Client>>(ClientResponse);
                }

                return View(ClientInfo);
            }
        }

        public async Task<ActionResult> SelectClient(int id)
        {
            var ClientInfo = new Client();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl);

                httpClient.DefaultRequestHeaders.Clear();

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await httpClient.GetAsync("api/Client/" + id);

                if (Res.IsSuccessStatusCode)
                {
                    var ClientResponse = Res.Content.ReadAsStringAsync().Result;

                    ClientInfo = JsonConvert.DeserializeObject<Client>(ClientResponse);
                }

                return View(ClientInfo);
            }
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
