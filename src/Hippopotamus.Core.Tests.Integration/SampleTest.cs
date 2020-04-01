using Hippopotamus.Core.Tests.WebApp.PageObjects;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Linq;

namespace Hippopotamus.Core.Tests.Integration
{
    public class SampleTest
    {
        private IWebHost _webHost = null;
        private string _hostBaseUri = null;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _webHost = new HostFactory().Create();
            _hostBaseUri = _webHost.ServerFeatures.Get<IServerAddressesFeature>().Addresses.First(x => x.StartsWith("http://"));
        }

        [SetUp]
        public void Setup()
        {
        }

        [TearDown]
        public void TearDown()
        {
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _webHost?.Dispose();
        }

        [Test]
        public void ElementIsLocatedProperlyInsideParentBlock()
        {
            using (var session = new SessionFactory().Create())
            {
                var webAppPage = session.NavigateTo<WebAppPage>(_hostBaseUri);
                var paragraphText = webAppPage.FirstBlock.Paragraph.Text;
                Assert.AreEqual("hipPOPotamus says hello!", paragraphText);
            }
        }

        [Test]
        public void NavigateToIsWaitingForPageReponse()
        {
            var delayRespose = TimeSpan.FromSeconds(5);

            using (var session = new SessionFactory().Create())
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();

                var webAppPage = session.NavigateTo<WebAppPage>($"{_hostBaseUri}/?delayResposeInMilliseconds={delayRespose.TotalMilliseconds}");

                stopWatch.Stop();
                var navigateToElapsedTime = stopWatch.Elapsed;

                Assert.IsTrue(navigateToElapsedTime >= delayRespose);
            }
        }
    }
}
