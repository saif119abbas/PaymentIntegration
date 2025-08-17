
namespace PaymentIntegration.Models
{
    public class InitiateSessionResponse
    {
        public bool IsSuccess { get; set; }
        public string Messgae { get; set; } = "";
        List<ValidationError> Errors { get; set; } = new List<ValidationError>();
        public InitiateSessionData Data= new InitiateSessionData();

    }
}
