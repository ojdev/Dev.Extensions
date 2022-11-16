namespace System;
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
            return default;
        }
    }

    /// <summary>
    /// 转换为Byte数组
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static byte[] ToUTF8Bytes(this string @this)
    {
        return Encoding.UTF8.GetBytes(@this);
    }
    /// <summary>
    /// 计算MD5值
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string GetMD5String(this string str)
    {
        StringBuilder hash = new();
        if (!str.IsNullOrWhiteSpace())
        {
            MD5 md5 = MD5.Create();
            byte[] buffer = md5.ComputeHash(str.ToUTF8Bytes());
            Array.ForEach(buffer, b => hash.AppendFormat("{0:X2}", b));
        }
        return hash.ToString();
    }

    /// <summary>
    /// 通过密钥对文本进行加密
    /// </summary>
    /// <param name="str"></param>
    /// <param name="SecretKey">密钥</param>
    /// <param name="SingleLine">True：单行；False：所有</param>
    /// <param name="Standard">是否使用标准方式</param>
    /// <returns>加密后的文本</returns>
    public static string DESEncrypt(this string str, string SecretKey, bool SingleLine = true, bool Standard = false)
    {
        DES des = DES.Create();
        if (Standard)
        {
            des.Mode = CipherMode.ECB;
            des.Padding = PaddingMode.Zeros;
        }
        des.Key = SecretKey.GetMD5String().Substring(0, 0x08).ToUTF8Bytes();
        des.IV = SecretKey.GetMD5String().Substring(0, 0x08).ToUTF8Bytes();
        using MemoryStream ms = new();
        using CryptoStream encStream = new(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
        using StreamWriter sw = new(encStream);
        if (SingleLine)
            sw.WriteLine(str);
        else
            sw.Write(str);
        sw.Close();
        encStream.Close();
        byte[] buffer = ms.ToArray();
        ms.Close();
        StringBuilder hash = new();
        Array.ForEach(buffer, b => hash.AppendFormat("{0:X2}", b));
        return hash.ToString();
    }
    /// <summary>
    /// 通过密钥，对字符串形式的加密数据进行解密
    /// </summary>
    /// <param name="str"></param>
    /// <param name="SecretKey">密钥</param>
    /// <param name="SingleLine">True：解析单行；False：解析所有</param>
    /// <param name="Standard">是否使用标准方式</param>
    /// <returns>原始文本</returns>
    public static string DESDecrypt(this string str, string SecretKey, bool SingleLine = true, bool Standard = false)
    {
        #region 数据还原
        int len = str.Length / 2;
        byte[] buffer = new byte[len];
        try
        {
            for (int i = 0; i < len; i++)
            {
                buffer[i] = Convert.ToByte(str.Substring(i * 0x02, 0x02), 0x10);
            }
        }
        catch
        {
            return "不是有效的加密数据。";
        }
        #endregion
        return buffer.DESDecrypt(SecretKey, SingleLine, Standard);
    }

    /// <summary>
    /// 通过密钥，对字符串形式的加密数据进行解密
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="SecretKey">密钥</param>
    /// <param name="SingleLine">True：解析单行；False：解析所有</param>
    /// <param name="Standard">是否使用标准方式</param>
    /// <returns></returns>
    private static string DESDecrypt(this byte[] bytes, string SecretKey, bool SingleLine = true, bool Standard = false)
    {
        DES des = DES.Create();
        if (Standard)
        {
            des.Mode = CipherMode.ECB;
            des.Padding = PaddingMode.Zeros;
        }
        des.Key = SecretKey.GetMD5String().Substring(0, 0x08).ToUTF8Bytes();
        des.IV = SecretKey.GetMD5String().Substring(0, 0x08).ToUTF8Bytes();
        using MemoryStream ms = new(bytes);
        string val = string.Empty;
        try
        {
            using CryptoStream encStream = new(ms, des.CreateDecryptor(), CryptoStreamMode.Read);
            using StreamReader sr = new(encStream);
            val = SingleLine ? sr.ReadLine() : sr.ReadToEnd();
            sr.Close();
            encStream.Close();
        }
        catch
        {
            val = "密钥错误，解密失败。";
        }
        finally
        {
            ms.Close();
        }
        return val;
    }
    
    /// <summary>
    /// 根据条件在尾部追加字符串
    /// </summary>
    /// <param name="val"></param>
    /// <param name="condition">成立条件</param>
    /// <param name="newstr">要追加的新字符串</param>
    /// <returns></returns>
    public static string AppendIf(this string val, bool condition, string newstr)
    {
        if (condition)
        {
            val += newstr;
        }
        return val;
    }
}
