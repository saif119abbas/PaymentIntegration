
namespace PaymentIntegration.Dtos
{

    public class ExecutePaymentDto
    {
        public decimal InvoiceValue { get; set; } = 0;
        public int PaymentMethodId { get; set; } = 20; // for testing set to 20
        public string CallBackUrl { get; set; } = "http://localhost:3000/payment";
        public string ErrorUrl { get; set; } = "http://localhost:3000/payment";

    }
}
