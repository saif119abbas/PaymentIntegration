
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PaymentIntegration.Dtos
{
    public class ChallengeDto
    {

        /// <summary>
        /// This is challenge.id, obtained from the Create Transaction Request response.
        /// Only useful if status == "INITIATED".
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }= default!;

        /// <summary>
        /// The answer to the challenge.
        /// </summary>
        [JsonProperty("answer")]
        public string Answer { get; set; } = default!;

        /// <summary>
        /// Optional reason code for REJECT answer (e.g., "CUST").
        /// </summary>
        [JsonProperty("reason_code")]
        public string ReasonCode { get; set; } = "";

        /// <summary>
        /// Optional additional description for REJECT answer.
        /// </summary>
        [JsonProperty("additional_information")]
        public string AdditionalInformation { get; set; } = "";
    }
}
