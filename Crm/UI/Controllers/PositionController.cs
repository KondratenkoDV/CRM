using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using UI.Models.Position;

namespace UI.Controllers
{
    public class PositionController : Controller
    {
        string Baseurl = "https://localhost:44352/";

        public async Task<ActionResult> CreatePosition(Position position)
        {
            var PositionInfo = new Position();

            var httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(Baseurl);

            httpClient.DefaultRequestHeaders.Clear();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await httpClient.PostAsJsonAsync("api/Position/", position);

            if (Res.IsSuccessStatusCode)
            {
                var PositionResponse = Res.Content.ReadAsStringAsync().Result;

                PositionInfo.Id = JsonConvert.DeserializeObject<int>(PositionResponse);
            }

            return View(PositionInfo.Id);
        }

        public async Task<ActionResult> SelectPosition(int id)
        {
            var PositionInfo = new Position();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl);

                httpClient.DefaultRequestHeaders.Clear();

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await httpClient.GetAsync("api/Position/" + id);

                if (Res.IsSuccessStatusCode)
                {
                    var PositionResponse = Res.Content.ReadAsStringAsync().Result;

                    PositionInfo = JsonConvert.DeserializeObject<Position>(PositionResponse);
                }

                return View(PositionInfo);
            }
        }

        public async Task<ActionResult> UpdatePosition(Position position, int id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl);

                httpClient.DefaultRequestHeaders.Clear();

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await httpClient.PutAsJsonAsync("api/Position/" + id, position);

                return View();
            }
        }

        public async Task<ActionResult> DeletePosition(int id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl);

                httpClient.DefaultRequestHeaders.Clear();

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await httpClient.DeleteAsync("api/Position/" + id);

                return View();
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

        public IActionResult UpdateFormPositionView(Position position)
        {
            return View(position);
        }
    }
}
