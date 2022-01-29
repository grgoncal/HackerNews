using HackerNews.Domain.Entities.HackerNews;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Domain.Interfaces.Infra.Services.HackerNews
{
    public interface IHackerNewsService
    {
        List<long> GetListOfBestHistoriesIds();
        New GetNewDetails(long newId);
    }
}
