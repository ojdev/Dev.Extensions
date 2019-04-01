using System;
using Xunit;

namespace CSharp.Extensions.UnitTest
{
    public class StringExtensionUnitTest
    {
        [Theory(DisplayName = "字符串的As转换", Timeout = 30000)]
        [InlineData("9", 9)]
        [InlineData("3.4", 3.4d)]
        [InlineData("3.4", 3.4f)]
        //[InlineData("3.4", 3.4)]
        [InlineData("0.000000000000000001", 0.000000000000000001)]
        [InlineData("true", true)]
        [InlineData("false", false)]
        public void As_UnitTest<TShoudeBe>(string str, TShoudeBe shoudeBe) where TShoudeBe : IComparable
        {
            Assert.Equal(shoudeBe, str.As<TShoudeBe>());
        }
        [Fact(DisplayName = "字符串Guid的As转换")]
        public void As_Guid_UnitTest()
        {
            Assert.Equal(Guid.Parse("25576f35-9bca-4fc8-b267-807c044db7b5"), "25576f35-9bca-4fc8-b267-807c044db7b5".As<Guid>());
        }
        [Fact(DisplayName = "字符串Decimal的As转换")]
        public void As_Decimal_UnitTest()
        {
            Assert.Equal(3.0000000000000000000001m, "3.0000000000000000000001".As<decimal>());
        }
    }
}
