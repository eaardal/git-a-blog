using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gitablog.BlogContentProcessor.UnitTests.Fixtures;
using NUnit.Framework;

namespace Gitablog.BlogContentProcessor.UnitTests
{
    [TestFixture]
    public class StateSynchronizerTests
    {
        private StateSynchronizerTestFixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new StateSynchronizerTestFixture();
        }

        [Test]
        public void CreatesSut()
        {
            var sut = _fixture.CreateSut();
            Assert.IsNotNull(sut);
            Assert.IsInstanceOf<StateSynchronizer>(sut);
        }
    }
}
