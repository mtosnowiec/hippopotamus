using OpenQA.Selenium;

namespace Hippopotamus.Core
{
    public class OrderedWebElement
    {
        public IWebElement WebElement { get; set; }

        public int Index { get; set; }
    }
}
