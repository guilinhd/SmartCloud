
using Volo.Abp.Modularity;
using Volo.Abp.AutoMapper;

namespace SmartCloud.Common
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