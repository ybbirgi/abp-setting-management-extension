using Volo.Abp.DependencyInjection;
using Volo.Abp.SettingManagement;
using YBB.AbpSettingManagement.Extensions.Constants;

namespace YBB.AbpSettingManagement.Extensions.SettingManagementProviders;

public class OrganizationUnitSettingManagementProvider : SettingManagementProvider, ITransientDependency
{
    public override string Name => SettingManagerConstants.OrganizationUnitProviderName;
    protected ICurrentOrganizationUnit CurrentOrganizationUnit;

    public OrganizationUnitSettingManagementProvider(
        ISettingManagementStore settingManagementStore,
        ICurrentOrganizationUnit currentOrganizationUnit
    ) : base(settingManagementStore)
    {
        CurrentOrganizationUnit = currentOrganizationUnit;
    }

    protected override string NormalizeProviderKey(string providerKey)
    {
        if (providerKey != null)
        {
            return providerKey;
        }

        return CurrentOrganizationUnit.Id?.ToString();
    }
}