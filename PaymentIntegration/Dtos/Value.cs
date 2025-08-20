using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PaymentIntegration.Dtos
{
    public class Value
    {
        [JsonProperty("amount")]
        [Required(ErrorMessage = "Amount is required")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Amount must be a positive number with up to 2 decimal places")]
        [Range(0.01, 1000000, ErrorMessage = "Amount must be between 0.01 and 1,000,000")]
        public string Amount { get; set; } = "";

        [JsonProperty("currency")]
        [Required(ErrorMessage = "Currency is required")]
        [RegularExpression("^(EUR|USD|ILS|JOD)$", ErrorMessage = "Currency must be EUR, USD, ILS, or JOD")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency code must be exactly 3 characters")]
        public string Currency { get; set; } = "";

        public bool IsAmountValidForCurrency()
        {
            if (!decimal.TryParse(Amount, out decimal amountValue))
                return false;

            // Currency-specific validation
            switch (Currency)
            {
                case "JOD":
                    // Jordanian Dinar typically has 3 decimal places
                    return Regex.IsMatch(Amount, @"^\d+(\.\d{1,3})?$");
                case "ILS":
                    // Israeli Shekel has 2 decimal places
                    // Additional validation for minimum amount
                    return amountValue >= 1.00m;
                default:
                    // EUR, USD - standard validation
                    return true;
            }
        }
        public decimal GetDecimalAmount()
        {
            if (decimal.TryParse(Amount, out decimal result))
            {
                return result;
            }
            return 0m;
        }

        // Custom validation for specific currency requirements
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            // Check if amount has proper decimal places for currency
            if (Currency == "JOD" && Amount.Contains('.') && Amount.Split('.')[1].Length > 3)
            {
                results.Add(new ValidationResult("JOD currency requires maximum 3 decimal places",
                    new[] { nameof(Amount) }));
            }

            // Check minimum amount for ILS
            if (Currency == "ILS" && GetDecimalAmount() < 1.00m)
            {
                results.Add(new ValidationResult("Minimum amount for ILS is 1.00",
                    new[] { nameof(Amount) }));
            }

            return results;
        }
    }
}