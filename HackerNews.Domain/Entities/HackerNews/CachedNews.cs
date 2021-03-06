using HackerNews.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HackerNews.Domain.Entities.HackerNews
{
    public class CachedNews
    {
        public CachedNews(DateTime expirationDate)
        {
            NewsList = null;
            ExpirationDate = expirationDate;
        }

        public CachedNews(List<New> newList, DateTime expirationDate)
        {
            NewsList = newList;
            ExpirationDate = expirationDate;
        }

        public List<New> NewsList { get; set; }
        public DateTime ExpirationDate { get; set; }

        public void RefreshCache(List<New> newsList)
        {
            NewsList = newsList;
            ExpirationDate = DateTime.Now.AddMinutes(GeneralConstants.CacheTTL);
        }

        public bool IsCacheInvalid()
        {
            if (NewsList == null || !NewsList.Any())
                return true;

            if (ExpirationDate < DateTime.Now)
                return true;

            return false;
        }

        public void UpdateCache(List<New> newsList)
        {
            NewsList = newsList;
        }
    }
}
