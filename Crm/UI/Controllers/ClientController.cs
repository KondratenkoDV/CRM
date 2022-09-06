using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using UI.Helpers;
using UI.Models.Client;
using UI.Models.Enum;

namespace UI.Controllers
{
    public class ClientController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly ApiConfiguration _baseUrl;

        public ClientController(IHttpClientFactory httpClientFactory, IOptions<ApiConfiguration> options)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = options.Value;
        }

        public async Task<ActionResult> CreateClient(CreateClientModel createClientModel)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var httpResponseMessage = await httpClient.PostAsJsonAsync($"{_baseUrl.Api}/Client/", createClientModel);

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

                var httpResponseMessage = await httpClient.GetAsync($"{_baseUrl.Api}/Client/");

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

                var httpResponseMessage = await httpClient.GetAsync($"{_baseUrl.Api}/Client/{id}");

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

                var httpResponseMessage = await httpClient.GetAsync($"{_baseUrl.Api}/Client/{id}");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var clientResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    selectingClientModel = JsonConvert.DeserializeObject<SelectingClientModel>(clientResponse);
                }

                var selectingEnumValueModel = new List<EnumValueModel>();

                httpResponseMessage = await httpClient.GetAsync($"{_baseUrl.Api}/EnumValue/");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var enumValueResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    selectingEnumValueModel = JsonConvert.DeserializeObject<List<EnumValueModel>>(enumValueResponse);
                }

                ViewBag.EnumValueModel = selectingEnumValueModel.Select(e => new SelectListItem()
                {
                    Text = e.Name
                });

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

                var httpResponseMessage = await httpClient.PutAsJsonAsync($"{_baseUrl.Api}/Client/{id}", updateClientModel);

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

                var httpResponseMessage = await httpClient.DeleteAsync($"{_baseUrl.Api}/Client/{id}");

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

        public async Task<IActionResult> CreateFormClientView()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var selectingEnumValueModel = new List<EnumValueModel>();

                var httpResponseMessage = await httpClient.GetAsync($"{_baseUrl.Api}/EnumValue/");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var enumValueResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    selectingEnumValueModel = JsonConvert.DeserializeObject<List<EnumValueModel>>(enumValueResponse);
                }

                ViewBag.EnumValueModel = selectingEnumValueModel.Select(e => new SelectListItem()
                {
                    Text = e.Name
                });

                return View();
            }
            catch (Exception ex)
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
    }
}
