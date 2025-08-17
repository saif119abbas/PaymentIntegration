
namespace PaymentIntegration.Dtos
{
    public class ExecutePaymentSessionDto
    {
        public decimal InvoiceValue { get; set; } = 0;
        public string SessionId { get; set; } = "";
        public string CallBackUrl { get; set; } = "http://localhost:3000/payment";
        public string ErrorUrl { get; set; } = "http://localhost:3000/payment";
    }
}
