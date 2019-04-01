namespace System
{
    public static class DateTimeOffsetExtension
    {
        /// <summary>
        /// 通过时间获取TimeSpan
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dateTimeOffset">留空则按当前时间计算</param>
        /// <returns></returns>
        public static TimeSpan GetTimeSpan(this DateTimeOffset source, DateTimeOffset? dateTimeOffset = null)
        {
            return (source - (dateTimeOffset ?? DateTimeOffset.Now));
        }
    }
}
