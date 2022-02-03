namespace System;

/// <summary>
/// 
/// </summary>
public static class TypeEx
{
    /// <summary>
    /// 获取派生的实体
    /// </summary>
    /// <returns></returns>
    public static IReadOnlyList<Type> GetTypesFrom<TInterface>()
    {
        return Assembly.GetExecutingAssembly().GetTypes()
            .Where(s => typeof(TInterface).IsAssignableFrom(s))
            .Select(s => s).ToList();
    }
}