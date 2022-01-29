using HackerNews.Domain.Entities.HackerNews;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Domain.Interfaces.App.Services.Cache
{
    public interface INewsCacheService
    {
        List<New> GetTop20News();
    }
}
