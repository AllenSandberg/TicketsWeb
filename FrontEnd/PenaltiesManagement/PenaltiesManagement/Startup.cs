using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PenaltiesManagement.APIServices;
using Microsoft.Extensions.FileProviders;
using System.IO;
using PenaltiesManagement.Models.Configuration;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;

namespace BCardSite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IFileProvider>(
                            new PhysicalFileProvider(
                                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));


            /** Config Website to work with Sessions. Each request to the website create a session object i nServer. */
            services.AddSession(
                option =>
                {
                    option.IdleTimeout = TimeSpan.FromMinutes(20);
                    option.Cookie.HttpOnly = true;
                }
                );

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();

                ApiLinks.AdminApiUrl= Configuration[$"GlabalAPI:{env.EnvironmentName}:LocalApi"];
                CustomConfiguration.DocumentsPath= Configuration[$"GlabalAPI:{env.EnvironmentName}:PathToDocuments"];
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                ApiLinks.AdminApiUrl = Configuration[$"GlabalAPI:{env.EnvironmentName}:TestApi"];
                CustomConfiguration.DocumentsPath = Configuration[$"GlabalAPI:{env.EnvironmentName}:PathToDocuments"];
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                        ForwardedHeaders.XForwardedProto
            });

            app.UseSession();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
