using Microsoft.Extensions.Options;
using Moq.Protected;
using Moq;
using System;
using UI.Helpers;
using UI.Models.WorkPlan;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UI.Controllers;
using Newtonsoft.Json;

namespace UI.Tests.Controllers
{
    public class WorkPlanControllerTests
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

            private SelectingWorkPlanModel GetSelectingWorkPlanModel()
            {
                return new SelectingWorkPlanModel()
                {
                    Id = 1,
                    DateStart = DateTime.Now.AddDays(-10),
                    DateFinish = DateTime.Now.AddDays(2),
                    ContractId = 0
                };
            }

            [Fact]
            public async void Task_When_CreateWorkPlan_Expect_WorkPlanWasCreated()
            {
                // Arrange

                var expected = 1;

                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent($"{expected}")
                };

                var mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

                var mockOptions = GetMockIOptions();

                var workPlanController = new WorkPlanController(mockClient.Object, mockOptions.Object);

                var createWorkPlanModel = new CreateWorkPlanModel()
                {
                    DateStart = DateTime.Now.AddDays(-10),
                    DateFinish = DateTime.Now.AddDays(2),
                    ContractId = 0
                };

                // Act

                var result = await workPlanController.CreateWorkPlan(createWorkPlanModel) as ViewResult;

                // Assert

                Assert.Equal(expected, result.Model);
            }

            [Fact]
            public async void Task_When_SelectWorkPlan_Expect_WorkPlanWasSelected()
            {
                // Arrange

                var expected = GetSelectingWorkPlanModel();

                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(expected))
                };

                var mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

                var mockOptions = GetMockIOptions();

                var workPlanController = new WorkPlanController(mockClient.Object, mockOptions.Object);

                // Act

                var result = await workPlanController.SelectWorkPlan(expected.Id) as ViewResult;

                var actual = result.Model as SelectingWorkPlanModel;

                // Assert

                Assert.Equal(expected.DateStart, actual.DateStart);
            }

            [Fact]
            public async void Task_When_UpdateWorkPlan_Expect_WorkPlanWasUpdate()
            {
                // Arrange

                var selectingWorkPlanModel = GetSelectingWorkPlanModel();

                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(selectingWorkPlanModel))
                };

                var mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

                var mockOptions = GetMockIOptions();

                var workPlanController = new WorkPlanController(mockClient.Object, mockOptions.Object);

                var viewResult = await workPlanController.UpdateWorkPlan(selectingWorkPlanModel.Id) as ViewResult;

                var updateWorkPlanModel = viewResult.Model as UpdateWorkPlanModel;

                response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    StatusCode = HttpStatusCode.OK,
                };

                mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

                workPlanController = new WorkPlanController(mockClient.Object, mockOptions.Object);

                // Act

                var result = await workPlanController.UpdateWorkPlan(updateWorkPlanModel, selectingWorkPlanModel.Id) as ViewResult;

                // Assert

                Assert.Null(result);
            }

            [Fact]
            public async void Task_When_DeleteWorkPlan_Expect_WorkPlanWasDeleted()
            {
                // Arrange

                var id = 1;

                var response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    StatusCode = HttpStatusCode.OK
                };

                var mockClient = GetMockIHttpClientFactory(GetMockHttpMessageHandler(response));

                var mockOptions = GetMockIOptions();

                var workPlanController = new WorkPlanController(mockClient.Object, mockOptions.Object);

                // Act

                var result = await workPlanController.DeleteWorkPlan(id) as ViewResult;

                // Assert

                Assert.Null(result);
            }
        }
    }
}
