using Microsoft.Extensions.Options;
using Moq.Protected;
using Moq;
using System;
using UI.Helpers;
using UI.Models.Employee;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UI.Controllers;
using Newtonsoft.Json;

namespace UI.Tests.Controllers
{
    public class EmployeeControllerTests
    {
        private Mock<HttpMessageHandler> GetMockHttpMessageHandler(HttpResponseMessage response)
        {
            var httpMessageHandler = new Mock<HttpMessageHandler>();

            httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(response);

            return httpMessageHandler;
        }

        private Mock<IHttpClientFactory> GetMockIHttpClientFactory(Mock<HttpMessageHandler> httpMessageHandler)
        {
            var httpClient = new HttpClient(httpMessageHandler.Object);

            var mockClient = new Mock<IHttpClientFactory>();

            mockClient.Setup(c => c.CreateClient(It.IsAny<string>())).Returns(httpClient);

            return mockClient;
        }

        private Mock<IOptions<ApiConfiguration>> GetMockIOptions()
        {
            var apiConfiguration = new ApiConfiguration()
            {
                Api = "http://Tests.ua"
            };

            var mockOptions = new Mock<IOptions<ApiConfiguration>>();

            mockOptions.Setup(o => o.Value).Returns(apiConfiguration);

            return mockOptions;
        }

        private SelectingEmployeeModel GetSelectingEmployeeModel()
        {
            return new SelectingEmployeeModel()
            {
                Id = 1,
                FirstName = "FirstName",
                LastName = "LastName",
                PositionId = 0
            };
        }

        [Fact]
        public async void Task_When_CreateEmployee_Expect_EmployeeWasCreated()
        {
            // Arrange

            var expected = 1;

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent($"{expected}")
            };

            var mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

            var mockOptions = GetMockIOptions();

            var employeeController = new EmployeeController(mockClient.Object, mockOptions.Object);

            var createEmployeeModel = new CreateEmployeeModel()
            {
                FirstName = "FirstName",
                LastName = "LastName",
                PositionId = 0
            };

            // Act

            var result = await employeeController.CreateEmployee(createEmployeeModel) as ViewResult;

            // Assert

            Assert.Equal(expected, result.Model);
        }

        [Fact]
        public async void Task_When_AllEmployees_Expect_EmployeesWasSelected()
        {
            // Arrange

            var expected = new List<SelectingEmployeeModel>()
            {
                GetSelectingEmployeeModel()
            };

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expected))
            };

            var mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

            var mockOptions = GetMockIOptions();

            var employeeController = new EmployeeController(mockClient.Object, mockOptions.Object);

            // Act

            var result = await employeeController.AllEmployees() as ViewResult;

            // Assert

            Assert.NotEmpty(result.Model as IEnumerable<SelectingEmployeeModel>);
        }

        [Fact]
        public async void Task_When_SelectEmployee_Expect_EmployeeWasSelected()
        {
            // Arrange

            var expected = GetSelectingEmployeeModel();

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expected))
            };

            var mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

            var mockOptions = GetMockIOptions();

            var employeeController = new EmployeeController(mockClient.Object, mockOptions.Object);

            // Act

            var result = await employeeController.SelectEmployee(expected.Id) as ViewResult;

            var actual = result.Model as SelectingEmployeeModel;

            // Assert

            Assert.Equal(expected.FirstName, actual.FirstName);
        }

        [Fact]
        public async void Task_When_UpdateEmployee_Expect_EmployeeWasUpdate()
        {
            // Arrange

            var selectingEmployeeModel = GetSelectingEmployeeModel();

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(selectingEmployeeModel))
            };

            var mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

            var mockOptions = GetMockIOptions();

            var employeeController = new EmployeeController(mockClient.Object, mockOptions.Object);

            var viewResult = await employeeController.UpdateEmployee(selectingEmployeeModel.Id) as ViewResult;

            var updateEmployeeModel = viewResult.Model as UpdateEmployeeModel;

            response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                StatusCode = HttpStatusCode.OK,
            };

            mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

            employeeController = new EmployeeController(mockClient.Object, mockOptions.Object);

            // Act

            var result = await employeeController.UpdateEmployee(updateEmployeeModel, selectingEmployeeModel.Id) as ViewResult;

            // Assert

            Assert.Null(result);
        }

        [Fact]
        public async void Task_When_DeleteEmployee_Expect_EmployeeWasDeleted()
        {
            // Arrange

            var id = 1;

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                StatusCode = HttpStatusCode.OK
            };

            var mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

            var mockOptions = GetMockIOptions();

            var employeeController = new EmployeeController(mockClient.Object, mockOptions.Object);

            // Act

            var result = await employeeController.DeleteEmployee(id) as ViewResult;

            // Assert

            Assert.Null(result);
        }
    }
}
