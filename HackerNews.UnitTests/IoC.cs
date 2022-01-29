using HackerNews.API;
using HackerNews.API.Application.Services.Cache;
using HackerNews.Domain.Entities.Base;
using HackerNews.Domain.Interfaces.App.Services.Cache;
using HackerNews.Domain.Interfaces.Infra.DataAccess.Redis;
using HackerNews.Domain.Interfaces.Infra.DataAccess.Redis.Factory;
using HackerNews.Domain.Interfaces.Infra.Logger;
using HackerNews.Domain.Interfaces.Infra.Services.HackerNews;
using HackerNews.Infraestructure.DataAccess.Redis;
using HackerNews.Infraestructure.DataAccess.Redis.Factory;
using HackerNews.Infraestructure.Logger;
using HackerNews.Infraestructure.Services.HackerNews;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;

namespace HackerNews.Tests
{
    [SetUpFixture]
    public class IoC
    {
        public static IServiceCollection ServiceCollection { get; set; }
        public static IServiceProvider ServiceProvider { get; set; }

        [OneTimeSetUp]
        public void AssemblyInit()
        {
            ServiceCollection = new ServiceCollection();
            ServiceCollection.AddScoped<IMediator, Mediator>();

            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                                   .AddEnvironmentVariables()
                                                   .Build();
            ServiceCollection.AddSingleton<IConfiguration>(configuration);
            ServiceCollection.Configure<AppSettings>(configuration);

            ServiceCollection.AddMvc();
            ServiceCollection.AddControllers();

            ServiceCollection.AddMediatR(typeof(Startup));
            ServiceCollection.AddMediatR(typeof(IRequestHandler<>));
            ServiceCollection.AddMediatR(typeof(IRequestHandler<,>));
            ServiceCollection.AddMediatR(typeof(INotificationHandler<>));

            ServiceCollection.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            ServiceCollection.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            ServiceCollection.AddSingleton<ILogger, Logger>();

            ServiceCollection.AddTransient<IHackerNewsClient, HackerNewsClient>();
            ServiceCollection.AddTransient<IHackerNewsService, HackerNewsService>();

            ServiceCollection.AddSingleton<INewsCacheService, NewsCacheService>();

            ServiceCollection.AddSingleton<IRedisConnectionFactory, RedisConnectionFactory>();
            ServiceCollection.AddSingleton<IHackerNewsRedis, HackerNewsRedis>();

            ServiceProvider = ServiceCollection.BuildServiceProvider();
        }
        
        public static IHackerNewsService GetHackerNewsRestClient()
        {
            return ServiceProvider.GetService<IHackerNewsService>();
        }

        public static IMediator GetMediator()
        {
            return ServiceProvider.GetService<IMediator>();
        }

        public static ControllerContext SetControllerContext()
        {
            return new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };
        }

        internal static IOptions<AppSettings> GetSettings()
        {
            return ServiceProvider.GetService<IOptions<AppSettings>>();
        }
    }
}
