
namespace FormProtectNet
{
    /// <summary>
    /// The result of the FormProtect tests
    /// </summary>
    public class FormProtectResult
    {
        /// <summary>
        /// Is the form submission spam
        /// </summary>
        public bool IsSpam { get; set; }

        /// <summary>
        /// Failed the empty field test?
        /// </summary>
        public bool FailedEmptyField { get; set; }

        /// <summary>
        /// Failed ProjectHoneypot Http blacklist test
        /// </summary>
        public bool FailedHttpBl { get; set; }
    }
}
