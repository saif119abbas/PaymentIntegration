
namespace PaymentIntegration.Models
{
    internal class InitiateSessionResponse
    {
        public bool IsSuccess { get; set; }
        public string Messgae { get; set; } = "";
        List<ValidationError> Errors { get; set; }=new List<ValidationError>();
        public string SessionId { get; set; } = "";
        public string CountryCode { get; set; } = "";
        public List<CustomerToken> CustomerTokens { get; set; }=new List<CustomerToken>();

    }
}
