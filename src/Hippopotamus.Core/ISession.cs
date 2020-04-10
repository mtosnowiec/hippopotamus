using OpenQA.Selenium;
using System;

namespace Hippopotamus.Core
{
    public interface ISession : IDisposable
    {
        IWebDriver Driver { get; }

        void End();

        TPage NavigateTo<TPage>(string url)
            where TPage : IPage;

        TPage CurrentPageAs<TPage>()
            where TPage : IPage;
    }
}