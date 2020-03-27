using OpenQA.Selenium;

namespace Hippopotamus.Core
{
    /// <summary>
    /// Options used to find child element in parent.
    /// </summary>
    public class FindOptions
    {
        /// <summary>
        /// Specifies condition when a child element of page is treated as found.
        /// </summary>
        public AccessibilityLevel AccessibilityLevel { get; set; }

        public bool AllowZeroElements { get; set; }

        public By Specification { get; set; }
    }
}
