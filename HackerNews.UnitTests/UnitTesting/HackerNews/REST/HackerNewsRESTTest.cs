using HackerNews.Domain.Entities.Base;
using HackerNews.Domain.Interfaces.Infra.Services.Base;
using HackerNews.Domain.Interfaces.Infra.Services.HackerNews;
using HackerNews.Infraestructure.Services.HackerNews;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Tests.UnitTesting.HackerNews.REST
{
    public class HackerNewsRestTest
    {
        private IHackerNewsService _hackerNewsService;
        private IOptions<AppSettings> _settings;

        private Mock<IHackerNewsClient> _hackerNewsClientMock;

        [SetUp]
        public void Setup()
        {
            _settings = IoC.GetSettings();
            _hackerNewsService = IoC.GetHackerNewsRestClient();
            _hackerNewsClientMock = new Mock<IHackerNewsClient>();
        }

        [Test]
        public void When_GetAllStoriesIds_Expect_AllStoriesIds()
        {
            var result = _hackerNewsService.GetIdListOfBestHistoriesAsync();

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Count > 1);
        }

        [Test]
        public void When_GetStoryDetail_Expect_StoryDetail()
        {
            var id = _hackerNewsService.GetIdListOfBestHistoriesAsync().FirstOrDefault();

            var detailedNew = _hackerNewsService.GetNewDetails(id);

            Assert.IsTrue(detailedNew != null);
            Assert.IsTrue(!string.IsNullOrEmpty(detailedNew.Title));
            Assert.IsTrue(!string.IsNullOrEmpty(detailedNew.By));
            Assert.IsTrue(detailedNew.Score != null);
            Assert.IsTrue(detailedNew.Id == id);
            Assert.IsTrue(detailedNew.Time != null && detailedNew.Time > 0);
        }

    }
}
