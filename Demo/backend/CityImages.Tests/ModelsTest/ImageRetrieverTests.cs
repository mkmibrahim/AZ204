using CityImages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CityImages.Tests.ModelsTest
{
    public class ImageRetrieverTests
    {
        [Fact]
        public void CreateImageRetriever()
        {
            // Arrange

            // Act
            var imageRetriever = new ImageRetriever();

            //Assert
            Assert.NotNull(imageRetriever);
        }

        [Fact]
        public async Task GetImageTestAsync()
        {
            //Arrange
            var imageRetriever = new ImageRetriever();

            //Act
            var result = await imageRetriever.RetrieveImages("Amsterdam");

            // Assert
            Assert.False(String.IsNullOrEmpty(result.FirstOrDefault()));
        }
    }
}
