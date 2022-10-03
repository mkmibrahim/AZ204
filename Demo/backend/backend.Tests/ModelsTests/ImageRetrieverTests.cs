using backend.Models;
using backend.Tests.Helpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
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
            var retriever = new ImageRetriever(optionsHelper.CreateOptions());

            //Assert
            Assert.NotNull(retriever);
        }

        [Theory]
        [InlineData("Paris", 1)]
        [InlineData("Amsterdam", 5)]
        public async Task RetrieveImagesAsync(string cityName, int quantity)
        {
            // Arrange
            var retriever = new ImageRetriever(optionsHelper.CreateOptions());

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
