using System.Text.Json.Serialization;
namespace PaymentIntegration.Models
{


    public class CustomerResponseForBank
    {
        [JsonPropertyName("customers")]
        public List<Customer> Customers { get; set; }
    }

    public class Customer
    {
        [JsonPropertyName("bank_id")]
        public string BankId { get; set; }

        [JsonPropertyName("customer_id")]
        public Guid CustomerId { get; set; }

        [JsonPropertyName("customer_number")]
        public string CustomerNumber { get; set; }

        [JsonPropertyName("legal_name")]
        public string LegalName { get; set; }

        [JsonPropertyName("mobile_phone_number")]
        public string MobilePhoneNumber { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("face_image")]
        public FaceImage FaceImage { get; set; }

        [JsonPropertyName("date_of_birth")]
        public DateTime DateOfBirth { get; set; }

        [JsonPropertyName("relationship_status")]
        public string RelationshipStatus { get; set; }

        [JsonPropertyName("dependants")]
        public int Dependants { get; set; }

        [JsonPropertyName("dob_of_dependants")]
        public List<DateTime> DobOfDependants { get; set; }

        [JsonPropertyName("credit_rating")]
        public CreditRating CreditRating1 { get; set; }

        [JsonPropertyName("credit_limit")]
        public CreditLimit CreditLimit1 { get; set; }

        [JsonPropertyName("highest_education_attained")]
        public string HighestEducationAttained { get; set; }

        [JsonPropertyName("employment_status")]
        public string EmploymentStatus { get; set; }

        [JsonPropertyName("kyc_status")]
        public bool KycStatus { get; set; }

        [JsonPropertyName("last_ok_date")]
        public DateTime LastOkDate { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("branch_id")]
        public string BranchId { get; set; }

        [JsonPropertyName("name_suffix")]
        public string NameSuffix { get; set; }


        public class CreditRating
        {
            [JsonPropertyName("rating")]
            public string Rating { get; set; }

            [JsonPropertyName("source")]
            public string Source { get; set; }
        }

        public class CreditLimit
        {
            [JsonPropertyName("currency")]
            public string Currency { get; set; }

            [JsonPropertyName("amount")]
            public string Amount { get; set; }
        }
    }
}

