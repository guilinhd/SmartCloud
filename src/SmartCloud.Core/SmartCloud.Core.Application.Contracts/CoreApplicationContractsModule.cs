using Volo.Abp.Modularity;
using SmartCloud.Common;

namespace SmartCloud.Core
{
    [DependsOn(
        typeof(CoreDomainSharedModule),
        typeof(CommonApplicationContractsModule)
    )]
    public class CoreApplicationContractsModule : AbpModule
    {

    }
}