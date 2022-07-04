using HackerNews.Domain.Entities.HackerNews;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Domain.Interfaces.Infra.Services.HackerNews
{
    public interface IHackerNewsService
    {
        Task<List<long>> GetIdListOfBestHistoriesAsync();
        Task<New> GetNewDetailAsync(string newId);
    }
}
