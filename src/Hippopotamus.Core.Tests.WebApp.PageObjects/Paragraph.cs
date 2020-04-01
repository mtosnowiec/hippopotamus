using OpenQA.Selenium;

namespace Hippopotamus.Core.Tests.WebApp.PageObjects
{
    public class Paragraph : Element
    {
        public Paragraph(IBlock parent, By specification)
            : base(parent, specification)
        {
        }
    }
}
