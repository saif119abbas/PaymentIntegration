using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PaymentIntegration.Dtos
{
    public class Destination
    {
        [JsonProperty("counterparty_id")]
        [Required(ErrorMessage = "Counterparty Id is required")]
        public string CounterpartyId { get; set; } = "";
    }
}
