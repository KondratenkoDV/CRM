using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UI.Models.Client;

namespace UI.Controllers
{
    public class ClientController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly string _clientUrl;

        public ClientController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _clientUrl = configuration.GetConnectionString("API");
        }

        public async Task<ActionResult> CreateClient(CreateClientModel createClientModel)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var httpResponseMessage = await httpClient.PostAsJsonAsync($"{_clientUrl}/Client/", createClientModel);

                if(httpResponseMessage.IsSuccessStatusCode)
                {
                    var clientResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    int id = JsonConvert.DeserializeObject<int>(clientResponse);

                    return View(id);
                }

                return RedirectToAction("CreateFormClientView");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<ActionResult> AllClients()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                List<SelectingClientModel> selectingClientModel = new();

                var httpResponseMessage = await httpClient.GetAsync($"{_clientUrl}/Client/");

                if(httpResponseMessage.IsSuccessStatusCode)
                {
                    var clientResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    selectingClientModel = JsonConvert.DeserializeObject<List<SelectingClientModel>>(clientResponse);
                }

                return View(selectingClientModel);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<ActionResult> SelectClient(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var selectingClientModel = new SelectingClientModel();

                var httpResponseMessage = await httpClient.GetAsync($"{_clientUrl}/Client/{id}");

                if(httpResponseMessage.IsSuccessStatusCode)
                {
                    var clientResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    selectingClientModel = JsonConvert.DeserializeObject<SelectingClientModel>(clientResponse);
                }

                return View(selectingClientModel);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<ActionResult> UpdateClient(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var selectingClientModel = new SelectingClientModel();

                var httpResponseMessage = await httpClient.GetAsync($"{_clientUrl}/Client/{id}");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var clientResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    selectingClientModel = JsonConvert.DeserializeObject<SelectingClientModel>(clientResponse);
                }

                return View(new UpdateClientModel()
                {
                    NewName = selectingClientModel.SelectedName,
                    NewСodeOfTheCountry = selectingClientModel.SelectedСodeOfTheCountry,
                    NewRegionCode = selectingClientModel.SelectedRegionCode,
                    NewSubscriberNumber = selectingClientModel.SelectedSubscriberNumber
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateClient(UpdateClientModel updateClientModel, int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var httpResponseMessage = await httpClient.PutAsJsonAsync($"{_clientUrl}/Client/{id}", updateClientModel);

                if(httpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Сhoice");
                }

                return View(updateClientModel);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<ActionResult> DeleteClient(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var httpResponseMessage = await httpClient.DeleteAsync($"{_clientUrl}/Client/{id}");

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
