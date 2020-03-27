using System;
using Hippopotamus.Core.Factories;
using OpenQA.Selenium;

namespace Hippopotamus.Core
{
    public class Session : IDisposable
    {
        public Session(IWebDriver webDriver)
        {
            Driver = webDriver;
        }

        public virtual TPage NavigateTo<TPage>(string url)
            where TPage : Page
        {
            Driver.Navigate().GoToUrl(url);

            return CurrentPageAs<TPage>();
        }

        public virtual TPage CurrentPageAs<TPage>()
            where TPage : Page
        {
            return PageFactory.Create<TPage>(this);
        }

        public virtual void End()
        {
            if (Driver != null)
            {
                Driver.Quit();

                Driver.Dispose();

                Driver = null;
            }
        }

        ~Session()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        public virtual IWebDriver Driver { get; private set; }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                End();
            }
        }
    }
}
