
namespace PaymentIntegration.Models
{
    public class UpdateSessionResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
        public List<ValidationError> ValidationErrors { get; set; } = new List<ValidationError>();
        public UpdateSessionData? Data { get; set; }
    }
}
