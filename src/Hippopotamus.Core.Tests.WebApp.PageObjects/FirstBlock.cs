using OpenQA.Selenium;

namespace Hippopotamus.Core.Tests.WebApp.PageObjects
{
    public class FirstBlock : Block
    {
        public FirstBlock(IBlock parent, By by)
            : base(parent, by)
        {
        }

        public Paragraph Paragraph => new Paragraph(this, By.TagName("p"));
    }
}
