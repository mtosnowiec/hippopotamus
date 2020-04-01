using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Hippopotamus.Core.Tests.Integration
{
    public class WebDriverFactory
    {
        public IWebDriver Create()
        {
            var options = new ChromeOptions();
            options.AddArgument("--no-sandbox");
            // options.AddArgument("--headless");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--ignore-certificate-errors");

            var webDriver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options);

            return webDriver;
        }
    }
}
