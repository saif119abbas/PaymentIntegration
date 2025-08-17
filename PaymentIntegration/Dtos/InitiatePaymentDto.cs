
namespace PaymentIntegration.Models
{
    public class InitiatePaymentDto
    {
        public int InvoiceAmount { get; set; } = 0;
        public string? CurrencyIso { get; set; }="";

    }
}
