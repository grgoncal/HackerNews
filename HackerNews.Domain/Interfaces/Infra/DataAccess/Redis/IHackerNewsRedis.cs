using HackerNews.Domain.Entities.HackerNews;
using HackerNews.Domain.Interfaces.Infra.DataAccess.Redis.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Domain.Interfaces.Infra.DataAccess.Redis
{
    public interface IHackerNewsRedis : IRedisBase<List<New>>
    {
    }
}
