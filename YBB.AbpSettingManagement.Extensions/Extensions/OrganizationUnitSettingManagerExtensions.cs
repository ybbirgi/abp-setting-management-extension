using JetBrains.Annotations;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;
using YBB.AbpSettingManagement.Extensions.SettingValueProviders;

namespace YBB.AbpSettingManagement.Extensions.Extensions;

public static class OrganizationUnitSettingManagerExtensions
{
          public static Task<string> GetOrNullForOrganizationUnitAsync(this ISettingManager settingManager, [NotNull] string name, Guid organizationUnitId, bool fallback = true)
          {
              return settingManager.GetOrNullAsync(name, OrganizationUnitSettingValueProvider.ProviderName, organizationUnitId.ToString(), fallback);
          }
  
          public static Task<string> GetOrNullForCurrentOrganizationUnitAsync(this ISettingManager settingManager, [NotNull] string name, bool fallback = true)
          {
              return settingManager.GetOrNullAsync(name, OrganizationUnitSettingValueProvider.ProviderName, null, fallback);
          }
  
          public static Task<List<SettingValue>> GetAllForOrganizationUnitAsync(this ISettingManager settingManager, Guid organizationUnitId, bool fallback = true)
          {
              return settingManager.GetAllAsync(OrganizationUnitSettingValueProvider.ProviderName, organizationUnitId.ToString(), fallback);
          }
  
          public static Task<List<SettingValue>> GetAllForCurrentOrganizationUnitAsync(this ISettingManager settingManager, bool fallback = true)
          {
              return settingManager.GetAllAsync(OrganizationUnitSettingValueProvider.ProviderName, null, fallback);
          }
  
          public static Task SetForOrganizationUnitAsync(this ISettingManager settingManager, Guid organizationUnitId, [NotNull] string name, [CanBeNull] string value, bool forceToSet = false)
          {
              return settingManager.SetAsync(name, value, OrganizationUnitSettingValueProvider.ProviderName, organizationUnitId.ToString(), forceToSet);
          }
  
          public static Task SetForCurrentOrganizationUnitAsync(this ISettingManager settingManager, [NotNull] string name, [CanBeNull] string value, bool forceToSet = false)
          {
              return settingManager.SetAsync(name, value, OrganizationUnitSettingValueProvider.ProviderName, null, forceToSet);
          }
  
          public static Task SetForOrganizationUnitOrGlobalAsync(this ISettingManager settingManager, Guid? organizationUnitId, [NotNull] string name, [CanBeNull] string value, bool forceToSet = false)
          {
              if (organizationUnitId.HasValue)
              {
                  return settingManager.SetForOrganizationUnitAsync(organizationUnitId.Value, name, value, forceToSet);
              }
  
              return settingManager.SetGlobalAsync(name, value);
          }
}