# Payment Integration

A service to help you integrate **online payments** through the [MyFatoorah](https://www.myfatoorah.com/) platform.

---

## **Features**
- Pay through a payment link.
- Integrate an embedded payment system.

---

## **Getting Started**

### 1. Create the Payment View
- Create a new view inside your project.
- You can copy the `EmbeddedPayment.cshtml` (or equivalent) from this project's `Views` folder.

---
### 2. Add the `InitiateSession` Method
  - Call the `InitiateSession` function from your controller.
  - If the result is successful, return the view you created.
```csharp
          public async Task<ActionResult<InitiateSessionResult>> InitiateSession( InitiateSessionDto initiateSessionDto)
    {

        try
        {
            var initiateSessionResponse = await _paymentService.InitiateSession(initiateSessionDto);
            var result = JsonConvert.DeserializeObject<InitiateSessionResult>(initiateSessionResponse);

            if (result == null)
            {
                return BadRequest("Failed to process payment initiation");
            }

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            ViewBag.SessionId = result.Data.SessionId;
            ViewBag.CountryCode = result.Data?.CountryCode;
            return View("EmbeddedPayment");
        }
        catch (Exception ex)
        {
            // Log the exception here
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request");
        }
    }

### 3. Add the `ExecutePayment` Endpoint
This endpoint will be called via AJAX from the payment view you created (In the AJAX section, make sure to add the correct path for the `ExecutePayment`.
```csharp
    [HttpPost]
    [ProducesResponseType(typeof(ExecutePaymentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ExecutePaymentResponse>> ExecutePayment([FromBody] ExecutePaymentDto executePaymentRequest)
    {
        try
        {
            var rawResponse = await _paymentService.ExecutePayment(executePaymentRequest);
            _logger.LogInformation("RAW API RESPONSE: {Response}", rawResponse);

            var result = JsonConvert.DeserializeObject<ExecutePaymentResponse>(rawResponse)!;

            // Validate the payment URL before returning it
            if (!IsValidMyFatoorahUrl(result.Data.PaymentURL))
            {
                throw new Exception("Invalid payment URL received from payment gateway");
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Payment failed");
            return BadRequest(ex.Message);
        }
    }
        ```
### 4. Payment URL Validation Helper
This method ensures the returned payment URL is safe and from the correct domain.
```csharp
private bool IsValidMyFatoorahUrl(string url)
{
    if (string.IsNullOrWhiteSpace(url))
        return false;

    var allowedDomains = new[]
    {
        "https://myfatoorah.com",
        "https://demo.myfatoorah.com",
        // Add any other allowed domains here
    };

    try
    {
        var uri = new Uri(url);

        // Validate domain
        if (!allowedDomains.Any(d => uri.AbsoluteUri.StartsWith(d, StringComparison.OrdinalIgnoreCase)))
            return false;

        // Validate path starts with expected pattern
        if (!uri.AbsolutePath.StartsWith("/En/KWT/PayInvoice/", StringComparison.OrdinalIgnoreCase))
            return false;

        // Validate required query parameters
        var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
        if (string.IsNullOrEmpty(query["paymentId"]) || string.IsNullOrEmpty(query["sessionId"]))
            return false;

        return true;
    }
    catch (UriFormatException)
    {
        return false;
    }
}

```


