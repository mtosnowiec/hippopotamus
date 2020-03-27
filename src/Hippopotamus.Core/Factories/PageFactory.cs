using System;

namespace Hippopotamus.Core.Factories
{
    public static class PageFactory
    {
        public static TPage Create<TPage>(Session session)
            where TPage : Page
        {
            return (TPage)Create(typeof(TPage), session);
        }

        public static object Create(
            Type pageToCreatedType, 
            Session session)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }

            if (!typeof(Page).IsAssignableFrom(pageToCreatedType))
            {
                throw new ArgumentException($"Given type {pageToCreatedType} is not assignable to '{typeof(Page).FullName}'.", nameof(pageToCreatedType));
            }

            var ctor = pageToCreatedType.GetConstructor(new[] { typeof(Session) });

            if (ctor == null)
            {
                throw new ArgumentException($"Given type '{pageToCreatedType.FullName}' does not have a constructor that takes one parameter of types '{typeof(Session).FullName}'.");
            }

            var createdPage = ctor.Invoke(new object[] { session }) as Page;

            createdPage.AfterCreated();

            return createdPage;
        }
    }
}
