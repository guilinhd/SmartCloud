using Volo.Abp.Modularity;

namespace SmartCloud.Core.Application.Contracts
{
    [DependsOn(
        typeof(CoreDomainSharedModule)
    )]
    public class CoreApplicationContractsModule : AbpModule
    {

    }
}