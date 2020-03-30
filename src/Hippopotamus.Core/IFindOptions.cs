using OpenQA.Selenium;

namespace Hippopotamus.Core
{
    public interface IFindOptions
    {
        AccessibilityLevel AccessibilityLevel { get; }

        bool AllowZeroElements { get; }

        By Specification { get; }
    }
}