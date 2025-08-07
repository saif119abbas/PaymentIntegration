
namespace PaymentIntegration.Models
{
    public class InvoiceData
    {
        public int InvoiceId { get; set; }
        public string ?InvoiceURL {  get; set; }
        public string? CustomerReference { get; set; }
        public string? UserDefinedField { get; set; }
    }
}
