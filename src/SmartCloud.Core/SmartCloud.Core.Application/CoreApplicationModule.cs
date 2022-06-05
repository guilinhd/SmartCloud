using Volo.Abp.Modularity;

namespace SmartCloud.Core
{
    [DependsOn(
        typeof(CoreDomainModule),
        typeof(CoreApplicationContractsModule)
    )]
    public class CoreApplicationModule : AbpModule
    {

    }
}