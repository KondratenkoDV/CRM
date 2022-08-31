using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UI.Models.WorkPlan;

namespace UI.Controllers
{
    public class WorkPlanController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private string _workPlanUrl = "https://localhost:44352/api/WorkPlan";

        public async Task<ActionResult> CreateWorkPlan(WorkPlan workPlan)
        {
            var workPlanInfo = new WorkPlan();

            var httpResponseMessage = await _httpClient.PostAsJsonAsync(_workPlanUrl, workPlan);

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

            var httpResponseMessage = await _httpClient.GetAsync($"{_workPlanUrl}/{id}");

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

            var httpResponseMessage = await _httpClient.GetAsync($"{_workPlanUrl}/{id}");

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
            var httpResponseMessage = await _httpClient.PutAsJsonAsync($"{_workPlanUrl}/{id}", workPlan);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Сhoice");
            }

            return View(workPlan);
        }

        public async Task<ActionResult> DeleteWorkPlan(int id)
        {
            var httpResponseMessage = await _httpClient.DeleteAsync($"{_workPlanUrl}/{id}");

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
