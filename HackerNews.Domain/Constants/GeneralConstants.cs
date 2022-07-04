using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Domain.Constants
{
    public static class GeneralConstants
    {
        public static readonly string HackerNews = "HackerNews";

        public static readonly string RedisKey_Top20News = "hacker-news.top20";

        public static readonly int CacheTTL = 15;
    }
}
