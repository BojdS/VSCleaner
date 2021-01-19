using System;
using VSCleaner.Core.Implementation;
using Xunit;

namespace VSCleaner.CoreTests
{
    public class DirectoryCleanerTests
    {
        [Fact]
        public void UnExistenceDirectory_ShouldThrowArgumentException()
        {
            // Arrange
            const string path = "Incorrect/Directory";

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => new DirectoryCleaner(path));
        }
            
    }
}