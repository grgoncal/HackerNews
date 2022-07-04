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

        public Response(Exception e)
        {
            Error = e;
        }

        public object Content { get; set; }
        public object Error { get; set; }

        public Task<Response> GetResponseAsTask()
        {
            return Task.FromResult(this);
        }

        public bool HasError()
        {
            return Error != null || Content == null;
        }
    }
}
