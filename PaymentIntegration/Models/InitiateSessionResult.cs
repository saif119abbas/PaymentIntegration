namespace PaymentIntegration.Models
{
    public class InitiateSessionResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<ValidationError> ValidationErrors { get; set; }
        public ResponseData Data { get; set; }
    }
}
