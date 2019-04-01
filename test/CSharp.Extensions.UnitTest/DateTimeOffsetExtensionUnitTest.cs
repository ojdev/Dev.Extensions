using System;
using Xunit;

namespace CSharp.Extensions.UnitTest
{
    public class DateTimeOffsetExtensionUnitTest
    {
        [Fact(DisplayName ="时间转换为TimeSpan")]
        public void GetTimeSpan_ShouldBe()
        {
            DateTimeOffset left = new DateTimeOffset(2019, 1, 3, 0, 0, 0, new TimeSpan(8, 0, 0));
            DateTimeOffset right = new DateTimeOffset(2019, 1, 1, 0, 0, 0, new TimeSpan(8, 0, 0));
            var result = left.GetTimeSpan(right);
            Assert.Equal(result, new TimeSpan(2, 0, 0, 0));
        }
    }
}
