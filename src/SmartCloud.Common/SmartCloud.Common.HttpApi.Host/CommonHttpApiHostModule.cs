using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SmartCloud.Common.EntityFrameworkCore;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
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

            
            app.UseRouting();
            //app.UseAuthentication();
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

        private void ConfigureAuthenticationServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = configuration["Authentication:Issuer"],
                        ValidAudience = configuration["Authentication:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Authentication:SigningKey"]))
                    };
                });
        }

        
    }
}
