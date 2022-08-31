

namespace FunctionProject.Tests
{
    public class UnitTest1
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
            var result = await imageRetriever.RetrieveImage("Amsterdam");

            // Assert
            Assert.False(String.IsNullOrEmpty(result));
        }
    }
}