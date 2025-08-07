namespace PaymentIntegration.Models
{
    public class GetPaymentStatusResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
        public object ValidationErrors { get; set; } = "";
        public PaymentStatusData? Data { get; set; }
    }
}
