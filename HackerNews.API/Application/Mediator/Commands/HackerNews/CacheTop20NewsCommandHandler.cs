using HackerNews.API.Application.Mediator.Base;
using HackerNews.API.Domain.Entities.Mediator.Commands;
using HackerNews.Domain.Constants;
using HackerNews.Domain.Entities.HackerNews;
using HackerNews.Domain.Entities.Integration;
using HackerNews.Domain.Interfaces.Infra.DataAccess.Redis;
using HackerNews.Domain.Interfaces.Infra.Logger;
using HackerNews.Domain.Interfaces.Infra.Services.HackerNews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.API.Application.Mediator.Commands.HackerNews
{
    public class CacheTop20NewsCommandHandler : AbstractExecutionHandler<CacheTop20NewsCommand>
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

        internal override async Task<Response> Execute(CacheTop20NewsCommand request, CancellationToken cancellationToken)
        {
            var idList = await GetBestHistoriesIdsAsync();
            var newsList = GetHistoriesDetails(idList);

            var top20News = newsList.OrderByDescending(n => n.Score).Take(20).ToList();

            _hackerNewsRedis.Add(RedisConstants.Top20News, top20News, TimeSpan.FromMinutes(15));

            return new Response(top20News);
        }

        private async Task<List<long>> GetBestHistoriesIdsAsync()
        {
            return await DoWorkAsync(async () =>
            {
                return await _hackerNewsService.GetIdListOfBestHistoriesAsync();
            }, (e) => _logger.Error("Failed to fetch best histories ids"));
        }

        private List<New> GetHistoriesDetails(List<long> idList)
        {
            var newsDetailsList = new List<New>();
            var taskList = new List<Task>();

            foreach (var id in idList)
            {
                var task = GetHistoryDetail(id, newsDetailsList);
                taskList.Add(task);
            }

            var taskAwaiter = Task.WhenAll(taskList);
            taskAwaiter.Wait();

            if (taskAwaiter.Status == TaskStatus.Faulted)
                throw new Exception("Failed retrieving stories details"); // Obs 1.

            return newsDetailsList;
        }

        private async Task GetHistoryDetail(long id, List<New> newsDetailsList)
        {
            await Task.Run(async () =>
            {
                var newDetail = await GetNewDetailAsync(id);

                lock (_locker)
                {
                    if (!newsDetailsList.Contains(newDetail))
                        newsDetailsList.Add(newDetail);
                }
            });
        }

        private async Task<New> GetNewDetailAsync(long id)
        {
            return await DoWorkAsync(async () =>
            {
                return await _hackerNewsService.GetNewDetailAsync(id.ToString());
            }, (e) => _logger.Error("Failed to fetch history details"));
        }
    }
}
