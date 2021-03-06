﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Gitablog.BlogContentProcessor;
using Gitablog.BlogContentProcessor.Models;
using Gitablog.BlogContentProcessor.Utils;
using Gitablog.Infrastructure;
using Gitablog.Web.App_Start;
using Gitablog.Web.Controllers;
using NUnit.Framework;

namespace Gitablog.Web.UnitTests
{
    [TestFixture]
    public class AutofacBootstrapperTests
    {
        private IContainer _container;

        [SetUp]
        public void SetUp()
        {
            _container = AutofacBootstrapper.WireDependencies();
        }

        // Gitablog
        [TestCase(typeof(BlogContentEngine))]
        [TestCase(typeof(PostDto))]
        [TestCase(typeof(BlogLayoutOrganizer))]
        [TestCase(typeof(ContentLocator))]
        [TestCase(typeof(BlogPostProcessor))]
        [TestCase(typeof(ContentRetriever))]
        [TestCase(typeof(ContentState))]
        [TestCase(typeof(FileDownloader))]
        [TestCase(typeof(GitHubContentLocatorStrategy))]
        [TestCase(typeof(GitHubRepository))]
        [TestCase(typeof(IoC))]
        [TestCase(typeof(MarkdownUtil))]
        [TestCase(typeof(GitPollResult))]
        [TestCase(typeof(RawMarkdownContent))]
        [TestCase(typeof(RemoteMarkdownFile))]
        [TestCase(typeof(StateSynchronizer))]
        // MVC
        [TestCase(typeof(HomeController))]
        public void ShouldBeRegistered(Type service)
        {
            var registered = _container.Resolve(service);
            Assert.IsNotNull(registered);
        }

        [TestCase(typeof(StateSynchronizer))]
        [TestCase(typeof(ContentState))]
        [TestCase(typeof(IoC))]
        public void ShouldBeSingleton(Type service)
        {
            var resolved1 = _container.Resolve(service);
            var resolved2 = _container.Resolve(service);
            var resolved3 = _container.Resolve(service);

            Assert.AreSame(resolved1, resolved2);
            Assert.AreSame(resolved1, resolved3);
            Assert.AreSame(resolved2, resolved3);
        }

        [TestCase(typeof(BlogContentEngine))]
        [TestCase(typeof(PostDto))]
        [TestCase(typeof(BlogLayoutOrganizer))]
        [TestCase(typeof(ContentLocator))]
        [TestCase(typeof(BlogPostProcessor))]
        [TestCase(typeof(ContentRetriever))]
        [TestCase(typeof(FileDownloader))]
        [TestCase(typeof(GitHubContentLocatorStrategy))]
        [TestCase(typeof(GitHubRepository))]
        [TestCase(typeof(MarkdownUtil))]
        [TestCase(typeof(GitPollResult))]
        [TestCase(typeof(RawMarkdownContent))]
        [TestCase(typeof(RemoteMarkdownFile))]
        public void ShouldBeInstance(Type service)
        {
            var resolved1 = _container.Resolve(service);
            var resolved2 = _container.Resolve(service);
            var resolved3 = _container.Resolve(service);
            
            Assert.AreNotSame(resolved1, resolved2);
            Assert.AreNotSame(resolved1, resolved3);
            Assert.AreNotSame(resolved2, resolved3);
        }
    }
}
