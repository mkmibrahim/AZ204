using backend.Models;
using backend.Tests.Helpers;
using Moq;
using Moq.Protected;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace backend.Tests.ModelsTests
{
    public class WeatherRetrieverTests
    {
        [Fact]
        public void CreateWeatherRetriever()
        {
            // Arrange

            // Act
            var retriever = new WeatherRetriever(optionsHelper.CreateOptions(), new HttpClient());
        }

        [Fact]
        public async Task RetrieveWeather()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                // Setup the Protected method to mock
                .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
                )
                // Prepare the expected response of the mocked http call
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent("{\"Time\":\"2022-10-01T22:00:55.000000+00:00\",\"Temperature\":18.27,\"Humidity\":80,\"History\":[{\"Time\":\"2022-10-11T09:06:55.8744061+00:00\",\"Temperature\":27.42,\"Humidity\":47,\"History\":null}]}"),
                })
                .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object);

            var retriever = new WeatherRetriever(optionsHelper.CreateOptions(), httpClient);

            var cityName = "Amsterdam";
            var weatherObject = await retriever.GetWeather(cityName);

            // Act
            Assert.NotNull(weatherObject); 
            Assert.Equal((decimal)18.27, weatherObject.Temperature);
            Assert.Equal(80, weatherObject.Humidity);
            Assert.NotNull(weatherObject.History);
            var firstHistoryInstance = weatherObject.History.FirstOrDefault();
            Assert.Equal((decimal)27.42, firstHistoryInstance.Temperature);
            Assert.Equal(47, firstHistoryInstance.Humidity);
             
            // also check the 'http' call was like we expected it
            var expectedUri = new Uri("https://localhost:5021/api/Weather/Get?cityName=Amsterdam");
 
            handlerMock.Protected().Verify(
               "SendAsync",
               Times.Exactly(1), // we expected a single external request
               ItExpr.Is<HttpRequestMessage>(req =>
                  req.Method == HttpMethod.Get  // we expected a GET request
                  && req.RequestUri == expectedUri // to this uri
               ),
               ItExpr.IsAny<CancellationToken>()
            );


        }
    }
}
