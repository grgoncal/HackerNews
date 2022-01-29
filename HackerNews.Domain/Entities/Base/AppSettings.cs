using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Domain.Entities.Base
{
    public class AppSettings
    {
        public List<Endpoint> Endpoints { get; set; }
        public List<RedisConfigs> RedisConfigs { get; set; }
    }

    public class RedisConfigs
    {
        public string Reference { get; set; }
        public string ConnectionString { get; set; }
    }

    public class Endpoint
    {
        public string Reference { get; set; }
        public string BaseUrl { get; set; }
        public List<string> Methods { get; set; }
    }
}
