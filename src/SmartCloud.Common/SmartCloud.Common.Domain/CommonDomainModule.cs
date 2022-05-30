using SmartCloud.Common.Domain.Shared;
using Volo.Abp.Modularity;

namespace SmartCloud.Common.Domain
{
    [DependsOn(typeof(CommonDomainSharedModule))]
    public class CommonDomainModule : AbpModule
    {

    }
}