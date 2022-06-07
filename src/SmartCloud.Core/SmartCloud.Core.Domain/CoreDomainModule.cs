using SmartCloud.Common;
using Volo.Abp.Modularity;

namespace SmartCloud.Core
{
    [DependsOn(
        typeof(CoreDomainSharedModule),
        typeof(CommonDomainModule)
    )]
    public class CoreDomainModule : AbpModule
    {

    }
}