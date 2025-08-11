using Newtonsoft.Json;

namespace PaymentIntegration.Models
{
    public class ExecutePaymentResponse
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("validationErrors")]
        public List<ValidationError> ValidationErrors { get; set; }

        [JsonProperty("data")]
        public ExcutePaymentData Data { get; set; }
    }
}
