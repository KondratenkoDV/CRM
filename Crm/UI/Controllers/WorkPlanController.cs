using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UI.Models.WorkPlan;

namespace UI.Controllers
{
    public class WorkPlanController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private string _baseUrl = "https://localhost:44352/";

        public async Task<ActionResult> CreateWorkPlan(WorkPlan workPlan)
        {
            var workPlanInfo = new WorkPlan();

            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.PostAsJsonAsync("api/WorkPlan/", workPlan);

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                var workPlanResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                workPlanInfo.Id = JsonConvert.DeserializeObject<int>(workPlanResponse);
            }

            return View(workPlanInfo.Id);
        }

        public async Task<ActionResult> SelectWorkPlan(int id)
        {
            var workPlanInfo = new WorkPlan();

            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.GetAsync("api/WorkPlan/" + id);

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                var workPlanResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                workPlanInfo = JsonConvert.DeserializeObject<WorkPlan>(workPlanResponse);
            }

            return View(workPlanInfo);
        }

        public async Task<ActionResult> UpdateWorkPlan(int id)
        {
            var workPlanInfo = new WorkPlan();

            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.GetAsync("api/WorkPlan/" + id);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var workPlanResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                workPlanInfo = JsonConvert.DeserializeObject<WorkPlan>(workPlanResponse);
            }

            return View(workPlanInfo);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateWorkPlan(WorkPlan workPlan, int id)
        {
            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.PutAsJsonAsync("api/WorkPlan/" + id, workPlan);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Сhoice");
            }

            return View(workPlan);
        }

        public async Task<ActionResult> DeleteWorkPlan(int id)
        {
            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.DeleteAsync("api/WorkPlan/" + id);

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Сhoice");
            }

            return View(httpResponseMessage.StatusCode);
        }

        public IActionResult SelectingWorkPlanView()
        {
            return View();
        }

        public IActionResult Сhoice()
        {
            return View();
        }

        public IActionResult CreateFormWorkPlanView()
        {
            return View();
        }
    }
}
