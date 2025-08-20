namespace PaymentIntegration.Models
{
    public class FaceImage
    {
        public string url { get; set; }
        public string date { get; set; }
    }

    public class CreditRating
    {
        public string rating { get; set; }
        public string source { get; set; }
    }

    public class CreditLimit
    {
        public string currency { get; set; }
        public string amount { get; set; }
    }

    public class CustomerResponse
    {
        public string bank_id { get; set; }
        public string customer_id { get; set; }
        public string customer_number { get; set; }
        public string legal_name { get; set; }
        public string mobile_phone_number { get; set; }
        public string email { get; set; }
        public FaceImage face_image { get; set; }
        public string date_of_birth { get; set; }
        public string relationship_status { get; set; }
        public int dependants { get; set; }
        public List<string> dob_of_dependants { get; set; }
        public CreditRating credit_rating { get; set; }
        public CreditLimit credit_limit { get; set; }
        public string highest_education_attained { get; set; }
        public string employment_status { get; set; }
        public bool kyc_status { get; set; }
        public string last_ok_date { get; set; }
        public string title { get; set; }
        public string branch_id { get; set; }
        public string name_suffix { get; set; }
    }

}
