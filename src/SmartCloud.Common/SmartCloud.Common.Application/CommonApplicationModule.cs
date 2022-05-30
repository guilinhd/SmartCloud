
using Volo.Abp.Modularity;
using Volo.Abp.AutoMapper;
using SmartCloud.Common.Application.Contracts;
using SmartCloud.Common.Domain;

namespace SmartCloud.Common.Application
{
    [DependsOn(
        typeof(AbpAutoMapperModule),
        typeof(CommonDomainModule),
        typeof(CommonApplicationContractsModule)
    )]
    public class CommonApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<CommonApplicationModule>();
            });
        }
    }
}