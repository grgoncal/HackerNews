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
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace HackerNews.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.Configure<AppSettings>(configuration);

            serviceCollection.AddMvc();
            serviceCollection.AddControllersWithViews().AddNewtonsoftJson(options =>
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            serviceCollection.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "HackerNews API", Version = "v1" });
            });

            serviceCollection.AddMediatR(typeof(Startup));
            serviceCollection.AddMediatR(typeof(IRequestHandler<>));
            serviceCollection.AddMediatR(typeof(IRequestHandler<,>));
            serviceCollection.AddMediatR(typeof(INotificationHandler<>));

            serviceCollection.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

            serviceCollection.AddSingleton<ILogger, Logger>();

            serviceCollection.AddTransient<IHackerNewsClient, HackerNewsClient>();
            serviceCollection.AddTransient<IHackerNewsService, HackerNewsService>();

            serviceCollection.AddSingleton<INewsCacheService, NewsCacheService>();

            serviceCollection.AddSingleton<IRedisConnectionFactory, RedisConnectionFactory>();
            serviceCollection.AddSingleton<IHackerNewsRedis, HackerNewsRedis>();

            return serviceCollection;
        }
    }
}
