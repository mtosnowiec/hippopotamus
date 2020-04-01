namespace Hippopotamus.Core.Tests.Integration
{
    public class SessionFactory
    {
        public ISession Create()
        {
            var webDriver = new WebDriverFactory().Create();
            var session = new Session(webDriver);
            
            return session;
        }
    }
}
