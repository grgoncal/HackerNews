using HackerNews.API.Application.Mediator.Base;
using HackerNews.Domain.Entities.HackerNews;
using HackerNews.Domain.Entities.Integration;
using HackerNews.Domain.Interfaces.App.Services.Cache;
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
        private readonly INewsCacheService _newsCacheService;

        public GetTop20NewsCommandHandler(INewsCacheService newsCacheService,
            ILogger logger) : base(logger)
        {
            _newsCacheService = newsCacheService;
        }

        internal override Task<Response> HandleRequest(GetTop20NewsCommand request, CancellationToken cancellationToken)
        {
            var top20News = _newsCacheService.GetTop20News();

            return new Response(top20News).GetResponseAsTask();
        }
    }
}
