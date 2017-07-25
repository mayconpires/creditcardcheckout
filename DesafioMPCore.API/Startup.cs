using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Formatters;
using AutoMapper;
using DesafioMPCore.API.AutoMapper;
using DesafioMPCore.Domain.Interface;
using DesafioMPCore.Application.CheckOut;
using DesafioMPCore.Domain.Interface.Application;
using DesafioMPCore.Infra.Http;
using System.IO;

namespace DesafioMPCore.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc()
                    //.AddMvcOptions(o => o.OutputFormatters
                    //    .Add(new XmlDataContractSerializerOutputFormatter()
                    //    ))
                    ;

            //AutoMapper Configuration
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });

            var mapper = config.CreateMapper();

            // Dependency Injection
            services.AddSingleton(mapper);
            services.AddTransient<IFinancialTransactionApp>(ft => new FinancialTransactionApp(new HttpClientHandler(), Configuration["CheckOutSaleUrlMundiPagg"]));
            services.AddTransient<IAccessTokenApp>(ft => new AccessTokenApp(new HttpClientHandler(), Configuration["UserAccessTokenServiceUrl"]));
            services.AddTransient<IMerchantApp>(ft => new MerchantApp(new HttpClientHandler(), Configuration["MerchantGetUrl"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStatusCodePages();
            app.UseMvc();

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404 &&
                   !Path.HasExtension(context.Request.Path.Value) &&
                   !context.Request.Path.Value.StartsWith("/api/"))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });
            //app.UseMvcWithDefaultRoute();

            app.UseDefaultFiles();
            app.UseStaticFiles();
        }
    }
}
