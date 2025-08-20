# Payment Integration

A service to help you integrate **online payments** through the [MyFatoorah](https://www.myfatoorah.com/) platform.

---

## **Features**
- Pay through a payment link.
- Integrate direct payment that allows you to control the front-end  ui.

---

## **Getting Started**
### 1. For a visa card payment
  - Call the `ExecutePayment` function from your controller as shown in the following code (set `PaymentMethodId` to 20), if the user needs to save the token, set `SaveToken` to true.
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
         /*  if (!IsValidMyFatoorahUrl(result.Data.PaymentURL))
           {
               throw new Exception("Invalid payment URL received from payment gateway");
           }*/

           return Ok(result);
       }
       catch (Exception ex)
       {
           _logger.LogError(ex, "Payment failed");
           return BadRequest(ex.Message);
       }
   }```
    
  - If the result is successful, store the `paymentURL` and call `DirectPayment` as follows:
    ```csharp
            [HttpPost]
        [ProducesResponseType(typeof(DirectPaymentResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ExecutePaymentResponse>> DirectPayment([FromBody] DirectPaymentDto directPaymentDto,string url)
        {
            try
            {
                var rawResponse = await _paymentService.DirectPayment(directPaymentDto);
                _logger.LogInformation("RAW API RESPONSE: {Response}", rawResponse);
                var result = JsonConvert.DeserializeObject<ExecutePaymentResponse>(rawResponse)!;
               /* if (!IsValidMyFatoorahUrl(result.Data.PaymentURL))
                {
                    throw new Exception("Invalid payment URL received from payment gateway");
                }*/

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Payment failed");
                return BadRequest(ex.Message);
            }
        }
    ```
    - If the user selects to save their card, the response will return the card token. We should store it with the user profile so he can use it, and next time we need to send the token and CVV of the card
### 2. For Google Pay, we need to contact to account manager to activate it.
### 3. For Apple Pay, we need to contact to account manager to activate it, and validate our domain.





