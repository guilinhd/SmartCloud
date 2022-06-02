using Microsoft.AspNetCore.Cors;
using Microsoft.OpenApi.Models;
using SmartCloud.Common.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.AspNetCore;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;

namespace SmartCloud.Common
{
    [DependsOn(
        typeof(AbpAspNetCoreModule),
        typeof(AbpAutofacModule),
        typeof(AbpSwashbuckleModule),
        typeof(CommonApplicationModule),
        typeof(CommonEntityFrameworkCoreModule),
        typeof(CommonHttpApiModule)
    )]
    public class CommonHttpApiHostModule : AbpModule
    {
        
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAspNetCoreMvcOptions>(options => {
                options.ConventionalControllers.Create(typeof(CommonApplicationModule).Assembly, opts =>
                {
                    opts.RootPath = "common";
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

        private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }
    }
}
