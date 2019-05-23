using System;
using Xunit;

namespace DebuggingExample.Tests 
{
    public class TimeProcessorTest 
    {
        [Fact]
        public void TestCurrentTimeUTC () 
        {
            // Arrange
            var processor = new TimeProcessor ();
            var preTestTimeUtc = DateTime.UtcNow;

            // Act
            var result = processor.CurrentTimeUTC ();

            // Assert that time moves forward
            var postTestTimeUtc = DateTime.UtcNow;
            Assert.True(result >= preTestTimeUtc);
            Assert.True(result <= postTestTimeUtc);
        }
    }
}