using System;
using System.IO;
using System.Linq;
using VSCleaner.Core.Implementation;
using Xunit;

namespace VSCleaner.CoreTests
{
    public class DirectoryCleanerTests
    {
        private readonly string testDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}/TestDirectory";
        private readonly string testSlnFile = "test.sln";
        private readonly string testCsprojFile = "test.csproj";
        
        [Fact]
        public void UnExistenceDirectory_ShouldThrowArgumentException()
        {
            // Arrange
            const string path = "Incorrect/Directory";

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => new DirectoryCleaner().CleanDirectory(path));
        }

        [Fact]
        public void NotProjectDirectory_ShouldThrowArgumentException()
        {
            // Arrange
            CreateDirectory(testDirectory);
            
            // Act
            //Arrange
            Assert.Throws<ArgumentException>(() => new DirectoryCleaner().CleanDirectory(testDirectory));
            Directory.Delete(testDirectory, true);
        }

        [Fact]
        public void EmptyDirectory_ShouldReturnEmptyCollection()
        {
            // Arrange
            CreateDirectory(testDirectory);
            File.Create($"{testDirectory}/{testCsprojFile}").Close();

            // Act
            var result = new DirectoryCleaner().CleanDirectory(testDirectory);
            
            // // Assert
            Assert.False(result.Any());
            Directory.Delete(testDirectory, true);
        }

        [Fact]
        public void DirectoryWithBinObj_ShouldRemove2Folders()
        {
            // Arrange
            CreateDirectory(testDirectory);
            File.Create($"{testDirectory}/{testSlnFile}").Close();
            var binFolder = $"{testDirectory}/bin";
            var objDirectory = $"{testDirectory}/obj";
            CreateDirectory(binFolder);
            CreateDirectory(objDirectory);
            
            // Act
            var result = new DirectoryCleaner().CleanDirectory(testDirectory);
            
            // Assert
            Assert.True(result.Count() == 2);
            
            Directory.Delete(testDirectory, true);
        }

        [Fact]
        public void RecursiveFolders_ShouldDeleteOnlyBinAndObj()
        {
            // Arrange
            CreateDirectory(testDirectory);
            File.Create($"{testDirectory}/{testSlnFile}").Close();
            var binFolder = $"{testDirectory}/bin";
            var objDirectory = $"{testDirectory}/obj";
            var keepDirectory = $"{testDirectory}/keepIt";
            var keepDirectory2 = $"{testDirectory}/keepIt2";
            var nestedBinFolder = $"{keepDirectory2}/bin";
            CreateDirectory(binFolder);
            CreateDirectory(objDirectory);
            CreateDirectory(keepDirectory);
            CreateDirectory(keepDirectory2);
            CreateDirectory(nestedBinFolder);
            
            // Act
            var result = new DirectoryCleaner().CleanDirectory(testDirectory);
            
            //Assert
            Assert.True(result.Count() == 3);
            Assert.True(Directory.Exists(keepDirectory));
            Assert.True(Directory.Exists(keepDirectory2));
            
            Directory.Delete(testDirectory, true);
        }
        
        private static void CreateDirectory(string fullName)
        {
            if (!Directory.Exists(fullName))
            {
                Directory.CreateDirectory(fullName);
            }
        }
    }
}