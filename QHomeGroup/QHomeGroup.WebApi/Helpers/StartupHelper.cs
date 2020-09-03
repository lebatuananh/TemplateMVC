using AutoMapper;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QHomeGroup.Application.AutoMapper;
using QHomeGroup.Application.Common;
using QHomeGroup.Application.Common.Contacts;
using QHomeGroup.Application.Common.Slides;
using QHomeGroup.Application.Content.Blogs;
using QHomeGroup.Application.Introduce;
using QHomeGroup.Application.Notification;
using QHomeGroup.Application.Projects;
using QHomeGroup.Application.Systems.Users;
using QHomeGroup.Data.EF.Abstract;
using QHomeGroup.Data.EF.Connector;
using QHomeGroup.Data.Entities.System;
using QHomeGroup.Infrastructure.Interfaces;
using QHomeGroup.Utilities.Constants;
using QHomeGroup.Utilities.Extensions;
using QHomeGroup.WebApi.Initialization;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Reflection;

namespace QHomeGroup.WebApi.Helpers
{
    public static class StartupHelper
    {
        public static readonly string AssemblyName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseLazyLoadingProxies()
                    .UseSqlServer(configuration.GetConnectionString(SystemConstants.ConnectionString), b =>
                    {
                        b.MigrationsAssembly(AssemblyName);
                    });
            });
            services.AddScoped<IUnitOfWork, EFUnitOfWork>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<,>), typeof(EFRepository<,>));
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IAppUserService, AppUserService>();
            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<IBlogService, BlogService>();
            services.AddTransient<ISlideService, SlideService>();
            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<IIntroduceConfigService, IntroduceConfigService>();
            services.AddTransient<IHomeConfigService, HomeConfigService>();
            services.AddTransient<ICompanyIntroService,CompanyIntroService>();
            services.AddScoped<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>();
            return services;
        }

        public static IServiceCollection AddFirebase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(sp => FirebaseApp.Create(new AppOptions()
            {
                Credential =
                    GoogleCredential.FromFile(configuration.GetValue<string>("GOOGLE_APPLICATION_CREDENTIALS")),
            }));
            services.AddScoped<INotificationService>(sp =>
            {
                var firebaseApp = sp.GetRequiredService<FirebaseApp>();             
                return new NotificationService(FirebaseMessaging.GetMessaging(firebaseApp), configuration);
            });
            return services;
        }

        public static IServiceCollection ConfigureInitialization(this IServiceCollection services)
        {
            services.AddTransient<IInitializationStage, DbMigrationStage>();
            services.AddTransient<IInitializationStage, AddDefaultDataStage>();
            return services;
        }

        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DomainToViewModelMappingProfile());
                cfg.AddProfile(new ViewModelToDomainMappingProfile());
            });
            services.AddSingleton(mapperConfig.CreateMapper().RegisterMap());

            return services;
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "QHome API", Version = "v1" });
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(security);
            });
        }

        public static void UseSwaggerUi(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "QHome API v1");
                c.DocumentTitle = "QHome API";
            });
        }
    }
}
