using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace Hippopotamus.Core
{
    public interface IBlock : IElement
    {
        IWebElement FindElement(IFindOptions findOptions);

        IWebElement FindElementImmediately(IFindOptions findOptions);

        IEnumerable<OrderedWebElement> FindElements(IFindOptions findOptions);

        TimeSpan FindTimeout { get; set; }
    }
}
