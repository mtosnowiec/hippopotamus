namespace Hippopotamus.Core
{
    /// <summary>
    /// Specifies condition when a element of page is treated as found.
    /// </summary>
    public enum AccessibilityLevel
    {
        /// <summary>
        /// Element exist as node in DOM (Document Object Model).
        /// Element can be visible or not ("displayed: none", "hidden").
        /// </summary>
        Exist = 1,

        /// <summary>
        /// Element is visible on a page (possibly outside viewport and you have to scroll to it).
        /// Because this is usually desirable option, it is explicitly used as the default (value 0).
        /// </summary>
        Visible = 0,

        /// <summary>
        /// Element is visible and interactable (without "disabled" attribute).
        /// </summary>
        Clickable = 2,
    }
}
