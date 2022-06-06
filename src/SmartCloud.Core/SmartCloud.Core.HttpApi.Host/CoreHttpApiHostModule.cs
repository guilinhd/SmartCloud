using Microsoft.OpenApi.Models;
using SmartCloud.Core.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Unicode;
using Volo.Abp;
using Volo.Abp.AspNetCore;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace SmartCloud.Core
{
    [DependsOn(
        typeof(AbpAspNetCoreModule),
        typeof(AbpAutofacModule),
        typeof(CoreApplicationModule),
        typeof(CoreEntityFrameworkCoreModule),
        typeof(CoreHttpApiModule)
    )]
    public class CoreHttpApiHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAspNetCoreMvcOptions>(options => {
                options.ConventionalControllers.Create(typeof(CoreApplicationModule).Assembly, opts =>
                {
                    opts.RootPath = "core";
                });
            });

            //Configure<MvcOptions>(configure => {
            //    var policy = new AuthorizationPolicyBuilder()
            //        .RequireAuthenticatedUser()
            //        .Build();
            //    configure.Filters.Add(new AuthorizeFilter(policy));
            //});

            Configure<AbpAntiForgeryOptions>(options => {
                options.AutoValidate = false;
            });

            context.Services.Configure<JsonSerializerOptions>(options => {
                options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.All);
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

            //app.UseCors();
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
