# Extended Setting Management

## Included NuGet Packages :
- Volo.Abp.Settings 7.3.2 => https://www.nuget.org/packages/Volo.Abp.Settings
- Volo.Abp.SettingManagement.Domain 7.3.2 => https://www.nuget.org/packages/Volo.Abp.SettingManagement.Domain
- YBB.ExtendedMultiTenancy => Still in progress.
## Project Purpose
The purpose of this project is to enhance the existing setting management structure within ABP by integrating the organization unit structure. This will make setting definitions more generic and easier to use. The project can be added as a NuGet package to other projects.

## Features
- Definitions for `IGlobalSetting`, `ITenantSetting`, and `IOrganizationUnitSetting`
- `OrganizationUnitSettingValueProvider`

## Setting Interfaces

### IGlobalSetting
- Settings defined using the `IGlobalSetting` interface are globally valid and cannot be changed based on Tenant-OrganizationUnit.

### ITenantSetting
- Settings defined using the `ITenantSetting` interface can vary by tenant but will be the same for all organization units within the same tenant.

### IOrganizationUnitSetting
- Settings defined using the `IOrganizationUnitSetting` interface can vary by organization unit.

## Setting Value Type

### SettingValueTypeAttribute
- Used for properties in setting definitions to specify the data type to be stored in the database.
    - Example: For a worker interval setting -> `SettingValueType[typeof(int)]`
    - Example: For external service connection routes -> `SettingValueType[typeof(string)]`

### SettingLocalizationTypeMap
- Contains localizations and default values for definable setting types (int, string, bool). Additions can be made here if necessary.

## Setting Definition Providers

### ExtendedSettingDefinitionProvider
- Contains the structure for creating settings generically. Modules integrating this as a NuGet package will locate classes implementing `IGlobalSetting`, `ITenantSetting`, and `IOrganizationUnitSetting` interfaces through assembly and define these classes.

## Setting Value Providers

### OrganizationUnitSettingValueProvider
- Used to access settings defined for an organization unit.

## Setting Manager Extensions

### OrganizationUnitSettingManagerExtensions
- An extension for the `ISettingManager` defined within ABP. This extension allows settings to be defined and listed.

## Example Setting Definition

```csharp
public class ApplicationSettingNames : IGlobalSetting
{
    private const string Prefix = "Application";
    
    public static class Base
    {
        private const string DefaultSettingPrefix = $"{Prefix}.Default";
        [SettingValueType(typeof(string))]
        public const string AppVersion = $"{DefaultSettingPrefix}.AppVersion";
        [SettingValueType(typeof(string))]
        public const string MobileAppVersion = $"{DefaultSettingPrefix}.MobileAppVersion";
        [SettingValueType(typeof(bool))]
        public const string ForceUpdate = $"{DefaultSettingPrefix}.ForceUpdate";
    }
}
```

## Using Defined Settings with Setting Manager

```csharp
public class TestAppService : ...AppService
{
    private readonly ISettingManager _settingManager;
    private readonly ICurrentTenant _currentTenant;
    private readonly ICurrentOrganizationUnit _currentOrganizationUnit;
    
    public TestAppService(
        ISettingManager settingManager,
        ICurrentTenant currentTenant,
        ICurrentOrganizationUnit currentOrganizationUnit)
    {
        _settingManager = settingManager;
        _currentTenant = currentTenant;
        _currentOrganizationUnit = currentOrganizationUnit;
    }
}

public async Task<TestDto> GetTestDataAsync()
{
    var apiKey = await _settingManager.GetOrNullForTenantAsync(
    "SETTING_KEY",
        (Guid)_currentTenant.Id!
    );     

    var organizationFolderId = int.Parse(await _settingManager.GetOrNullForOrganizationUnitAsync(
            "SETTING_KEY", 
        (Guid)_currentOrganizationUnit.Id!
    ));
    
    // Additional logic...
   
```
}
