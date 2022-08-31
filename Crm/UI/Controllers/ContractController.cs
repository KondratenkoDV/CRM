using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UI.Models.Contract;

namespace UI.Controllers
{
    public class ContractController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private string _baseUrl = "https://localhost:44352/";

        public async Task<ActionResult> CreateContract(Contract contract)
        {
            var contractInfo = new Contract();

            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.PostAsJsonAsync("api/Contract/", contract);

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                var clientResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                contractInfo.Id = JsonConvert.DeserializeObject<int>(clientResponse);
            }

            return View(contractInfo.Id);
        }

        public async Task<ActionResult> AllContracts()
        {
            List<Contract> contractInfo = new List<Contract>();

            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.GetAsync("api/Contract");

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                var contractResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                contractInfo = JsonConvert.DeserializeObject<List<Contract>>(contractResponse);
            }

            return View(contractInfo);
        }

        public async Task<ActionResult> SelectContract(int id)
        {
            var contractInfo = new Contract();

            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.GetAsync("api/Contract/" + id);

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                var contractResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                contractInfo = JsonConvert.DeserializeObject<Contract>(contractResponse);
            }

            return View(contractInfo);
        }

        public async Task<ActionResult> UpdateContract(int id)
        {
            var contractInfo = new Contract();

            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.GetAsync("api/Contract/" + id);

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                var contractResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                contractInfo = JsonConvert.DeserializeObject<Contract>(contractResponse);
            }

            return View(contractInfo);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateContract(Contract contract, int id)
        {
            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.PutAsJsonAsync("api/Contract/" + id, contract);

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Сhoice");
            }

            return View(contract);
        }

        public async Task<ActionResult> DeleteContract(int id)
        {
            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.DeleteAsync("api/Contract/" + id);

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Сhoice");
            }

            return View(httpResponseMessage.StatusCode);
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
