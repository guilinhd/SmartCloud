using Microsoft.OpenApi.Models;
using SmartCloud.Common.Application;
using SmartCloud.Common.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.AspNetCore;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace SmartCloud.Common.HttpApi.Host
{
    [DependsOn(
        typeof(AbpAspNetCoreModule),
        typeof(AbpAutofacModule),
        typeof(CommonApplicationModule),
        typeof(CommonEntityFrameworkCoreModule),
        typeof(CommonHttpApiModule)
    )]
    public class CommonHttpApiHostModule : AbpModule
    {
        
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAspNetCoreMvcOptions>(options => {
                options.ConventionalControllers.Create(typeof(CommonApplicationModule).Assembly);
            });

            ConfigureSwaggerServices(context.Services);
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmartCloud.Common.Api");
                });
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseConfiguredEndpoints();
        }

        private void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "SmartCloud.Common.Api", Description = "v1.0" });
                c.CustomSchemaIds(type => type.FullName);
                c.DocInclusionPredicate((doc, description) => true);
            });
        }
    }
}
