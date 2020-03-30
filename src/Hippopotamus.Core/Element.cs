using System;
using OpenQA.Selenium;

namespace Hippopotamus.Core
{
    public abstract class Element : IElement
    {
        private protected Element(By specification)
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            this.Specification = specification;
        }

        protected Element(IBlock parent, By specification)
            : this(specification)
        {
            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            this.Parent = parent;
        }

        public AccessibilityLevel AccessibilityLevel { get; set; }

        public bool Exists()
        {
            return Parent.FindElementImmediately(GetFindOptions()) != null;
        }

        public IPage Page
        {
            get
            {
                IElement currentElement = this;

                while (currentElement.Parent != null)
                {
                    currentElement = currentElement.Parent;
                }

                var page = currentElement as IPage;

                if (page == null)
                {
                    throw new Exception("Element musi mieć Page jako swojego pierwotnego przodka");
                }

                return page;
            }
        }

        public IBlock Parent { get; }

        public virtual ISession Session => Page.Session;

        public virtual IWebElement Tag => Parent.FindElement(GetFindOptions());

        public virtual string Text => Tag.Text;

        protected By Specification { get; }

        private IFindOptions GetFindOptions()
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
