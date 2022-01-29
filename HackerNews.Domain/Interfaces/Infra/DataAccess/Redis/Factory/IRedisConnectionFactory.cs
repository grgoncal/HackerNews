using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Domain.Interfaces.Infra.DataAccess.Redis.Factory
{
    public interface IRedisConnectionFactory
    {
        public ConnectionMultiplexer GetHackerNewsConnection();
    }
}
