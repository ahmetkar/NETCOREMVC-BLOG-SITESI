using Blog.Data.Context;
using Blog.Data.Repositories.Abstractions;
using Blog.Data.Repositories.Concretes;
using Blog.Data.UnitOfWorks;
using Blog.Service.FluentValidations;
using Blog.Service.Helpers.Images;
using Blog.Service.Services.Abstractions;
using Blog.Service.Services.Concrete;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Service.Extensions
{
    public static class ServiceLayerExtensions
    {
        [Obsolete]
        public static IServiceCollection LoadServiceLayerExtension(this IServiceCollection services, IConfiguration config)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly); //katmandaki tüm automapperleri ekliyor.

            services.AddScoped<IArticleService,ArticleService>();
            services.AddScoped<ICategoryService,CategoryService>();
            services.AddScoped<IImageHelper,ImageHelper>();
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IArticleAIService, ArticleAIService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IMessagesService, MessagesService>();
            services.AddScoped<IImageService, ImageService>();

            services.AddControllersWithViews().AddFluentValidation(opt => {
                opt.RegisterValidatorsFromAssemblyContaining<ArticleValidator>();
                opt.DisableDataAnnotationsValidation = true;
                opt.ValidatorOptions.LanguageManager.Culture = new System.Globalization.CultureInfo("tr");

            });

            return services;
        }
    }
}
