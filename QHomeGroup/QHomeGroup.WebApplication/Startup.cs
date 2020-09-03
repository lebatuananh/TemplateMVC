using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QHomeGroup.WebApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using QHomeGroup.WebApplication.Helpers;
using IEmailSender = QHomeGroup.WebApplication.Services.IEmailSender;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Identity;
using QHomeGroup.Data.Entities.System;
using QHomeGroup.Data.EF.Connector;

namespace QHomeGroup.WebApplication
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
            services
                .AddRouting(options => options.LowercaseUrls = true)
                .AddMapper()
                .AddFirebase(Configuration)
                .AddCustomDbContext(Configuration)
                .AddRepositories()
                .AddApplicationServices();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Configure Identity
            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddPortableObjectLocalization(options =>
            {
                options.ResourcesPath = "Localization";
            });
            services.Configure<RequestLocalizationOptions>(
               opts =>
               {
                   var supportedCultures = new List<CultureInfo>
                   {
                        new CultureInfo("en"),
                        new CultureInfo("vi"),
                   };

                   opts.DefaultRequestCulture = new RequestCulture("vi");
                   opts.SupportedCultures = supportedCultures;
                   opts.SupportedUICultures = supportedCultures;
                   opts.RequestCultureProviders.Clear();
                   opts.RequestCultureProviders.Add(new CookieRequestCultureProvider());
               });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(
                options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseRequestLocalization();
            loggerFactory.AddFile("Logs/logfile-{Date}.txt");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                //webapp
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}