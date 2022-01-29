using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Domain.Entities.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RedisAttribute : Attribute
    {
        public string Reference { get; set; }
    }
}
