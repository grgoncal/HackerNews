using HackerNews.Domain.Interfaces.Infra.DataAccess.Redis.Base;
using HackerNews.Domain.Interfaces.Infra.Logger;
using HackerNews.Infraestructure.Tools;
using Newtonsoft.Json;
using Polly;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Infraestructure.DataAccess.Redis.Base
{
    public abstract class RedisBase<T> : AbstractHandler, IRedisBase<T>
    {
        protected IDatabase _database;
        protected IServer _server;
        protected ILogger _logger;

        protected RedisBase(ILogger logger)
        {
            _logger = logger;
        }

        public async Task AddAsync(string key, object value, TimeSpan? expiresIn = null)
        {
            expiresIn ??= TimeSpan.FromMinutes(15);

            await RetryDoWorkAsync(async () =>
            {
                var content = JsonConvert.SerializeObject(value);
                var redisValue = new RedisValue(content);

                await _database.StringSetAsync(key, redisValue, expiresIn);
            }, (e, retryNumber) =>
            {
                var serializedValue = JsonConvert.SerializeObject(value);
                _logger.Error($"Retry {retryNumber} failed to set object {serializedValue} into key {key} with error {e}");
            }, 3);
        }

        public async Task<T> GetAsync(string key)
        {
            var result = string.Empty;

            await RetryDoWorkAsync(async () => 
            {
                result = await _database.StringGetAsync(key);
            }, (e, retryNumber) => 
            {
                _logger.Error($"Retry {retryNumber} failed to get key {key} with error {e}");
            }, 3);

            if (result == null)
                return default;

            return JsonConvert.DeserializeObject<T>(result.ToString());
        }
    }
}
