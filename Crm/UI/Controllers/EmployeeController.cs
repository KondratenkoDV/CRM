using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UI.Models.Employee;

namespace UI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private string _employeeUrl = "https://localhost:44352/api/Employee";

        public async Task<ActionResult> CreateEmployee(Employee employee)
        {
            var employeeInfo = new Employee();

            var httpResponseMessage = await _httpClient.PostAsJsonAsync(_employeeUrl, employee);

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

            var httpResponseMessage = await _httpClient.GetAsync(_employeeUrl);

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

            var httpResponseMessage = await _httpClient.GetAsync($"{_employeeUrl}/{id}");

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

            var httpResponseMessage = await _httpClient.GetAsync($"{_employeeUrl}/{id}");

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
            var httpResponseMessage = await _httpClient.PutAsJsonAsync($"{_employeeUrl}/{id}", employee);

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Сhoice");
            }

            return View(employee);
        }

        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var httpResponseMessage = await _httpClient.DeleteAsync($"{_employeeUrl}/{id}");

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
