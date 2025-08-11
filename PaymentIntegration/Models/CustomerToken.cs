
namespace PaymentIntegration.Models
{
    public class CustomerToken
    {
        public string Token { get; set; }
        public string CardNumber { get; set; }
        public string CardBrand { get; set; }
        public bool Is3DSVerified { get; set; }
    }
}
