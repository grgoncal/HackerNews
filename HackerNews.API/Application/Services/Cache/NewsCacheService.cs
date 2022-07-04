using HackerNews.API.Application.Mediator.Commands.HackerNews;
using HackerNews.API.Domain.Entities.Mediator.Commands;
using HackerNews.Domain.Constants;
using HackerNews.Domain.Entities.HackerNews;
using HackerNews.Domain.Entities.Integration;
using HackerNews.Domain.Interfaces.App.Services.Cache;
using HackerNews.Domain.Interfaces.Infra.DataAccess.Redis;
using HackerNews.Domain.Interfaces.Infra.Logger;
using HackerNews.Infraestructure.Tools;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.API.Application.Services.Cache
{
    public class NewsCacheService : AbstractHandler, INewsCacheService
    {
        private readonly CachedNews cachedNews;

        private readonly IHackerNewsRedis _hackerNewsRedis;
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public NewsCacheService(IHackerNewsRedis hackerNewsRedis,
            IMediator mediator,
            ILogger logger)
        {
            _hackerNewsRedis = hackerNewsRedis;
            _mediator = mediator;
            _logger = logger;

            cachedNews = new CachedNews(DateTime.Now.AddMinutes(15));
        }

        public async Task<Response> GetTop20NewsAsync()
        {
            if (cachedNews.IsCacheInvalid())
            {
                try
                {
                    await DoWorkAsync(async () =>
                    {
                        await UpdateCacheAndGetNews();
                    }, (e) => _logger.Error($"Failed to cache top hacker news {e}"));
                }
                catch (Exception e)
                {
                    return new Response(e);
                }
            }

            return new Response(cachedNews.NewsList);
        }

        private async Task UpdateCacheAndGetNews()
        {
            var top20News = await _hackerNewsRedis.GetAsync(GeneralConstants.RedisKey_Top20News);

            if (top20News == null)
            {
                var cacheTop20NewsCommand = new CacheTop20NewsCommand();
                var response = await _mediator.Send(cacheTop20NewsCommand);

                top20News = ParseResponse(response);
                cachedNews.RefreshCache(top20News);
            }

            cachedNews.UpdateCache(top20News);
        }

        private List<New> ParseResponse(Response response)
        {
            if (response.HasError())
                throw new Exception("Failed to obtain top 20 news");  // Option: use outdated cache info (if any)

            return response.Content as List<New>;
        }
    }
}
