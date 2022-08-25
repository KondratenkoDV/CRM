using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using UI.Models.WorkPlan;

namespace UI.Controllers
{
    public class WorkPlanController : Controller
    {
        string Baseurl = "https://localhost:44352/";

        public async Task<ActionResult> CreateWorkPlan(WorkPlan workPlan)
        {
            var WorkPlanInfo = new WorkPlan();

            var httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(Baseurl);

            httpClient.DefaultRequestHeaders.Clear();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await httpClient.PostAsJsonAsync("api/WorkPlan/", workPlan);

            if (Res.IsSuccessStatusCode)
            {
                var WorkPlanResponse = Res.Content.ReadAsStringAsync().Result;

                WorkPlanInfo.Id = JsonConvert.DeserializeObject<int>(WorkPlanResponse);
            }

            return View(WorkPlanInfo.Id);
        }

        public async Task<ActionResult> SelectWorkPlan(int id)
        {
            var WorkPlanInfo = new WorkPlan();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl);

                httpClient.DefaultRequestHeaders.Clear();

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await httpClient.GetAsync("api/WorkPlan/" + id);

                if (Res.IsSuccessStatusCode)
                {
                    var WorkPlanResponse = Res.Content.ReadAsStringAsync().Result;

                    WorkPlanInfo = JsonConvert.DeserializeObject<WorkPlan>(WorkPlanResponse);
                }

                return View(WorkPlanInfo);
            }
        }

        public async Task<ActionResult> UpdateWorkPlan(WorkPlan workPlan, int id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl);

                httpClient.DefaultRequestHeaders.Clear();

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await httpClient.PutAsJsonAsync("api/WorkPlan/" + id, workPlan);

                return View();
            }
        }

        public async Task<ActionResult> DeleteWorkPlan(int id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl);

                httpClient.DefaultRequestHeaders.Clear();

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await httpClient.DeleteAsync("api/WorkPlan/" + id);

                return View();
            }
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

        public IActionResult UpdateFormWorkPlanView(WorkPlan workPlan)
        {
            return View(workPlan);
        }
    }
}
