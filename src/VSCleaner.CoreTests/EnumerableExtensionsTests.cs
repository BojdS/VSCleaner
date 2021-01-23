using System;
using System.Linq;
using Xunit;
using VSCleaner.Core.Extensions;

namespace VSCleaner.CoreTests
{
    public class EnumerableExtensionsTests
    {
        [Fact]
        public void NullObject_ShouldThrowNullReferenceException()
        {
            // Arrange
            // Act
            var result = ((object) null).ToEnumerableOfOne();
            
            // Assert
            Assert.Throws<NullReferenceException>(() => result.ToArray());
        }

        [Fact]
        public void Object_ShouldReturnEnumerableWithOnElement()
        {
            // Arrange
            var test = new object();
            
            // Act
            var result = test.ToEnumerableOfOne();
            
            // Assert
            Assert.True(result.Count() == 1);
        }
    }
}