using System;
using OpenQA.Selenium;

namespace Hippopotamus.Core
{
    public abstract class Page : Block, IPage
    {
        protected Page(
            ISession session,
            TimeSpan timeout)
            : this(
                  session,
                  By.TagName("body"),
                  timeout)
        {
        }

        private Page(
            ISession session,
            By by,
            TimeSpan timeout)
            : base(by)
        {
            this.Session = session;
            this.WaitTimeout = timeout;
        }

        public override IWebElement Tag =>
            Session.Driver
                .SwitchTo()
                .DefaultContent()
                .FindElement(this.Specification);

        public override ISession Session { get; }
    }
}
