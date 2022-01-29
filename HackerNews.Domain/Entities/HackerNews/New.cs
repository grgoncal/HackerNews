using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace HackerNews.Domain.Entities.HackerNews
{
    public class New
    {
        public string By { get; set; }
        public long Descendants { get; set; }
        public long Id { get; set; }
        public List<long> Kids { get; set; }
        public long Score { get; set; }
        public long Time { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
    }
}
