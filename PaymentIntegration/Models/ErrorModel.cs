using Newtonsoft.Json;

namespace PaymentIntegration.Models
{
    public class ErrorModel
    {
      /*  [JsonProperty("IsSuccess")]
        public bool IsSuccess { get; set; } = false;*/
        [JsonProperty("code")]
        public string Code { get; set; } = "";
        [JsonProperty("message")]
        public string Message { get; set; } = "";
    }
}
