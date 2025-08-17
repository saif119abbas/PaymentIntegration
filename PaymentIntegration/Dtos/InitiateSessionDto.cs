
namespace PaymentIntegration.Dtos
{
    public class InitiateSessionDto
    {
        public string CustomerIdentifier { get; set; } = "";
        public bool  SaveToken { get; set; } = false;
        public bool IsRecurring { get; set; } = false;
    }
}
