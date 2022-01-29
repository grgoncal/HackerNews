using HackerNews.Domain.Interfaces.Infra.DataAccess.Redis.Base;
using HackerNews.Domain.Interfaces.Infra.Logger;
using HackerNews.Infraestructure.Tools.SafeCaller;
using Newtonsoft.Json;
using Polly;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Infraestructure.DataAccess.Redis.Base
{
    public abstract class RedisBase<T> : IRedisBase<T>
    {
        protected IDatabase _database;
        protected IServer _server;
        protected ILogger _logger;

        protected RedisBase()
        {
        }

        public void Add(string key, object value, TimeSpan? expiresIn = null)
        {
            expiresIn = expiresIn ?? TimeSpan.FromMinutes(60);

            SafeCaller.SafeCall(() =>
            {
                var content = JsonConvert.SerializeObject(value);
                var redisValue = new RedisValue(content);

                _database.StringSet(key, redisValue, expiresIn);
            }, _logger);
        }

        public T Get(string key)
        {
            var result = string.Empty;

            SafeCaller.SafeCall(() =>
            {
                result = _database.StringGetAsync(key).Result;
            }, _logger);

            if (result == null)
                return default(T);

            return JsonConvert.DeserializeObject<T>(result.ToString());
        }
    }
}
