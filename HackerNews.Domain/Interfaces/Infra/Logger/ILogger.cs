using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Domain.Interfaces.Infra.Logger
{
    public interface ILogger
    {
        void Error(string message);
        void Info(string message);
        void Warning(string message);
        void Debug(string message);
    }
}
