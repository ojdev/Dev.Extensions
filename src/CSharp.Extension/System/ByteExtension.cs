namespace System;
/// <summary>
/// 
/// </summary>
public static class ByteExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static string OfString(this byte[] @this) => Encoding.UTF8.GetString(@this);

    /// <summary>
    /// 通过密钥，对字符串形式的加密数据进行解密
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="SecretKey">密钥</param>
    /// <param name="SingleLine">True：解析单行；False：解析所有</param>
    /// <param name="Standard">是否使用标准方式</param>
    /// <returns></returns>
    public static string Decrypt(this byte[] bytes, string SecretKey, bool SingleLine = true, bool Standard = false)
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
}
