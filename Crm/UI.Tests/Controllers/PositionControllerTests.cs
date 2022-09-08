using Microsoft.Extensions.Options;
using Moq.Protected;
using Moq;
using System;
using UI.Helpers;
using UI.Models.Position;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UI.Controllers;
using Newtonsoft.Json;

namespace UI.Tests.Controllers
{
    public class PositionControllerTests
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

        private SelectingPositionModel GetSelectingPositionModel()
        {
            return new SelectingPositionModel()
            {
                Id = 1,
                Name = "Name"
            };
        }

        [Fact]
        public async void Task_When_CreatePosition_Expect_PositionWasCreated()
        {
            // Arrange

            var expected = 1;

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent($"{expected}")
            };

            var mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

            var mockOptions = GetMockIOptions();

            var positionController = new PositionController(mockClient.Object, mockOptions.Object);

            var createPositionModel = new CreatePositionModel()
            {
                Name = "Name"
            };

            // Act

            var result = await positionController.CreatePosition(createPositionModel) as ViewResult;

            // Assert

            Assert.Equal(expected, result.Model);
        }

        [Fact]
        public async void Task_When_SelectPosition_Expect_PositionWasSelected()
        {
            // Arrange

            var expected = GetSelectingPositionModel();

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expected))
            };

            var mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

            var mockOptions = GetMockIOptions();

            var positionController = new PositionController(mockClient.Object, mockOptions.Object);

            // Act

            var result = await positionController.SelectPosition(expected.Id) as ViewResult;

            var actual = result.Model as SelectingPositionModel;

            // Assert

            Assert.Equal(expected.Name, actual.Name);
        }

        [Fact]
        public async void Task_When_UpdatePosition_Expect_PositionWasUpdate()
        {
            // Arrange

            var selectingPositionModel = GetSelectingPositionModel();

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(selectingPositionModel))
            };

            var mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

            var mockOptions = GetMockIOptions();

            var positionController = new PositionController(mockClient.Object, mockOptions.Object);

            var viewResult = await positionController.UpdatePosition(selectingPositionModel.Id) as ViewResult;

            var updatePositionModel = viewResult.Model as UpdatePositionModel;

            response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                StatusCode = HttpStatusCode.OK,
            };

            mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

            positionController = new PositionController(mockClient.Object, mockOptions.Object);

            // Act

            var result = await positionController.UpdatePosition(updatePositionModel, selectingPositionModel.Id) as ViewResult;

            // Assert

            Assert.Null(result);
        }

        [Fact]
        public async void Task_When_DeletePosition_Expect_PositionWasDeleted()
        {
            // Arrange

            var id = 1;

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                StatusCode = HttpStatusCode.OK
            };

            var mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

            var mockOptions = GetMockIOptions();

            var positionController = new PositionController(mockClient.Object, mockOptions.Object);

            // Act

            var result = await positionController.DeletePosition(id) as ViewResult;

            // Assert

            Assert.Null(result);
        }
    }
}
