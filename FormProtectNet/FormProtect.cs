using System.Web;

namespace FormProtectNet
{
    /// <summary>
    /// Protect forms from abuse
    /// </summary>
    public class FormProtect
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FormProtect()
        {
            EmptyFieldName = "formfield1234567894";
        }

        /// <summary>
        /// The field name to use for the empty field that should not be filled out
        /// </summary>
        public string EmptyFieldName { get; set; }

        /// <summary>
        /// The ProjectHoneypot API key to use
        /// </summary>
        public string ProjectHoneypotApiKey { get; set; }

        /// <summary>
        /// Call this within the form tag to protect a form. Use the Verify function in the 
        /// form processing code to complete the form protection.
        /// </summary>
        /// <returns></returns>
        public HtmlString Protect()
        {
            var html = "";
            if (!string.IsNullOrEmpty(EmptyFieldName))
            {
                //Add html for empty field test
                html += string.Format(@"<span style=""display:none"">Leave this field empty: <input type=""text"" value="""" name=""{0}""></span>", EmptyFieldName);
            }
            return new HtmlString(html);
        }

        /// <summary>
        /// Test the form submission for indications that it is spam
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public FormProtectResult Verify(HttpRequestBase request)
        {
            var fpr = new FormProtectResult();
            if (!string.IsNullOrEmpty(EmptyFieldName))
            {
                //Run empty field test
                if (!string.IsNullOrEmpty(request[EmptyFieldName]))
                {
                    fpr.FailedEmptyField = true;
                    fpr.IsSpam = true;
                }
            }

            if (!string.IsNullOrEmpty(ProjectHoneypotApiKey))
            {
                //Run ProjectHoneypot test
                if (ProjectHoneypot.Lookup(request.UserHostAddress, ProjectHoneypotApiKey) == ProjectHoneypot.HttpBlLookupResult.OnBlacklist)
                {
                    fpr.FailedHttpBl = true;
                    fpr.IsSpam = true;
                }
            }
            return fpr;
        }
    }
}
