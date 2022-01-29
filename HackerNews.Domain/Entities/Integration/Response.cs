using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Domain.Entities.Integration
{
    public class Response
    {
        public Response()
        {
        }

        public Response(object content)
        {
            Content = content;
        }

        public Response(string error)
        {
            Error = error;
        }

        public object Content { get; set; }
        public string Error { get; set; }

        public Task<Response> GetResponseAsTask()
        {
            return Task.FromResult(this);
        }

        public bool HasError()
        {
            return !string.IsNullOrEmpty(Error) || Content == null;
        }
    }
}
