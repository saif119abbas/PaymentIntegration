using Newtonsoft.Json;

namespace PaymentIntegration.Models
{
    public class Response
    {
        [JsonProperty("IsSuccess")]
        public bool IsSuccess { get; set; } = true;
        [JsonProperty("code")]
        public string Code { get; set; } = "";
        [JsonProperty("message")]
        public string Message { get; set; } = "";
    }
}
