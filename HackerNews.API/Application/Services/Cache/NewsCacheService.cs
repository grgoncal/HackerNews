using HackerNews.API.Application.Mediator.Commands.HackerNews;
using HackerNews.Domain.Entities.HackerNews;
using HackerNews.Domain.Entities.Integration;
using HackerNews.Domain.Interfaces.App.Services.Cache;
using MediatR;
using System;
using System.Collections.Generic;

namespace HackerNews.API.Application.Services.Cache
{
    public class NewsCacheService : INewsCacheService
    {
        private readonly CachedNews cachedNews;

        private readonly IMediator _mediator;

        public NewsCacheService(IMediator mediator)
        {
            _mediator = mediator;

            cachedNews = new CachedNews(DateTime.Now);
        }

        public List<New> GetTop20News()
        {
            if (cachedNews.IsCacheInvalid())
            {
                var cacheTop20NewsCommand = new CacheTop20NewsCommand();
                var response = _mediator.Send(cacheTop20NewsCommand).GetAwaiter().GetResult();

                var top20News = ParseResponse(response);
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
