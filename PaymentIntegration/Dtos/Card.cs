using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PaymentIntegration.Dtos
{
    public class Card
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
        [CreditCard(ErrorMessage = "Invalid card number")]
        [RegularExpression("^[0-9]{13,19}$", ErrorMessage = "Card number must be 13-19 digits")]
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

    
        public bool IsExpired()
        {
            if (int.TryParse(ExpiryYear, out int year) &&
                int.TryParse(ExpiryMonth, out int month))
            {
                var currentDate = DateTime.Now;
                var expiryDate = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);
                return currentDate > expiryDate;
            }
            return true; 
        }
        public static bool IsValidCardNumber(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                return false;

 
            cardNumber = Regex.Replace(cardNumber, @"[^\d]", "");

            int sum = 0;
            bool alternate = false;
            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                if (!char.IsDigit(cardNumber[i]))
                    return false;

                int digit = cardNumber[i] - '0';
                if (alternate)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit = (digit % 10) + 1;
                    }
                }
                sum += digit;
                alternate = !alternate;
            }
            return (sum % 10 == 0);
        }
    }
}