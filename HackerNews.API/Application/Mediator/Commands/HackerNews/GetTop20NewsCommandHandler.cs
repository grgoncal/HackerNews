using HackerNews.API.Application.Mediator.Base;
using HackerNews.Domain.Entities.HackerNews;
using HackerNews.Domain.Entities.Integration;
using HackerNews.Domain.Interfaces.Infra.DataAccess.Redis;
using HackerNews.Domain.Interfaces.Infra.Logger;
using HackerNews.Domain.Interfaces.Infra.Services.HackerNews;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.API.Application.Mediator.Commands.HackerNews
{
    public class GetTop20NewsCommandHandler : AbstractRequestHandler<GetTop20NewsCommand>
    {
        private readonly IMediator _mediator;
        private readonly IHackerNewsRedis _hackerNewsRedis;

        public GetTop20NewsCommandHandler(IMediator mediator,
            IHackerNewsRedis hackerNewsRedis,
            ILogger logger) : base(logger)
        {
            _mediator = mediator;
            _hackerNewsRedis = hackerNewsRedis;
        }

        internal override Task<Response> HandleRequest(GetTop20NewsCommand request, CancellationToken cancellationToken)
        {
            var top20News = _hackerNewsRedis.Get("hacker-news.top20");

            if (top20News == null)
            {
                var cacheTop20NewsCommand = new CacheTop20NewsCommand();
                var result = _mediator.Send(cacheTop20NewsCommand).Result;
                top20News = ParseResult(result);                
            }

            return new Response(top20News).GetResponseAsTask();
        }

        private List<New> ParseResult(Response result)
        {
            if (result == null)
                throw new Exception($"Error ocurred while getting top 20 news");

            if (result != null && !string.IsNullOrEmpty(result.Error))
                throw new Exception($"Error ocurred while getting top 20 news: {result.Error}");

            return result.Content as List<New>;
        }
    }
}
