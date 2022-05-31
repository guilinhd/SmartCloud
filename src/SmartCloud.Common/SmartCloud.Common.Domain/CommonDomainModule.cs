using SmartCloud.Common.Domain.Shared;
using System.Text.Json;
using System.Text.Unicode;
using Volo.Abp.Modularity;

namespace SmartCloud.Common.Domain
{
    [DependsOn(typeof(CommonDomainSharedModule))]
    public class CommonDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            
        }
    }
}