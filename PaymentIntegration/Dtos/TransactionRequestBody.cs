using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PaymentIntegration.Dtos
{
    public class TransactionRequestBody
    {
        [JsonProperty("card_type")]
        [Required(ErrorMessage = "Card type is required")]
        [RegularExpression("^(Credit|Debit|Prepaid)$", ErrorMessage = "Card type must be Credit, Debit, or Prepaid")]
        public string CardType { get; set; } = "";

        [JsonProperty("brand")]
        [Required(ErrorMessage = "Brand is required")]
        [RegularExpression("^(Visa|MasterCard|American Express|Discover)$",
            ErrorMessage = "Brand must be Visa, MasterCard, American Express, or Discover")]
        public string Brand { get; set; } = "";

        [JsonProperty("cvv")]
        [Required(ErrorMessage = "CVV is required")]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "CVV must be 3 or 4 digits")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "CVV must contain only numbers")]
        public string CVV { get; set; } = "";

        [JsonProperty("card_number")]
        [Required(ErrorMessage = "Card number is required")]
        //[CreditCard(ErrorMessage = "Invalid card number")]
        //[RegularExpression("^[0-9]{13,19}$", ErrorMessage = "Card number must be 13-19 digits")]
        public string CardNumber { get; set; } = "";

        [JsonProperty("name_on_card")]
        [Required(ErrorMessage = "Name on card is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
        [RegularExpression("^[a-zA-Z ]+$", ErrorMessage = "Name can only contain letters and spaces")]
        public string NameOnCard { get; set; } = "";

        [JsonProperty("expiry_year")]
        [Required(ErrorMessage = "Expiry year is required")]
        [RegularExpression("^20[2-9][0-9]$", ErrorMessage = "Expiry year must be in format YYYY and not expired")]
        public string ExpiryYear { get; set; } = "";

        [JsonProperty("expiry_month")]
        [Required(ErrorMessage = "Expiry month is required")]
        [RegularExpression("^(0[1-9]|1[0-2])$", ErrorMessage = "Expiry month must be between 01 and 12")]
        public string ExpiryMonth { get; set; } = "";
        [JsonProperty("amount")]
        [Required(ErrorMessage = "Amount is required")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Amount must be a positive number with up to 2 decimal places")]
        [Range(0.01, 1000000, ErrorMessage = "Amount must be between 0.01 and 1,000,000")]
        public string Amount { get; set; } = "";

        [JsonProperty("currency")]
        [Required(ErrorMessage = "Currency is required")]
        //[RegularExpression("^(EUR|USD|ILS|JOD)$", ErrorMessage = "Currency must be EUR, USD, ILS, or JOD")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency code must be exactly 3 characters")]
        public string Currency { get; set; } = "";
        [Required(ErrorMessage = "The employee should be defined")]
        public Guid? TipReceiverId { get; set; }

        [Required(ErrorMessage = "The phone number is required")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "The payment method is required")]
        public int PaymentMethod { get; set; }
    }
}
