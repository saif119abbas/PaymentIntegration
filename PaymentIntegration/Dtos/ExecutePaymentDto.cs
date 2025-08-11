
namespace PaymentIntegration.Dtos
{

    public class ExecutePaymentDto
    {
        public decimal InvoiceValue { get; set; } = 0;
        public string SessionId { get; set; } = "";
        public string CallBackUrl { get; set; } = "";
        public string ErrorUrl { get; set; } = "";

    }
}
