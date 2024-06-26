namespace YBB.AbpSettingManagement.Extensions.Attributes;

public class SettingValueTypeAttribute : Attribute
{
    public Type ValueType { get; set; }

    public SettingValueTypeAttribute(Type valueType)
    {
        ValueType = valueType;
    }
}