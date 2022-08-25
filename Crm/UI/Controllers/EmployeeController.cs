using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using UI.Models.Employee;

namespace UI.Controllers
{
    public class EmployeeController : Controller
    {
        string Baseurl = "https://localhost:44352/";

        public async Task<ActionResult> CreateEmployee(Employee employee)
        {
            var EmployeeInfo = new Employee();

            var httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(Baseurl);

            httpClient.DefaultRequestHeaders.Clear();

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage Res = await httpClient.PostAsJsonAsync("api/Employee/", employee);

            if (Res.IsSuccessStatusCode)
            {
                var ClientResponse = Res.Content.ReadAsStringAsync().Result;

                EmployeeInfo.Id = JsonConvert.DeserializeObject<int>(ClientResponse);
            }

            return View(EmployeeInfo.Id);
        }

        public async Task<ActionResult> AllEmployees()
        {
            List<Employee> EmployeeInfo = new List<Employee>();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl);

                httpClient.DefaultRequestHeaders.Clear();

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await httpClient.GetAsync("api/Employee/");

                if (Res.IsSuccessStatusCode)
                {
                    var EmployeeResponse = Res.Content.ReadAsStringAsync().Result;

                    EmployeeInfo = JsonConvert.DeserializeObject<List<Employee>>(EmployeeResponse);
                }

                return View(EmployeeInfo);
            }
        }

        public async Task<ActionResult> SelectEmployee(int id)
        {
            var EmployeeInfo = new Employee();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl);

                httpClient.DefaultRequestHeaders.Clear();

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await httpClient.GetAsync("api/Employee/" + id);

                if (Res.IsSuccessStatusCode)
                {
                    var ContractResponse = Res.Content.ReadAsStringAsync().Result;

                    EmployeeInfo = JsonConvert.DeserializeObject<Employee>(ContractResponse);
                }

                return View(EmployeeInfo);
            }
        }

        public async Task<ActionResult> UpdateEmployee(Employee employee, int id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl);

                httpClient.DefaultRequestHeaders.Clear();

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await httpClient.PutAsJsonAsync("api/Employee/" + id, employee);

                return View();
            }
        }

        public async Task<ActionResult> DeleteEmployee(int id)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(Baseurl);

                httpClient.DefaultRequestHeaders.Clear();

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage Res = await httpClient.DeleteAsync("api/Employee/" + id);

                return View();
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

        public IActionResult UpdateFormEmployeeView(Employee employee)
        {
            return View(employee);
        }
    }
}
