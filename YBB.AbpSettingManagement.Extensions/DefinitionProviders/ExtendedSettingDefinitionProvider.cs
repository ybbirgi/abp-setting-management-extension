using System.Reflection;
using Volo.Abp.Localization;
using Volo.Abp.Settings;
using YBB.AbpSettingManagement.Extensions.Attributes;
using YBB.AbpSettingManagement.Extensions.Constants;
using YBB.AbpSettingManagement.Extensions.Interfaces;
using YBB.AbpSettingManagement.Extensions.Localization;

namespace YBB.AbpSettingManagement.Extensions.DefinitionProviders;

public class ExtendedSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        var settings = AppDomain.CurrentDomain.GetAssemblies()
            .Where(c =>
                c.FullName != null &&
                !c.FullName.IsNullOrWhiteSpace() &&
                c.FullName.Contains("Domain"))
            .SelectMany(p => p.GetTypes().Where(
                a =>
                    a is
                    {
                        IsClass: true
                    } &&
                    a.IsAssignableTo<ISetting>())).ToList();

        if (settings.IsNullOrEmpty())
        {
            Console.WriteLine("Setting not found for ExtendedSettingDefinitionProvider");
        }
        
        foreach (var setting in settings)
        {
            var provider = setting.IsAssignableTo<IGlobalSetting>()
                ? SettingManagerConstants.GlobalProvider
                : setting.IsAssignableTo<ITenantSetting>()
                    ? SettingManagerConstants.TenantProvider
                    : SettingManagerConstants.OrganizationUnitProvider;
            var members = setting.GetNestedTypes();
            foreach (var member in members)
            {
                var fields = member.GetFields();
                foreach (var field in fields)
                {
                    var attribute = field.GetCustomAttribute<SettingValueTypeAttribute>();
                    var fieldValue = field.GetValue(null) as string;
                    SettingLocalizationTypeMap.TypeMap.TryGetValue(attribute.ValueType, out var localizationType);
                    SettingLocalizationTypeMap.DefaultValueMap.TryGetValue(attribute.ValueType, out var defaultValue);
                    context.Add(new SettingDefinition(fieldValue, defaultValue,
                            LSettings(fieldValue), isVisibleToClients: true)
                        .WithProviders(provider)
                        .WithProperty("type", localizationType)
                    );
                }
            }
        }
    }

    protected static LocalizableString LSettings(string? name)
    {
        return LocalizableString.Create<ExtendedSettingResource>($"DisplayName:Setting.{name}");
    }
}