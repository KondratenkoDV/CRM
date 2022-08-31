using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UI.Models.Employee;

namespace UI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private string _baseUrl = "https://localhost:44352/";

        public async Task<ActionResult> CreateEmployee(Employee employee)
        {
            var employeeInfo = new Employee();

            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.PostAsJsonAsync("api/Employee/", employee);

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                var employeeResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                employeeInfo.Id = JsonConvert.DeserializeObject<int>(employeeResponse);
            }

            return View(employeeInfo.Id);
        }

        public async Task<ActionResult> AllEmployees()
        {
            List<Employee> employeeInfo = new List<Employee>();

            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.GetAsync("api/Employee/");

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                var employeeResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                employeeInfo = JsonConvert.DeserializeObject<List<Employee>>(employeeResponse);
            }

            return View(employeeInfo);
        }

        public async Task<ActionResult> SelectEmployee(int id)
        {
            var employeeInfo = new Employee();

            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.GetAsync("api/Employee/" + id);

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                var employeeResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                employeeInfo = JsonConvert.DeserializeObject<Employee>(employeeResponse);
            }

            return View(employeeInfo);
        }

        public async Task<ActionResult> UpdateEmployee(int id)
        {
            var employeeInfo = new Employee();

            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.GetAsync("api/Employee/" + id);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                var employeeResponse = httpResponseMessage.Content.ReadAsStringAsync().Result;

                employeeInfo = JsonConvert.DeserializeObject<Employee>(employeeResponse);
            }

            return View(employeeInfo);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateEmployee(Employee employee, int id)
        {
            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.PutAsJsonAsync("api/Employee/" + id, employee);

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Сhoice");
            }

            return View(employee);
        }

        public async Task<ActionResult> DeleteEmployee(int id)
        {
            _httpClient.BaseAddress = new Uri(_baseUrl);

            var httpResponseMessage = await _httpClient.DeleteAsync("api/Employee/" + id);

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Сhoice");
            }

            return View(httpResponseMessage.StatusCode);
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
