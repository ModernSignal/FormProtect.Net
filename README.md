# FormProtect.Net
Spam protection for .Net forms

This project was inspired by [CFFormProtect](http://cfformprotect.riaforge.org/). 
Currently only two tests are implemented: a hidden form field and ProjectHoneypot. 
These two tests are surprisingly effective in stopping most spam, however.

Available on [nuget.org](https://www.nuget.org/packages/FormProtectNet).

## Tests Implemented

- Hidden form field
- Project Honey Pot HTTP Blacklist check (https://www.projecthoneypot.org/services_overview.php)

## Tests to Consider for Future Implementation

- Time difference: set minimum and maximum amount of time for form to be submitted.
This would catch bots that submit inhumanly fast and also bots that cache submitted data.
- Spam strings: search for specific strings in the request to block. 
Not an ideal way to stop spam, but can be used in a pinch to stop particular
annoying messages if not caught by other methods.

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
