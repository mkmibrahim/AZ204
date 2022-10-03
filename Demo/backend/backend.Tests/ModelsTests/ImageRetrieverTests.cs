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

        [Trait("Category","Integration")]
        [Theory]
        [InlineData("Paris", 1)]
        [InlineData("Amsterdam", 5)]
        public async Task RetrieveImagesAsync(string cityName, int quantity)
        {
            // Arrange
            var retriever = new ImageRetriever(optionsHelper.CreateOptions(), new HttpClient());

            // Act
            var images = await retriever.getImageAsync(cityName, quantity);

            // Assert
            Assert.NotNull(images);
            Assert.Equal(quantity, images.Count);
            for(int i = 0; i < images.Count; i++)
                Assert.False(String.IsNullOrEmpty(images[i]));
        }

        [Fact]
        public async Task RetrieveImages1()
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
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri (optionsHelper.CreateOptions().Value.CityImagesUrl)
            };

            var retriever = new ImageRetriever(optionsHelper.CreateOptions(), httpClient);

            // Act
            var images = await retriever.getImageAsync(cityName, quantity);

            // Assert
            Assert.NotNull(images);
            Assert.Equal(quantity, images.Count);
            for(int i = 0; i < images.Count; i++)
                Assert.False(String.IsNullOrEmpty(images[i]));
        }

    }
}
