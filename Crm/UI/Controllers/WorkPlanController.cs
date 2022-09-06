using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using UI.Helpers;
using UI.Models.WorkPlan;

namespace UI.Controllers
{
    public class WorkPlanController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly ApiConfiguration _baseUrl;

        public WorkPlanController(IHttpClientFactory httpClientFactory, IOptions<ApiConfiguration> options)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = options.Value;
        }

        public async Task<ActionResult> CreateWorkPlan(CreateWorkPlanModel createWorkPlanModel)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var httpResponseMessage = await httpClient.PostAsJsonAsync($"{_baseUrl.Api}/WorkPlan/", createWorkPlanModel);

                if(httpResponseMessage.IsSuccessStatusCode)
                {
                    var workPlanResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    int id = JsonConvert.DeserializeObject<int>(workPlanResponse);

                    return View(id);
                }

                return RedirectToAction("CreateFormWorkPlanView");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<ActionResult> SelectWorkPlan(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var selectingWorkPlanModel = new SelectingWorkPlanModel();

                var httpResponseMessage = await httpClient.GetAsync($"{_baseUrl.Api}/WorkPlan/{id}");

                if(httpResponseMessage.IsSuccessStatusCode)
                {
                    var workPlanResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    selectingWorkPlanModel = JsonConvert.DeserializeObject<SelectingWorkPlanModel>(workPlanResponse);
                }

                return View(selectingWorkPlanModel);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<ActionResult> UpdateWorkPlan(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var selectingWorkPlanModel = new SelectingWorkPlanModel();

                var httpResponseMessage = await httpClient.GetAsync($"{_baseUrl.Api}/WorkPlan/{id}");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var workPlanResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    selectingWorkPlanModel = JsonConvert.DeserializeObject<SelectingWorkPlanModel>(workPlanResponse);
                }

                return View(new UpdateWorkPlanModel()
                {
                    NewDateStart = selectingWorkPlanModel.DateStart,
                    NewDateFinish = selectingWorkPlanModel.DateFinish,
                    NewContractId = selectingWorkPlanModel.ContractId
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateWorkPlan(UpdateWorkPlanModel updateWorkPlanModel, int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var httpResponseMessage = await httpClient.PutAsJsonAsync($"{_baseUrl.Api}/WorkPlan/{id}", updateWorkPlanModel);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Сhoice");
                }

                return View(updateWorkPlanModel);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<ActionResult> DeleteWorkPlan(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var httpResponseMessage = await httpClient.DeleteAsync($"{_baseUrl.Api}/WorkPlan/{id}");

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
