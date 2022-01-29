using HackerNews.API.Application.Mediator.Base;
using HackerNews.Domain.Constants;
using HackerNews.Domain.Entities.HackerNews;
using HackerNews.Domain.Entities.Integration;
using HackerNews.Domain.Interfaces.Infra.DataAccess.Redis;
using HackerNews.Domain.Interfaces.Infra.Logger;
using HackerNews.Domain.Interfaces.Infra.Services.HackerNews;
using HackerNews.Infraestructure.Tools.SafeCaller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.API.Application.Mediator.Commands.HackerNews
{
    public class CacheTop20NewsCommandHandler : AbstractRequestHandler<CacheTop20NewsCommand>
    {
        private readonly IHackerNewsRedis _hackerNewsRedis;
        private readonly IHackerNewsService _hackerNewsService;

        private readonly object _locker = new object();

        public CacheTop20NewsCommandHandler(IHackerNewsRedis hackerNewsRedis, 
            IHackerNewsService hackerNewsService, 
            ILogger logger) : base(logger)
        {
            _hackerNewsRedis = hackerNewsRedis;
            _hackerNewsService = hackerNewsService;
        }

        internal override Task<Response> HandleRequest(CacheTop20NewsCommand request, CancellationToken cancellationToken)
        {
            var idList = GetBestHistoryIdList();
            var newsList = GetHistoriesDetails(idList);

            var top20News = newsList.OrderByDescending(n => n.Score).Take(20).ToList();

            _hackerNewsRedis.Add(RedisConstants.Top20News, top20News, TimeSpan.FromMinutes(15));

            return new Response(top20News).GetResponseAsTask();
        }

        private List<long> GetBestHistoryIdList()
        {
            var idList = new List<long>();

            SafeCaller.SafeCall(() =>
            {
                idList = _hackerNewsService.GetListOfBestHistoriesIds();
            }, _logger);

            return idList;
        }

        private List<New> GetHistoriesDetails(List<long> idList)
        {
            var newsDetailsList = new List<New>();

            var taskList = new List<Task>();
            foreach (var id in idList)
                taskList.Add(GetHistoryDetail(id, newsDetailsList));

            var taskAwaiter = Task.WhenAll(taskList);
            taskAwaiter.Wait();

            if (taskAwaiter.Status == TaskStatus.Faulted)
                throw new Exception("Failed retrieving stories details"); // Obs 1.

            return newsDetailsList;
        }

        private Task GetHistoryDetail(long id, List<New> newsDetailsList)
        {
            return Task.Run(() =>
            {
                var newDetail = GetNewDetail(id);

                lock (_locker)
                {
                    if (!newsDetailsList.Contains(newDetail))
                        newsDetailsList.Add(newDetail);
                }
            });
        }

        private New GetNewDetail(long id)
        {
            var newDetail = new New();

            SafeCaller.SafeCall(() =>
            {
                newDetail = _hackerNewsService.GetNewDetails(id);
            }, _logger);

            return newDetail;
        }
    }
}
