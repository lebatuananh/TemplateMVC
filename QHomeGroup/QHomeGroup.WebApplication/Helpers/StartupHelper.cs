using AutoMapper;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
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
using System.Reflection;

namespace QHomeGroup.WebApplication.Helpers
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
            services.AddTransient<ICompanyIntroService, CompanyIntroService>();
            services.AddScoped<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddScoped<RoleManager<AppRole>, RoleManager<AppRole>>();
            return services;
        }
    }
}
