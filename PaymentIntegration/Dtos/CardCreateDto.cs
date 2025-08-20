using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PaymentIntegration.Dtos
{
    public class CardRequestDto
    {
        [JsonProperty("card_number")]
        [Required(ErrorMessage = "Card number is required")]
        [CreditCard(ErrorMessage = "Invalid card number")]
        [RegularExpression("^[0-9]{13,19}$", ErrorMessage = "Card number must be 13-19 digits")]
        public string CardNumber { get; set; } = "";

        [JsonProperty("card_type")]
        [Required(ErrorMessage = "Card type is required")]
        [RegularExpression("^(Credit|Debit|Prepaid)$", ErrorMessage = "Card type must be Credit, Debit, or Prepaid")]
        public string CardType { get; set; } = "";

        [JsonProperty("name_on_card")]
        [Required(ErrorMessage = "Name on card is required")]
        [StringLength(100, ErrorMessage = "Name must be less than 100 characters")]
        public string NameOnCard { get; set; } = "";

        [JsonProperty("issue_number")]
        [StringLength(10, ErrorMessage = "Issue number must be less than 10 characters")]
        public string IssueNumber { get; set; } = "";

        [JsonProperty("serial_number")]
        [StringLength(20, ErrorMessage = "Serial number must be less than 20 characters")]
        public string SerialNumber { get; set; } = "";

        [JsonProperty("valid_from_date")]
        [Required(ErrorMessage = "Valid from date is required")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}Z$",
            ErrorMessage = "Valid from date must be in ISO 8601 format (YYYY-MM-DDTHH:MM:SSZ)")]
        public string ValidFromDate { get; set; } = "";

        [JsonProperty("expires_date")]
        [Required(ErrorMessage = "Expiry date is required")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}Z$",
            ErrorMessage = "Expiry date must be in ISO 8601 format (YYYY-MM-DDTHH:MM:SSZ)")]
        public string ExpiresDate { get; set; } = "";

        [JsonProperty("enabled")]
        public bool Enabled { get; set; } = true;

        [JsonProperty("technology")]
        [StringLength(50, ErrorMessage = "Technology must be less than 50 characters")]
        public string Technology { get; set; } = "";

        [JsonProperty("networks")]
        public List<string> Networks { get; set; } = new List<string>();

        [JsonProperty("allows")]
        [Required(ErrorMessage = "Allowed operations are required")]
        [MinLength(1, ErrorMessage = "At least one allowed operation must be specified")]
        public List<string> Allows { get; set; } = new List<string>();

        [JsonProperty("account_id")]
        [Required(ErrorMessage = "Account ID is required")]
        [RegularExpression(@"^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$",
            ErrorMessage = "Account ID must be a valid GUID")]
        public string AccountId { get; set; } = "";

        [JsonProperty("replacement")]
        [Required(ErrorMessage = "Replacement information is required")]
        public ReplacementInfo Replacement { get; set; }=new ReplacementInfo();

        [JsonProperty("pin_reset")]
        [Required(ErrorMessage = "PIN reset information is required")]
        [MinLength(1, ErrorMessage = "At least one PIN reset record is required")]
        public List<PinReset> PinResets { get; set; } = new List<PinReset>();

        [JsonProperty("collected")]
        [Required(ErrorMessage = "Collection date is required")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}Z$",
            ErrorMessage = "Collection date must be in ISO 8601 format (YYYY-MM-DDTHH:MM:SSZ)")]
        public string Collected { get; set; } = "";

        [JsonProperty("posted")]
        [Required(ErrorMessage = "Posting date is required")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}Z$",
            ErrorMessage = "Posted date must be in ISO 8601 format (YYYY-MM-DDTHH:MM:SSZ)")]
        public string Posted { get; set; } = "";

        [JsonProperty("customer_id")]
        [Required(ErrorMessage = "Customer ID is required")]
        [RegularExpression(@"^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$",
            ErrorMessage = "Customer ID must be a valid GUID")]
        public string CustomerId { get; set; } = "";

        [JsonProperty("brand")]
        [Required(ErrorMessage = "Brand is required")]
        [RegularExpression("^(Visa|MasterCard|American Express|Discover)$",
            ErrorMessage = "Brand must be Visa, MasterCard, American Express, or Discover")]
        public string Brand { get; set; } = "";

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            // Validate date formats and relationships
            ValidateDate(ValidFromDate, "Valid from date", nameof(ValidFromDate), results);
            ValidateDate(ExpiresDate, "Expiry date", nameof(ExpiresDate), results);
            ValidateDate(Collected, "Collected date", nameof(Collected), results);
            ValidateDate(Posted, "Posted date", nameof(Posted), results);
            ValidateDate(Replacement?.RequestedDate!, "Replacement requested date", "Replacement.RequestedDate", results);

            foreach (var pinReset in PinResets)
            {
                ValidateDate(pinReset.RequestedDate, "PIN reset requested date", "PinResets.RequestedDate", results);
            }

            // Validate expiry is after valid from
            if (DateTime.TryParse(ValidFromDate, out DateTime validFrom) &&
                DateTime.TryParse(ExpiresDate, out DateTime expires) &&
                expires <= validFrom)
            {
                results.Add(new ValidationResult("Expiry date must be after valid from date",
                    new[] { nameof(ExpiresDate) }));
            }

            return results;
        }

        private void ValidateDate(string dateString, string fieldName, string propertyName, List<ValidationResult> results)
        {
            if (!DateTime.TryParse(dateString, out _))
            {
                results.Add(new ValidationResult($"{fieldName} must be a valid ISO 8601 date",
                    new[] { propertyName }));
            }
        }
    }

    public class ReplacementInfo
    {
        [JsonProperty("requested_date")]
        [Required(ErrorMessage = "Requested date is required")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}Z$",
            ErrorMessage = "Requested date must be in ISO 8601 format (YYYY-MM-DDTHH:MM:SSZ)")]
        public string RequestedDate { get; set; } = "";

        [JsonProperty("reason_requested")]
        [Required(ErrorMessage = "Reason requested is required")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ReplacementReason ReasonRequested { get; set; }
    }

    public class PinReset
    {
        [JsonProperty("requested_date")]
        [Required(ErrorMessage = "Requested date is required")]
        [RegularExpression(@"^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}Z$",
            ErrorMessage = "Requested date must be in ISO 8601 format (YYYY-MM-DDTHH:MM:SSZ)")]
        public string RequestedDate { get; set; } = "";

        [JsonProperty("reason_requested")]
        [Required(ErrorMessage = "Reason requested is required")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PinResetReason ReasonRequested { get; set; }
    }

    public enum ReplacementReason
    {
        RENEW,
        LOST,
        STOLEN,
        DAMAGED
    }

    public enum PinResetReason
    {
        FORGOT,
        GOOD_SECURITY_PRACTICE,
        SUSPECTED_FRAUD
    }
}