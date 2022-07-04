using HackerNews.Domain.Entities.Base;
using StackExchange.Redis;
using System;
using Microsoft.Extensions.Options;
using HackerNews.Domain.Entities.Attributes;
using System.Linq;
using HackerNews.Domain.Interfaces.Infra.DataAccess.Redis.Factory;
using HackerNews.Domain.Interfaces.Infra.Logger;
using Newtonsoft.Json;

namespace HackerNews.Infraestructure.DataAccess.Redis.Factory
{
    public class RedisConnectionFactory : IRedisConnectionFactory
    {
        [Redis(Reference = "HackerNews")]
        public Lazy<ConnectionMultiplexer> _hackerNewsConnection { get; set; }

        public RedisConnectionFactory(IOptions<AppSettings> settings,
            ILogger logger)
        {
            var redisConfigs = settings.Value.RedisConfigs;

            logger.Info($"Redis configs: {JsonConvert.SerializeObject(redisConfigs)}");

            _hackerNewsConnection = new Lazy<ConnectionMultiplexer>(() =>
            {
                var connectionString = redisConfigs.FirstOrDefault(c => c.Reference == GetRedisConfig("_hackerNewsConnection")).ConnectionString;
                return ConnectionMultiplexer.Connect(connectionString);
            });
        }
    
        public ConnectionMultiplexer GetHackerNewsConnection()
        {
            return _hackerNewsConnection.Value;
        }

        private string GetRedisConfig(string propertyName)
        {
            var prop = this.GetType().GetProperty(propertyName).GetCustomAttributes(true).FirstOrDefault();

            if (prop is RedisAttribute)
                return (prop as RedisAttribute).Reference;

            return string.Empty;
        }
    }
}
