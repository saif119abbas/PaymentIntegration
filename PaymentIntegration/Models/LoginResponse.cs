using Newtonsoft.Json;

namespace PaymentIntegration.Models
{
    public class LoginResponse
    {
        [JsonProperty("IsSuccess", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsSuccess { get; set; }

        [JsonProperty("Messgae", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        [JsonProperty("token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }
        public bool LoginSuccessful => IsSuccess ?? !string.IsNullOrEmpty(Token);
    }
}