using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UI.Models.Position;

namespace UI.Controllers
{
    public class PositionController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private string _baseUrl = "https://localhost:44352/";

        public async Task<ActionResult> CreatePosition(Position position)
        {
            var positionInfo = new Position();

            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.PostAsJsonAsync("api/Position/", position);

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                var positionResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                positionInfo.Id = JsonConvert.DeserializeObject<int>(positionResponse);
            }

            return View(positionInfo.Id);
        }

        public async Task<ActionResult> SelectPosition(int id)
        {
            var positionInfo = new Position();

            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.GetAsync("api/Position/" + id);

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                var positionResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                positionInfo = JsonConvert.DeserializeObject<Position>(positionResponse);
            }

            return View(positionInfo);            
        }

        public async Task<ActionResult> UpdatePosition(int id)
        {
            var positionInfo = new Position();

            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.GetAsync("api/Position/" + id);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var positionResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                positionInfo = JsonConvert.DeserializeObject<Position>(positionResponse);
            }

            return View(positionInfo);
        }

        [HttpPost]
        public async Task<ActionResult> UpdatePosition(Position position, int id)
        {
            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.PutAsJsonAsync("api/Position/" + id, position);

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Сhoice");
            }

            return View(position);
        }

        public async Task<ActionResult> DeletePosition(int id)
        {
            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.DeleteAsync("api/Position/" + id);

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Сhoice");
            }

            return View(httpResponseMessage.StatusCode);
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
