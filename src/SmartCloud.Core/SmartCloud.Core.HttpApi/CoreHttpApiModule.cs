using Volo.Abp.Modularity;
using SmartCloud.Common;

namespace SmartCloud.Core
{
    [DependsOn(
        typeof(CoreApplicationContractsModule),
        typeof(CommonHttpApiModule)
    )]
    public class CoreHttpApiModule : AbpModule
    {

    }
}