using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionProject.Tests
{
    [CollectionDefinition("Startup with Environment Variables Collection")]
    public class StartupCollection : ICollectionFixture<StartupFixture>
    {

    }

    [Collection("Startup with Environment Variables Collection")]
    public class ImageRetrieverTests
    {
        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }
        [Fact]
        public void CreateImageRetrieverTest()
        {
            //Arrange

            //Act
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

        [Fact]
        public async Task GetImagesTestAsync()
        {
            //Arrange
            var imageRetriever = new ImageRetriever();

            //Act
            var result = await imageRetriever.RetrieveImages("Amsterdam", 5);

            // Assert
            for(int i = 0; i < 5; i++)
            {
                Assert.False(String.IsNullOrEmpty(result[i]));
            }
            
        }
    }
}
