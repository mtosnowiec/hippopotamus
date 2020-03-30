using OpenQA.Selenium;

namespace Hippopotamus.Core
{
    public interface IElement
    {
        AccessibilityLevel AccessibilityLevel { get; set; }

        bool Exists();

        IPage Page { get; }

        IBlock Parent { get; }

        ISession Session { get; }

        IWebElement Tag { get; }

        string Text { get; }

        void AfterCreated();
    }
}
