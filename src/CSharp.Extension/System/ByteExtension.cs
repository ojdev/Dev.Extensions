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

}
