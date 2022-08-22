using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using UI.Models.Contract;

namespace UI.Controllers
{
    public class ContractController : Controller
    {
        string Baseurl = "https://localhost:44352/";

        public async Task<ActionResult> CreateContract(Contract contract)
        {
            var ContractInfo = new Contract();

            var httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(Baseurl);

            httpClient.DefaultRequestHeaders.Clear();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await httpClient.PostAsJsonAsync("api/Contract/", contract);

            if (Res.IsSuccessStatusCode)
            {
                var ClientResponse = Res.Content.ReadAsStringAsync().Result;

                ContractInfo.Id = JsonConvert.DeserializeObject<int>(ClientResponse);
            }

            return View(ContractInfo.Id);
        }

        public async Task<ActionResult> SelectContract(int id)
        {
            var ContractInfo = new Contract();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl);

                httpClient.DefaultRequestHeaders.Clear();

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await httpClient.GetAsync("api/Contract/" + id);

                if (Res.IsSuccessStatusCode)
                {
                    var ContractResponse = Res.Content.ReadAsStringAsync().Result;

                    ContractInfo = JsonConvert.DeserializeObject<Contract>(ContractResponse);
                }

                return View(ContractInfo);
            }
        }

        public IActionResult SelectingContractView()
        {
            return View();
        }

        public IActionResult Сhoice()
        {
            return View();
        }

        public IActionResult CreateFormContractView()
        {
            return View();
        }
    }
}
