using System.ComponentModel;
using System.Text;

namespace System
{
    /// <summary>
    /// 
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// 判断字符串是否为空
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string @this)
        {
            return string.IsNullOrWhiteSpace(@this);
        }

        /// <summary>
        /// 判断字符串是否不为空
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool NotSpace(this string @this)
        {
            return !string.IsNullOrWhiteSpace(@this);
        }
        /// <summary>
        /// 如果为空字串则返回null否则返回原值，使用场景为 string x="";var y=x??"x";
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string ValueOrNull(this string @this)
        {
            return @this.NotSpace() ? @this : null;
        }
        /// <summary>
        /// 类型转换
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static TType As<TType>(this string @this) where TType : IComparable
        {
            try
            {
                return (TType)TypeDescriptor.GetConverter(typeof(TType)).ConvertFromInvariantString(@this);
            }
            catch
            {
                return default(TType);
            }
        }

        /// <summary>
        /// 转换为Byte数组
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string @this)
        {
            return Encoding.UTF8.GetBytes(@this);
        }
        /// <summary>
        /// 忽略大小写的字符串比较
        /// </summary>
        /// <param name="this"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool UEquals(this string @this, string value)
        {
            if (@this.IsNullOrWhiteSpace() || value.IsNullOrWhiteSpace())
            {
                return @this.IsNullOrWhiteSpace() && value.IsNullOrWhiteSpace();
            }
            return @this?.ToLower() == value?.ToLower();
        }
    }
}
