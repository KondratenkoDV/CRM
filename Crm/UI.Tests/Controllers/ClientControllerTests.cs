using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Collections;
using System.Net;
using UI.Controllers;
using UI.Helpers;
using UI.Models.Client;
using UI.Models.Enum;
using Xunit;

namespace UI.Tests.Controllers
{
    public class ClientControllerTests
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
            var mockClient = new Mock<IHttpClientFactory>();

            mockClient.Setup(c => c.CreateClient(It.IsAny<string>()))
                .Returns((string s) => new HttpClient(httpMessageHandler.Object));

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

        private SelectingClientModel GetSelectingClientModel()
        {
            return new SelectingClientModel()
            {
                Id = 1,
                SelectedName = "Name",
                SelectedСodeOfTheCountry = "380",
                SelectedRegionCode = "00",
                SelectedSubscriberNumber = "0000000"
            };
        }

        [Fact]
        public async void Task_When_CreateClient_Expect_ClientWasCreated()
        {
            // Arrange

            var expected = 1;

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent($"{expected}")
            };

            var mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

            var mockOptions = GetMockIOptions();

            var clientController = new ClientController(mockClient.Object, mockOptions.Object);

            var createClientModel = new CreateClientModel()
            {
                Name = "Name",
                СodeOfTheCountry = "380",
                RegionCode = "00",
                SubscriberNumber = "0000000"
            };

            // Act

            var result = await clientController.CreateClient(createClientModel) as ViewResult;

            // Assert

            Assert.Equal(expected, result.Model);
        }

        [Fact]
        public async void Task_When_AllClients_Expect_ClientsWasSelected()
        {
            // Arrange

            var expected = new List<SelectingClientModel>()
            {
                GetSelectingClientModel()
            };

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expected))
            };

            var mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

            var mockOptions = GetMockIOptions();

            var clientController = new ClientController(mockClient.Object, mockOptions.Object);

            // Act

            var result = await clientController.AllClients() as ViewResult;

            // Assert

            Assert.NotEmpty(result.Model as IEnumerable<SelectingClientModel>);
        }

        [Fact]
        public async void Task_When_SelectClient_Expect_ClientWasSelected()
        {
            // Arrange

            var expected = GetSelectingClientModel();

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expected))
            };

            var mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

            var mockOptions = GetMockIOptions();

            var clientController = new ClientController(mockClient.Object, mockOptions.Object);

            // Act

            var result = await clientController.SelectClient(expected.Id) as ViewResult;

            var actual = result.Model as SelectingClientModel;

            // Assert

            Assert.Equal(expected.SelectedName, actual.SelectedName);
        }

        [Fact]
        public async void Task_When_UpdateClient_Expect_ClientWasSelected()
        {
            // Arrange

            var selectingClientModel = GetSelectingClientModel();

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(selectingClientModel))
            };

            var mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

            var mockOptions = GetMockIOptions();

            var clientController = new ClientController(mockClient.Object, mockOptions.Object);

            // Act

            var result = await clientController.UpdateClient(selectingClientModel.Id) as RedirectToActionResult;

            // Assert

            Assert.Equal("UpdateClientView", result.ActionName );
        }

        [Fact]
        public async void Task_When_UpdateClient_Expect_ClientWasUpdate()
        {
            // Arrange

            int id = 1;

            var updateClientModel = new UpdateClientModel()
            {
                NewName = "NewName",
                NewСodeOfTheCountry = "830",
                NewRegionCode = "11",
                NewSubscriberNumber = "1111111"
            };

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(updateClientModel))
            };

            var mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

            var mockOptions = GetMockIOptions();

            var clientController = new ClientController(mockClient.Object, mockOptions.Object);

            // Act

            var result = await clientController.UpdateClient(updateClientModel, id) as RedirectToActionResult;

            // Assert

            Assert.Equal("Сhoice", result.ActionName);
        }

        [Fact]
        public async void Task_When_DeleteClient_Expect_ClientWasDeleted()
        {
            // Arrange

            var id = 1;

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                StatusCode = HttpStatusCode.OK
            };

            var mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

            var mockOptions = GetMockIOptions();

            var clientController = new ClientController(mockClient.Object, mockOptions.Object);

            // Act

            var result = await clientController.DeleteClient(id) as RedirectToActionResult;

            // Assert

            Assert.Equal("Сhoice", result.ActionName);
        }

        [Fact]
        public async void Task_When_CreateFormClientView_Expect_PassesEnumValues()
        {
            // Arrange

            var valueСodeOfTheCountry = new List<ValueCodeOfTheCountryModel>();

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(valueСodeOfTheCountry))
            };

            var mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

            var mockOptions = GetMockIOptions();

            var clientController = new ClientController(mockClient.Object, mockOptions.Object);

            // Act

            var result = await clientController.CreateFormClientView() as ViewResult;

            var expected = result.ViewData as IEnumerable;

            // Assert

            Assert.NotEmpty(expected);
        }
    }
}
