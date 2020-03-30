using System;

namespace Hippopotamus.Core.Factories
{
    public static class PageFactory
    {
        public static TPage Create<TPage>(ISession session)
            where TPage : IPage
        {
            return (TPage)Create(typeof(TPage), session);
        }

        public static object Create(
            Type pageToCreatedType,
            ISession session)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            if (!typeof(IPage).IsAssignableFrom(pageToCreatedType))
            {
                throw new ArgumentException($"Given type {pageToCreatedType} is not assignable to '{typeof(IPage).FullName}'.", nameof(pageToCreatedType));
            }

            var ctor = pageToCreatedType.GetConstructor(new[] { typeof(ISession) });

            if (ctor == null)
            {
                throw new ArgumentException($"Given type '{pageToCreatedType.FullName}' does not have a constructor that takes one parameter of types '{typeof(ISession).FullName}'.");
            }

            var createdPage = ctor.Invoke(new object[] { session }) as IPage;

            createdPage.AfterCreated();

            return createdPage;
        }
    }
}
