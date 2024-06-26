namespace YBB.AbpSettingManagement.Extensions.Constants;

public static class SettingLocalizationTypeMap
{
    public static readonly Dictionary<Type, string> TypeMap = new Dictionary<Type, string>
    {
        { typeof(string), "string" },
        { typeof(int), "int" },
        { typeof(bool), "bool" }
    };

    public static readonly Dictionary<Type, string> DefaultValueMap = new Dictionary<Type, string>
    {
        { typeof(string), "" },
        { typeof(int), "0" },
        { typeof(bool), "true" }
    };
}