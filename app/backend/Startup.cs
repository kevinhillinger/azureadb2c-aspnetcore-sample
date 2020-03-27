using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SampleWebApp.B2c.Authentication;
using SampleWebApp.Security;

namespace SampleWebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddConfiguration(Configuration);
            services.AddCertificateAuthentication();
            services.AddB2cAuthentication();
            services.AddCorsPolicies();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory, IOptions<List<CorsPolicyConfig>> corsOptions)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCertificateForwarding();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    static class IServiceCollectionExtensions
    {

        public static void AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<B2cAuthenticationConfig>(configuration.GetSection(B2cAuthenticationConfig.ConfigurationSectionName));
            services.Configure<CorsPolicyConfig>(configuration.GetSection(CorsPolicyConfig.ConfigurationSectionName));
            services.Configure<CertificatesConfig>(configuration.GetSection(CertificatesConfig.ConfigurationSectionName));
        }
    }
}
