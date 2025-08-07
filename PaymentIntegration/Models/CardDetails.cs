
namespace PaymentIntegration.Models
{
    public class CardDetails
    {
        public string NameOnCard { get; set; }
        public string Number { get; set; }
        public string PanHash { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string Brand { get; set; }
        public string Issuer { get; set; }
        public string IssuerCountry { get; set; }
        public string FundingMethod { get; set; }

    }
}
