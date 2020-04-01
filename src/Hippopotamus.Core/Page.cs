using System;
using OpenQA.Selenium;

namespace Hippopotamus.Core
{
    public abstract class Page : Block, IPage
    {
        protected Page(ISession session)
            : base(By.TagName("body"))
        {
            this.Session = session;
            this.FindTimeout = TimeSpan.Zero;
        }

        public override IWebElement Tag =>
            Session.Driver
                .SwitchTo()
                .DefaultContent()
                .FindElement(this.Specification);

        public override ISession Session { get; }
    }
}
