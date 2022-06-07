using Volo.Abp.Modularity;
using SmartCloud.Common;

namespace SmartCloud.Core
{
    [DependsOn(
        typeof(CoreDomainModule),
        typeof(CoreApplicationContractsModule),
        typeof(CommonApplicationModule)
    )]
    public class CoreApplicationModule : AbpModule
    {

    }
}