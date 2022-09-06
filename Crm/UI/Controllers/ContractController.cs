using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using UI.Helpers;
using UI.Models.Contract;

namespace UI.Controllers
{
    public class ContractController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly ApiConfiguration _baseUrl;

        public ContractController(IHttpClientFactory httpClientFactory, IOptions<ApiConfiguration> options)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = options.Value;
        }

        public async Task<ActionResult> CreateContract(CreateContractModel createContractModel)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var httpResponseMessage = await httpClient.PostAsJsonAsync($"{_baseUrl.Api}/Contract/", createContractModel);

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

                var httpResponseMessage = await httpClient.GetAsync($"{_baseUrl.Api}/Contract/");

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

                var httpResponseMessage = await httpClient.GetAsync($"{_baseUrl.Api}/Contract/{id}");

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

                var httpResponseMessage = await httpClient.GetAsync($"{_baseUrl.Api}/Contract/{id}");

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

                var httpResponseMessage = await httpClient.PutAsJsonAsync($"{_baseUrl.Api}/Contract/{id}", updateContractModel);

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

                var httpResponseMessage = await httpClient.DeleteAsync($"{_baseUrl.Api}/Contract/{id}");

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
