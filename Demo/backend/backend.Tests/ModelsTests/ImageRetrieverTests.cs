using backend.Models;
using backend.Tests.Helpers;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static System.Net.WebRequestMethods;

namespace backend.Tests.ModelsTests
{
    public class ImageRetrieverTests
    {
        [Fact]
        public void CreateImageRetriever()
        {
            //Arrange
            

            //Act
            var retriever = new ImageRetriever(optionsHelper.CreateOptions(), new HttpClient());

            //Assert
            Assert.NotNull(retriever);
        }

        [Fact]
        public async Task RetrieveImages()
        {
            // Arrange
            string cityName = "Amsterdam";
            int quantity = 2;
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                // Setup the Protected method to mock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                    )
                // prepare the expected response
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent("{\"city\":\"Amsterdam\",\"images\":[\"image1url\",\"imag2url\"]}")
                })
                .Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object);

            var retriever = new ImageRetriever(optionsHelper.CreateOptions(), httpClient);

            // Act
            var images = await retriever.getImageAsync(cityName, quantity);

            // Assert
            Assert.NotNull(images);
            Assert.Equal(quantity, images.Count);
            for(int i = 0; i < images.Count; i++)
                Assert.False(String.IsNullOrEmpty(images[i]));
            // also check the 'http' call was like we expected it
            var expectedUri = new Uri("https://localhost:5001/api/Images/get?cityName=Amsterdam&quantity=2");

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
