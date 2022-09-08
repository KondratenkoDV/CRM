using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq.Protected;
using Moq;
using System;
using System.Net;
using UI.Controllers;
using UI.Helpers;
using Xunit;
using UI.Models.Contract;
using Newtonsoft.Json;

namespace UI.Tests.Controllers
{
    public class ContractControllerTests
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

        private SelectingContractModel GetSelectingContractModel()
        {
            return new SelectingContractModel()
            {
                Id = 1,
                Address = "Address",
                Subject = "Subject",
                Price = 0,
                ClientId = 0
            };
        }

        [Fact]
        public async void Task_When_CreateContract_Expect_ContractWasCreated()
        {
            // Arrange

            var expected = 1;

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent($"{expected}")
            };

            var mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

            var mockOptions = GetMockIOptions();

            var contractController = new ContractController(mockClient.Object, mockOptions.Object);

            var createContractModel = new CreateContractModel()
            {
                Address = "Address",
                Subject = "Subject",
                Price = 0,
                ClientId = 0
            };

            // Act

            var result = await contractController.CreateContract(createContractModel) as ViewResult;

            // Assert

            Assert.Equal(expected, result.Model);
        }

        [Fact]
        public async void Task_When_AllContracts_Expect_ContractsWasSelected()
        {
            // Arrange

            var expected = new List<SelectingContractModel>()
            {
                GetSelectingContractModel()
            };

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expected))
            };

            var mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

            var mockOptions = GetMockIOptions();

            var contractController = new ContractController(mockClient.Object, mockOptions.Object);

            // Act

            var result = await contractController.AllContracts() as ViewResult;

            // Assert

            Assert.NotEmpty(result.Model as IEnumerable<SelectingContractModel>);
        }

        [Fact]
        public async void Task_When_SelectContract_Expect_ContractWasSelected()
        {
            // Arrange

            var expected = GetSelectingContractModel();

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expected))
            };

            var mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

            var mockOptions = GetMockIOptions();

            var contractController = new ContractController(mockClient.Object, mockOptions.Object);

            // Act

            var result = await contractController.SelectContract(expected.Id) as ViewResult;

            var actual = result.Model as SelectingContractModel;

            // Assert

            Assert.Equal(expected.Address, actual.Address);
        }

        [Fact]
        public async void Task_When_UpdateContract_Expect_ContractWasUpdate()
        {
            // Arrange

            var selectingContractModel = GetSelectingContractModel();

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(selectingContractModel))
            };

            var mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

            var mockOptions = GetMockIOptions();

            var contractController = new ContractController(mockClient.Object, mockOptions.Object);

            var viewResult = await contractController.UpdateContract(selectingContractModel.Id) as ViewResult;

            var updateContractModel = viewResult.Model as UpdateContractModel;

            response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                StatusCode = HttpStatusCode.OK,
            };

            mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

            contractController = new ContractController(mockClient.Object, mockOptions.Object);

            // Act

            var result = await contractController.UpdateContract(updateContractModel, selectingContractModel.Id) as ViewResult;

            // Assert

            Assert.Null(result);
        }

        [Fact]
        public async void Task_When_DeleteContract_Expect_ContractWasDeleted()
        {
            // Arrange

            var id = 1;

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                StatusCode = HttpStatusCode.OK
            };

            var mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

            var mockOptions = GetMockIOptions();

            var contractController = new ContractController(mockClient.Object, mockOptions.Object);

            // Act

            var result = await contractController.DeleteContract(id) as ViewResult;

            // Assert

            Assert.Null(result);
        }
    }
}