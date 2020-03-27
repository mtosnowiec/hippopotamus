using System;
using OpenQA.Selenium;

namespace Hippopotamus.Core
{
    public abstract class Page : Block
    {
        protected Page(Session session, TimeSpan timeout) : this(session, By.TagName("body"), timeout)
        {
        }

        internal Page(Session session, By by, TimeSpan timeout)
            : base(null, by, timeout)
        {
            Session = session;
        }

        public override IWebElement Tag =>
            Session.Driver
                .SwitchTo()
                .DefaultContent()
                .FindElement(Specification);

        public override Session Session { get; }
    }
}
