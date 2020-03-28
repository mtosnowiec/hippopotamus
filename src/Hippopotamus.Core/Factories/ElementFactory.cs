using System;
using OpenQA.Selenium;

namespace Hippopotamus.Core.Factories
{
    public static class ElementFactory
    {
        public static TElement Create<TElement>(
            Block parent,
            By by)
            where TElement : Element
        {
            return (TElement)Create(typeof(TElement), parent, by);
        }

        public static object Create(
            Type elementToCreateType, 
            Block parent, 
            By by)
        {
            if (elementToCreateType == null)
            {
                throw new ArgumentNullException(nameof(elementToCreateType));
            }

            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            if (by == null)
            {
                throw new ArgumentNullException(nameof(by));
            }

            if (!typeof(Element).IsAssignableFrom(elementToCreateType))
            {
                throw new ArgumentException($"Given type '{elementToCreateType.FullName}' is not assignable to '{typeof(Element).FullName}'.", nameof(elementToCreateType));
            }

            var ctor = elementToCreateType.GetConstructor(new[] { typeof(Block), typeof(By) });

            if (ctor == null)
            {
                throw new ArgumentException($"Given type '{elementToCreateType.FullName}' does not have a constructor that takes two parameters of types '{typeof(Block).FullName}' and '{typeof(By).FullName}'.");
            }

            var createdElement = (Element)ctor.Invoke(new object[] { parent, by });

            createdElement.AfterCreated();

            return createdElement;
        }
    }
}
