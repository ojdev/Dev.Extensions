namespace System
{
    /// <summary>
    /// 
    /// </summary>
    public static class BoolExtension
    {
        /// <summary>
        /// 布尔值转换为汉字
        /// </summary>
        /// <param name="this"></param>
        /// <param name="trueString"></param>
        /// <param name="falseString"></param>
        /// <param name="nullString"></param>
        /// <returns></returns>
        public static string BoolToChinese(this bool? @this, string trueString = "是", string falseString = "否", string nullString = "")
        {
            if (@this != null)
            {
                return BoolToChinese(@this.Value, trueString, falseString);
            }
            return nullString;
        }
        /// <summary>
        /// 布尔值转换为汉字
        /// </summary>
        /// <param name="this"></param>
        /// <param name="trueString"></param>
        /// <param name="falseString"></param>
        /// <returns></returns>
        public static string BoolToChinese(this bool @this, string trueString = "是", string falseString = "否")
        {
            return @this ? trueString : falseString;
        }
    }
}
