using HackerNews.API.Application.Mediator.Commands.HackerNews;
using HackerNews.Domain.Constants;
using HackerNews.Domain.Entities.HackerNews;
using HackerNews.Domain.Entities.Integration;
using HackerNews.Domain.Interfaces.App.Services.Cache;
using HackerNews.Domain.Interfaces.Infra.DataAccess.Redis;
using MediatR;
using System;
using System.Collections.Generic;

namespace HackerNews.API.Application.Services.Cache
{
    public class NewsCacheService : INewsCacheService
    {
        private readonly CachedNews cachedNews;

        private readonly IHackerNewsRedis _hackerNewsRedis;
        private readonly IMediator _mediator;

        public NewsCacheService(IHackerNewsRedis hackerNewsRedis,
            IMediator mediator)
        {
            _hackerNewsRedis = hackerNewsRedis;
            _mediator = mediator;

            cachedNews = new CachedNews(DateTime.Now.AddMinutes(15));
        }

        public List<New> GetTop20News()
        {
            if (cachedNews.IsCacheInvalid())
            {
                var top20News = _hackerNewsRedis.Get(RedisConstants.Top20News);

                if (top20News == null)
                {
                    var cacheTop20NewsCommand = new CacheTop20NewsCommand();
                    var response = _mediator.Send(cacheTop20NewsCommand).GetAwaiter().GetResult();

                    top20News = ParseResponse(response);
                    cachedNews.RefreshCache(top20News);
                }

                cachedNews.UpdateCache(top20News);
            }

            return cachedNews.NewsList;
        }

        private List<New> ParseResponse(Response response)
        {
            if (response.HasError())
                throw new Exception("Failed to obtain top 20 news");  // Option: use outdated cache info (if any)

            return response.Content as List<New>;
        }
    }
}
