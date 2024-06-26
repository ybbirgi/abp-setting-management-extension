using Volo.Abp.Settings;
using YBB.AbpSettingManagement.Extensions.Constants;

namespace YBB.AbpSettingManagement.Extensions.SettingValueProviders;

public class OrganizationUnitSettingValueProvider : SettingValueProvider
{
    public const string ProviderName = SettingManagerConstants.OrganizationUnitProviderName;

    public override string Name => ProviderName;

    protected ICurrentOrganizationUnit CurrentOrganizationUnit;

    public OrganizationUnitSettingValueProvider(
        ISettingStore settingStore,
        ICurrentOrganizationUnit currentOrganizationUnit) : base(settingStore)
    {
        CurrentOrganizationUnit = currentOrganizationUnit;
    }

    public override async Task<string> GetOrNullAsync(SettingDefinition setting)
    {
        return await SettingStore.GetOrNullAsync(setting.Name, Name, CurrentOrganizationUnit.Id?.ToString());
    }

    public override async Task<List<SettingValue>> GetAllAsync(SettingDefinition[] settings)
    {
        return await SettingStore.GetAllAsync(settings.Select(x => x.Name).ToArray(), Name, CurrentOrganizationUnit.Id?.ToString());
    }

}