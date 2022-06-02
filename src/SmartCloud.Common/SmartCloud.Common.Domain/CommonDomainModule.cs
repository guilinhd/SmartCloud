using Volo.Abp.Modularity;

namespace SmartCloud.Common
{
    [DependsOn(typeof(CommonDomainSharedModule))]
    public class CommonDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            
        }
    }
}