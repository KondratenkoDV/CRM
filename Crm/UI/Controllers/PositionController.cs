using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using UI.Helpers;
using UI.Models.Position;

namespace UI.Controllers
{
    public class PositionController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly ApiConfiguration _baseUrl;

        public PositionController(IHttpClientFactory httpClientFactory, IOptions<ApiConfiguration> options)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = options.Value;
        }

        public async Task<ActionResult> CreatePosition(CreatePositionModel createPositionModel)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var httpResponseMessage = await httpClient.PostAsJsonAsync($"{_baseUrl.Api}/Position/", createPositionModel);

                if(httpResponseMessage.IsSuccessStatusCode)
                {
                    var positionResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    int id = JsonConvert.DeserializeObject<int>(positionResponse);

                    return View(id);
                }

                return RedirectToAction("CreateFormPositionView");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<ActionResult> SelectPosition(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var selectingPositionModel = new SelectingPositionModel();

                var httpResponseMessage = await httpClient.GetAsync($"{_baseUrl.Api}/Position/{id}");

                if(httpResponseMessage.IsSuccessStatusCode)
                {
                    var positionResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    selectingPositionModel = JsonConvert.DeserializeObject<SelectingPositionModel>(positionResponse);
                }

                return View(selectingPositionModel);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<ActionResult> UpdatePosition(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var selectingPositionModel = new SelectingPositionModel();

                var httpResponseMessage = await httpClient.GetAsync($"{_baseUrl.Api}/Position/{id}");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var positionResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    selectingPositionModel = JsonConvert.DeserializeObject<SelectingPositionModel>(positionResponse);
                }

                return View(new UpdatePositionModel()
                {
                    NewName = selectingPositionModel.Name
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePosition(UpdatePositionModel updatePositionModel, int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var httpResponseMessage = await httpClient.PutAsJsonAsync($"{_baseUrl.Api}/Position/{id}", updatePositionModel);

                if(httpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Сhoice");
                }

                return View(updatePositionModel);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<ActionResult> DeletePosition(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var httpResponseMessage = await httpClient.DeleteAsync($"{_baseUrl.Api}/Position/{id}");

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

        public IActionResult SelectingPositionView()
        {
            return View();
        }

        public IActionResult Сhoice()
        {
            return View();
        }

        public IActionResult CreateFormPositionView()
        {
            return View();
        }
    }
}
