using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UI.Models.Contract;

namespace UI.Controllers
{
    public class ContractController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private string _contractUrl;

        public ContractController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _contractUrl = configuration.GetConnectionString("API");
        }

        public async Task<ActionResult> CreateContract(CreateContractModel createContractModel)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var httpResponseMessage = await httpClient.PostAsJsonAsync($"{_contractUrl}/Contract/", createContractModel);

                if(httpResponseMessage.IsSuccessStatusCode)
                {
                    var clientResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    int id = JsonConvert.DeserializeObject<int>(clientResponse);

                    return View(id);
                }

                return RedirectToAction("CreateFormContractView");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<ActionResult> AllContracts()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                List<SelectingContractModel> selectingContractModel = new();

                var httpResponseMessage = await httpClient.GetAsync($"{_contractUrl}/Contract/");

                if(httpResponseMessage.IsSuccessStatusCode)
                {
                    var contractResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    selectingContractModel = JsonConvert.DeserializeObject<List<SelectingContractModel>>(contractResponse);
                }

                return View(selectingContractModel);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<ActionResult> SelectContract(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var selectingContractModel = new SelectingContractModel();

                var httpResponseMessage = await httpClient.GetAsync($"{_contractUrl}/Contract/{id}");

                if(httpResponseMessage.IsSuccessStatusCode)
                {
                    var contractResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    selectingContractModel = JsonConvert.DeserializeObject<SelectingContractModel>(contractResponse);
                }

                return View(selectingContractModel);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<ActionResult> UpdateContract(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var selectingContractModel = new SelectingContractModel();

                var httpResponseMessage = await httpClient.GetAsync($"{_contractUrl}/Contract/{id}");

                if(httpResponseMessage.IsSuccessStatusCode)
                {
                    var contractResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    selectingContractModel = JsonConvert.DeserializeObject<SelectingContractModel>(contractResponse);
                }

                return View(new UpdateContractModel()
                {
                    NewSubject = selectingContractModel.Subject,
                    NewAddress = selectingContractModel.Address,
                    NewPrice = selectingContractModel.Price,
                    NewClientId = selectingContractModel.ClientId
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateContract(UpdateContractModel updateContractModel, int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var httpResponseMessage = await httpClient.PutAsJsonAsync($"{_contractUrl}/Contract/{id}", updateContractModel);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Сhoice");
                }

                return View(updateContractModel);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<ActionResult> DeleteContract(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var httpResponseMessage = await httpClient.DeleteAsync($"{_contractUrl}/Contract/{id}");

                if(httpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Сhoice");
                }

                return View(httpResponseMessage.StatusCode);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
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
