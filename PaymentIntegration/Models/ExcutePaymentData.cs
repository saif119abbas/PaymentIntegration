

namespace PaymentIntegration.Models
{
    public class ExcutePaymentData
    {
       public int InvoiceId { get; set; }
       public bool IsDirectPayment { get; set; }
       public string PaymentURL { get; set; } = "";
       public string ?CustomerReference { get; set; }
       public string? UserDefinedField { get; set; }
       public string? RecurringId { get; set; }
    }
}
