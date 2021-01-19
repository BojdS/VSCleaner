using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using VSCleaner.Core.Contracts;
using VSCleaner.Core.Implementation;
using Xunit;

namespace VSCleaner.CoreTests
{
    public class ArgumentsReaderTests
    {
        private readonly Mock<IArgumentsValidator> argumentValidatorMock = new ();
        private readonly IArgumentsReader argumentsReader;

        public ArgumentsReaderTests()
        {
            argumentsReader = new ArgumentsReader(argumentValidatorMock.Object);
        }
        
        [Theory]
        [MemberData(nameof(ArgumentsData))]
        public void ArgumentResolver_ShouldReturnArgumentsAndValuesDictionary(Dictionary<string, string> expected, params string[] arguments)
        {
            // Arrange
            argumentValidatorMock
                .Setup(x => x.ValidateArguments(arguments))
                .Returns(() => true);
            
            // Act
            try
            {
                var result = argumentsReader.ReadArgumentsToDictionary(arguments);

                // Assert
                Assert.True(expected.Count == result.Count);
                Assert.True(expected.SequenceEqual(result));
            }
            catch (Exception ex)
            {
                Assert.IsType<ArgumentException>(ex);
            }
        }

        public static IEnumerable<object[]> ArgumentsData()
        {
            yield return new object[]
            {
                new Dictionary<string, string> {{"-p", "Test Path"}}, new[] {"-p", "Test Path"}
            };
            
            yield return new object[]
            {
                new Dictionary<string, string> {{"-testKey", "test.Val"}, {"-p", "Path"}},
                new[] {"-testKey", "test.Val", "-p", "Path"}
            };
            
            yield return new object[]
            {
                new Dictionary<string, string> {{"-p", "pathTest2"}}, 
                new[] {"-p", "pathTest2"}
            };
            yield return new object[]
            {
                new Dictionary<string, string> (), 
                Array.Empty<string>()
            };
        }
    }
}