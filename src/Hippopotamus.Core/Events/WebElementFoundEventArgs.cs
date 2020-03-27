using OpenQA.Selenium;

namespace Hippopotamus.Core.Events
{
    public class WebElementFoundEventArgs : WebElementFindingEventArgs
    {
        public IWebElement WebElement { get; set; }
    }
}
