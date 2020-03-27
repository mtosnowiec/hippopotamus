using System;
using OpenQA.Selenium;

namespace Hippopotamus.Core
{
    public abstract class Element
    {
        protected Element(Block parent, By by)
        {
            Parent = parent;
            Specification = by;
            AccessibilityLevel = AccessibilityLevel.Visible;
        }

        public AccessibilityLevel AccessibilityLevel { get; set; }

        public bool Exists()
        {
            return Parent.FindElementImmediately(GetFindOptions()) != null;
        }

        public Page Page
        {
            get
            {
                Element currentElement = this;

                while (currentElement.Parent != null)
                {
                    currentElement = currentElement.Parent;
                }

                var page = currentElement as Page;

                if (page == null)
                {
                    throw new Exception("Element musi mieć Page jako swojego pierwotnego przodka");
                }

                return page;
            }
        }

        public Block Parent { get; }

        public virtual Session Session => Page.Session;

        public virtual IWebElement Tag => Parent.FindElement(GetFindOptions());

        public virtual string Text => Tag.Text;

        protected By Specification { get; }

        private FindOptions GetFindOptions()
        {
            return new FindOptions
            {
                AccessibilityLevel = AccessibilityLevel,
                Specification = Specification
            };
        }

        public virtual void AfterCreated()
        {
        }
    }
}
