using System.Collections.Generic;
using VSCleaner.Core.Contracts;
using VSCleaner.Core.Implementation;
using Xunit;

namespace VSCleaner.CoreTests
{
    public class ArgumentsValidatorTests
    {
        private readonly IArgumentsValidator validator = new ArgumentsValidator();
        
        [Fact]
        public void EmptyArguments_ShouldReturnFalse()
        {
            // Arrange
            var arguments = new List<string>();
            
            // Act
            var result = validator.ValidateArguments(arguments);

            // Assert
            Assert.False(result);
            Assert.False(string.IsNullOrWhiteSpace(validator.ErrorMessage));
        }

        [Fact]
        public void NotEvenArgumentsCount_ShouldReturnFalse()
        {
            // Arrange
            var arguments = new List<string>
            {
                "-p",
                "testArg2",
                "arg3"
            };
            
            // Act
            var result = validator.ValidateArguments(arguments);
            
            // Assert
            Assert.False(result);
            Assert.False(string.IsNullOrWhiteSpace(validator.ErrorMessage));
        }
        
        [Fact]
        public void WithoutPathArguments_ShouldReturnFalse()
        {
            // Arrange
            var arguments = new List<string>
            {
                "-testArg1",
                "testArg2"
            };
            
            // Act
            var result = validator.ValidateArguments(arguments);
            
            // Assert
            Assert.False(result);
            Assert.False(string.IsNullOrWhiteSpace(validator.ErrorMessage));
        }

        [Fact]
        public void DuplicateArguments_ShouldReturnFalse()
        {
            // Arrange
            var arguments = new List<string>
            {
                "-p",
                "Value1",
                "-p",
                "Value2",
                "-targ",
                "var3",
                "-targ",
                "val3"
            };
            
            // Act
            var result = validator.ValidateArguments(arguments);
            
            // Assert
            // Assert
            Assert.False(result);
            Assert.False(string.IsNullOrWhiteSpace(validator.ErrorMessage));
        }

        [Fact]
        public void ArgumentIncorrectPrefix_ShouldReturnFalse()
        {
            // Arrange
            var arguments = new List<string>
            {
                "-p",
                "testArg2",
                "arg3",
                "Hello"
            };
            
            // Act
            var result = validator.ValidateArguments(arguments);
            
            // Assert
            Assert.False(result);
            Assert.False(string.IsNullOrWhiteSpace(validator.ErrorMessage));
        }

        [Fact]
        public void OddArgumentsWithPrefixesAndPath_ShouldReturnTrue()
        {
            // Arrange
            var arguments = new List<string>
            {
                "-p",
                "PathValue",
                "-test",
                "TestVal"
            };
            
            // Act
            var result = validator.ValidateArguments(arguments);
            
            // Assert
            Assert.True(result);
            Assert.True(string.IsNullOrWhiteSpace(validator.ErrorMessage));
        }
    }
}