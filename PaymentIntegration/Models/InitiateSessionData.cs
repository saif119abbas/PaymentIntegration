
namespace PaymentIntegration.Models
{
    public class InitiateSessionData
    {
        public string SessionId { get; set; } = "";
        public string CountryCode { get; set; } = "";
        public List<CustomerToken> CustomerTokens { get; set; } = new List<CustomerToken>();
    }
}
