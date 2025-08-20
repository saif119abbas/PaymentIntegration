
namespace PaymentIntegration.Dtos
{
    public class DirectPaymentBodyDto
    {
        public string PaymentType { get; set; } = "";
        public string Token { get; set; } = "";
        public bool SaveToken { get; set; } = true;
        public bool ByPass3D { get; set; } = true;
        //public CardDto Card { get; set; }
    }
}
