using OpenQA.Selenium;
using System;

namespace Hippopotamus.Core.Tests.WebApp.PageObjects
{
    public class WebAppPage : Page
    {
        public WebAppPage(ISession session)
            : base(session)
        {
        }

        public FirstBlock FirstBlock => new FirstBlock(this, By.Id("first-block"));
    }
}
