
using System.ComponentModel.DataAnnotations;

namespace PaymentIntegration.Dtos
{
    public class CreateInvoiceDto
    {
        [Required(ErrorMessage ="Please provide the notification option")]
       
        public string NotificationOption { get; set; } = "LNK";
        [Required(ErrorMessage = "Please provide the customer name")]
        public string? CustomerName { get; set; }
        [Required(ErrorMessage = "Please provide the invoice value")]
        public decimal InvoiceValue { get; set; }
        public string ?CallBackUrl { get; set; }
        public string? ErrorUrl { get; set; }
    }
}
