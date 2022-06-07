using SmartCloud.Common;
using Volo.Abp.Modularity;

namespace SmartCloud.Core
{
    [DependsOn(
        typeof(CommonDomainSharedModule)    
    )]
    public class CoreDomainSharedModule : AbpModule
    {

    }
}