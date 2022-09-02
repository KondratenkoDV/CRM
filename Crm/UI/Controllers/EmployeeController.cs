using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UI.Models.Employee;

namespace UI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private string _employeeUrl;

        public EmployeeController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _employeeUrl = configuration.GetConnectionString("API");
        }

        public async Task<ActionResult> CreateEmployee(CreateEmployeeModel createEmployeeModel)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var httpResponseMessage = await httpClient.PostAsJsonAsync($"{_employeeUrl}/Employee/", createEmployeeModel);

                if(httpResponseMessage.IsSuccessStatusCode)
                {
                    var employeeResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    int id = JsonConvert.DeserializeObject<int>(employeeResponse);

                    return View(id);
                }

                return RedirectToAction("CreateFormEmployeeView");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<ActionResult> AllEmployees()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                List<SelectingEmployeeModel> selectingEmployeeModel = new();

                var httpResponseMessage = await httpClient.GetAsync($"{_employeeUrl}/Employee/");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var employeeResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    selectingEmployeeModel = JsonConvert.DeserializeObject<List<SelectingEmployeeModel>>(employeeResponse);
                }

                return View(selectingEmployeeModel);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<ActionResult> SelectEmployee(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var selectingEmployeeModel = new SelectingEmployeeModel();

                var httpResponseMessage = await httpClient.GetAsync($"{_employeeUrl}/Employee/{id}");

                if(httpResponseMessage.IsSuccessStatusCode)
                {
                    var employeeResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    selectingEmployeeModel = JsonConvert.DeserializeObject<SelectingEmployeeModel>(employeeResponse);
                }

                return View(selectingEmployeeModel);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<ActionResult> UpdateEmployee(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var selectingEmployeeModel = new SelectingEmployeeModel();

                var httpResponseMessage = await httpClient.GetAsync($"{_employeeUrl}/Employee/{id}");

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    var employeeResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                    selectingEmployeeModel = JsonConvert.DeserializeObject<SelectingEmployeeModel>(employeeResponse);
                }

                return View(new UpdateEmployeeModel()
                {
                    NewFirstName = selectingEmployeeModel.FirstName,
                    NewLastName = selectingEmployeeModel.LastName,
                    NewPositionId = selectingEmployeeModel.PositionId
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateEmployee(UpdateEmployeeModel updateEmployeeModel, int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var httpResponseMessage = await httpClient.PutAsJsonAsync($"{_employeeUrl}/Employee/{id}", updateEmployeeModel);

                if(httpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("Сhoice");
                }

                return View(updateEmployeeModel);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<ActionResult> DeleteEmployee(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();

                var httpResponseMessage = await httpClient.DeleteAsync($"{_employeeUrl}/Employee/{id}");

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

        public IActionResult SelectingEmployeeView()
        {
            return View();
        }

        public IActionResult Сhoice()
        {
            return View();
        }

        public IActionResult CreateFormEmployeeView()
        {
            return View();
        }
    }
}
