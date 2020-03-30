using OpenQA.Selenium;

namespace Hippopotamus.Core
{
    public interface ISession
    {
        IWebDriver Driver { get; }

        void End();

        TPage NavigateTo<TPage>(string url)
            where TPage : IPage;

        TPage CurrentPageAs<TPage>()
            where TPage : IPage;
    }
}