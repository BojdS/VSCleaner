using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using VSCleaner.Core.Constants;
using VSCleaner.Core.Contracts;
using VSCleaner.Core.Implementation;
using Xunit;

namespace VSCleaner.CoreTests
{
    public class ArgumentsResolverTests
    {
        private readonly Mock<IDirectoryCleaner> directoryCleanerMock = new ();
        private readonly IArgumentsResolver argumentsResolver;
        
        public ArgumentsResolverTests()
        {
            argumentsResolver = new ArgumentsResolver(directoryCleanerMock.Object);
        }

        [Fact]
        public void ValidArguments_ShouldReturnFuncCollection()
        {
            // Arrange
            var arguments = new Dictionary<string, string>
            {
                {VsCleanerConstants.ArgumentsConstants.Path, "TestFolder"}
            };
            directoryCleanerMock
                .Setup(x => x.CleanDirectory(It.IsAny<string>()))
                .Returns(() => new List<string>());
            
            // Act
            var result = argumentsResolver.ProcessArguments(arguments);
            var cleanerDelegate = result.FirstOrDefault();
            // Assert
            Assert.IsAssignableFrom<IEnumerable<Func<IEnumerable<string>>>>(result);
            Assert.True(result.Any());
            Assert.NotNull(cleanerDelegate);
            Assert.Empty(cleanerDelegate());
        }

        [Fact]
        public void EmptyArguments_ShouldThrowArgumentException()
        {
            // Arrange
            var arguments = new Dictionary<string, string>();
            
            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => argumentsResolver.ProcessArguments(arguments));
        }

        [Fact]
        public void InvalidArgument_ShouldThrowArgumentException()
        {
            // Arrange
            var arguments = new Dictionary<string, string>
            {
                {"invalid", ""}
            };
            
            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => argumentsResolver.ProcessArguments(arguments));
        }
    }
}