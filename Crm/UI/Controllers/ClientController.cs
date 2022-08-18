using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using UI.Models;

namespace UI.Controllers
{
    public class ClientController : Controller
    {
        string Baseurl = "https://localhost:44352/";

        public async Task<ActionResult> Index()
        {
            List<Client> ClientInfo = new List<Client>();

            using(var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl);

                httpClient.DefaultRequestHeaders.Clear();

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await httpClient.GetAsync("api/Client");

                if(Res.IsSuccessStatusCode)
                {
                    var ClientResponse = Res.Content.ReadAsStringAsync().Result;

                    ClientInfo = JsonConvert.DeserializeObject<List<Client>>(ClientResponse);
                }

                return View(ClientInfo);
            }
        }
    }
}
