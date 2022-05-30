using SmartCloud.Common.Domain.Shared;
using Volo.Abp.Modularity;

namespace SmartCloud.Common.Application.Contracts
{
    [DependsOn(
        typeof(CommonDomainSharedModule)
    )]
    public class CommonApplicationContractsModule : AbpModule
    {

    }
}