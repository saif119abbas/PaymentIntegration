
namespace PaymentIntegration.Models
{
    public class Entitlement
    {
        public string entitlement_id { get; set; }
        public string role_name { get; set; }
        public string bank_id { get; set; }
    }

    public class Entitlements
    {
        public List<Entitlement> list { get; set; }
    }

    public class CreateUserResponse
    {
        public string user_id { get; set; }
        public string email { get; set; }
        public string provider_id { get; set; }
        public string provider { get; set; }
        public string username { get; set; }
        public Entitlements entitlements { get; set; }
    }

}
