
namespace PaymentIntegration.Models
{
    public class CreateInvoiceResponse
    {
        public bool IsSuccess {  get; set; }
        public string ?Message { get; set; }
        public string? ValidationErrors { get; set; } = "";
        public InvoiceData? Data { get; set; }
    }
}
