# FormProtect.Net
Spam protection for .Net forms

This project was inspired by [CFFormProtect](http://cfformprotect.riaforge.org/). 
Currently only two tests are implemented: a hidden form field and ProjectHoneypot. 
These two tests are surprisingly effective in stopping most spam, however.

## Usage

### Step 1: Instantiate FormProtect object:

    var formProtect = new FormProtectNet.FormProtect
    {
        ProjectHoneypotApiKey = ConfigurationManager.AppSettings["ProjectHoneypotApiKey"]
    };

See [Project Honey Pot](http://www.projecthoneypot.org) for more information about getting 
an API key.

### Step 2: Call FormProtect.Protect() within your form to add necessary html:

    @formProtect.Protect()

### Step 3: Call FormProtect.Verify() when the form is submitted to test for spam

    var formProtectResult = formProtect.Verify(Request);
    if (formProtectResult.IsSpam)
    {
        // Ignore or log the request
    }
    else
    {
        // Process the request
    }
