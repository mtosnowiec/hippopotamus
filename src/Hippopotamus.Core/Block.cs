using System;
using System.Collections.Generic;
using System.Linq;
using Hippopotamus.Core.Events;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Hippopotamus.Core
{
    public abstract class Block : Element
    {
        public static event WebElementFindingEventHandler WebElementFinding;

        public static event WebElementFoundEventHandler WebElementFound;

        public static readonly TimeSpan DefaultTimeout = TimeSpan.FromTicks(3000);

        protected Block(Block parent, By by, TimeSpan? timeout = null)
            : base(parent, by)
        {
            WaitTimeout = timeout ?? parent?.WaitTimeout ?? DefaultTimeout;
        }

        public TimeSpan WaitTimeout { get; set; }

        public virtual IWebElement FindElement(FindOptions findOptions)
        {
            WebElementFinding?.Invoke(this, new WebElementFindingEventArgs { FindOptions = findOptions });

            var element = Wait.Until(FindElementWithCondition(findOptions));

            return element;
        }

        private Func<IWebDriver, IWebElement> FindElementWithCondition(FindOptions findOptions)
        {
            return (driver) =>
            {
                try
                {
                    var webElement = Tag.FindElement(findOptions.Specification);
                    var expectedCondition = CreateCondition(findOptions.AccessibilityLevel);

                    if (webElement != null && expectedCondition(webElement))
                    {
                        WebElementFound?.Invoke(this, new WebElementFoundEventArgs { FindOptions = findOptions, WebElement = webElement });

                        return webElement;
                    }

                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            };
        }

        private Func<IWebElement, bool> CreateCondition(AccessibilityLevel level)
        {
            switch (level)
            {
                case AccessibilityLevel.Visible: return x => x.Displayed;
                case AccessibilityLevel.Exist: return x => true;
                case AccessibilityLevel.Clickable: return x => x.Displayed && x.Enabled;
                default:
                    throw new NotImplementedException($"Brak obsługi wartości: {level}");
            }
        }

        public virtual IWebElement FindElementImmediately(FindOptions findOptions)
        {
            try
            {
                WebElementFinding?.Invoke(this, new WebElementFindingEventArgs { FindOptions = findOptions });

                var webElement = new WebDriverWait(Session.Driver, TimeSpan.Zero)
                    .Until(FindElementWithCondition(findOptions));

                return webElement;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual IEnumerable<OrderedWebElement> FindElements(FindOptions findOptions)
        {
            var elements = Wait.Until(driver =>
            {
                try
                {
                    var expectedCondition = CreateCondition(findOptions.AccessibilityLevel);
                    var foundOrderedWebElements = Tag.FindElements(findOptions.Specification)
                        .Select((webElement, index) => new OrderedWebElement
                        {
                            WebElement = webElement,
                            Index = index
                        })
                        .Where((orderedWebElement, index) => orderedWebElement != null && expectedCondition(orderedWebElement.WebElement))
                        .ToList();

                    return findOptions.AllowZeroElements
                        ? foundOrderedWebElements
                        : foundOrderedWebElements.Count > 0
                            ? foundOrderedWebElements
                            : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            return elements;
        }

        protected WebDriverWait Wait => new WebDriverWait(Session.Driver, WaitTimeout);
    }
}
