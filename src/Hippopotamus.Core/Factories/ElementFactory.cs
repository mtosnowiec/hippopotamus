using System;
using OpenQA.Selenium;

namespace Hippopotamus.Core.Factories
{
    public static class ElementFactory
    {
        public static TElement Create<TElement>(
            IBlock parent,
            By specification)
            where TElement : IElement
        {
            return (TElement)Create(typeof(TElement), parent, specification);
        }

        public static object Create(
            Type elementToCreateType,
            IBlock parent,
            By specification)
        {
            if (elementToCreateType == null)
            {
                throw new ArgumentNullException(nameof(elementToCreateType));
            }

            if (parent == null)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            if (!typeof(IElement).IsAssignableFrom(elementToCreateType))
            {
                throw new ArgumentException($"Given type '{elementToCreateType.FullName}' is not assignable to '{typeof(IElement).FullName}'.", nameof(elementToCreateType));
            }

            var ctor = elementToCreateType.GetConstructor(new[] { typeof(IBlock), typeof(By) });

            if (ctor == null)
            {
                throw new ArgumentException($"Given type '{elementToCreateType.FullName}' does not have a constructor that takes two parameters of types '{typeof(IBlock).FullName}' and '{typeof(By).FullName}'.", nameof(elementToCreateType));
            }

            var createdElement = (IElement)ctor.Invoke(new object[] { parent, specification });

            createdElement.AfterCreated();

            return createdElement;
        }
    }
}
