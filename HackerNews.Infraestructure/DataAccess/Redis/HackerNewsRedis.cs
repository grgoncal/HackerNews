using HackerNews.Domain.Entities.HackerNews;
using HackerNews.Domain.Interfaces.Infra.DataAccess.Redis;
using HackerNews.Domain.Interfaces.Infra.DataAccess.Redis.Factory;
using HackerNews.Domain.Interfaces.Infra.Logger;
using HackerNews.Infraestructure.DataAccess.Redis.Base;
using System.Collections.Generic;
using System.Linq;

namespace HackerNews.Infraestructure.DataAccess.Redis
{
    public class HackerNewsRedis : RedisBase<List<New>>, IHackerNewsRedis
    {
        public HackerNewsRedis(IRedisConnectionFactory redisConnectionFactory,
            ILogger logger) : base(logger)
        {
            var connection = redisConnectionFactory.GetHackerNewsConnection();
            var endpoint = connection.GetEndPoints().FirstOrDefault();

            _database = connection.GetDatabase();
            _server = connection.GetServer(endpoint);
        }
    }
}
