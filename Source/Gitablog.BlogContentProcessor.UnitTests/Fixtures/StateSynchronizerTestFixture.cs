using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gitablog.BlogContentProcessor.Abstract;
using Gitablog.TestFramework;
using Moq;

namespace Gitablog.BlogContentProcessor.UnitTests.Fixtures
{
    class StateSynchronizerTestFixture : ITestFixture<StateSynchronizer>
    {
        public Mock<IContentState> ContentState { get; set; }
        public Mock<IBlogContentEngine> BlogContentEngine { get; set; }

        public StateSynchronizerTestFixture()
        {
            ContentState = new Mock<IContentState>();
            BlogContentEngine = new Mock<IBlogContentEngine>();
        }

        public StateSynchronizer CreateSut()
        {
            return new StateSynchronizer(ContentState.Object, BlogContentEngine.Object);
        }
    }
}
