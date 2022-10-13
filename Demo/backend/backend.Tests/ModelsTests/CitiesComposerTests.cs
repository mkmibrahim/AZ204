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
    public class CitiesComposerTests
    {
        [Fact]
        public void CreateCitiesComposerTest()
        {
            // Arrange
            var imageRetrieverMock = new Mock<IImageRetriever>();

            // Act
            var availableCitiesComposer = new AvailableCitiesComposer(optionsHelper.CreateOptions(),
                                                                        imageRetrieverMock.Object);
        }

        [Fact]
        public async void GetAvailableCitiesTest()
        {
            // Arrange
            var imageRetrieverMock = new Mock<IImageRetriever>();
            var images = new List<string>()
            {
                "string1"
            };
            imageRetrieverMock.Setup(i => i.getImageAsync(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.FromResult(images));
            var availableCitiesComposer = new AvailableCitiesComposer(optionsHelper.CreateOptions(),
                                                                        imageRetrieverMock.Object);


            // Act
            var result = await availableCitiesComposer.GetAvailableCities();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count > 2);
        }
    }
}
