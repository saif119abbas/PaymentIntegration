
namespace PaymentIntegration.Models
{
    public class ResponseData
    {
        public string SessionId { get; set; }
        public string CountryCode { get; set; }
        public List<CustomerToken> CustomerTokens { get; set; }
    }
}
