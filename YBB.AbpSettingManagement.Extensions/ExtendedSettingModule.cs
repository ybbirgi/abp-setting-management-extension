using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Volo.Abp.VirtualFileSystem;
using YBB.AbpSettingManagement.Extensions.DefinitionProviders;

namespace YBB.AbpSettingManagement.Extensions;

[DependsOn(
    typeof(AbpValidationModule),
    typeof(AbpDddDomainSharedModule)
)]
public class ExtendedSettingModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ExtendedSettingModule>();
        });
        
        context.Services.AddSingleton<ExtendedSettingDefinitionProvider,ExtendedSettingDefinitionProvider>();
    }
}