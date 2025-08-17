
namespace PaymentIntegration.Dtos
{
    public class CardDto
    {
        public string Number { get; set; } = "";
        public string ExpiryMonth { get; set; } = "";
        public string ExpiryYear { get; set; } = "";
        public string SecurityCode { get; set; } = "";
        public string HolderName { get; set; } = "";
    }
}
