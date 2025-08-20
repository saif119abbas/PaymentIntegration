using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace PaymentIntegration.Dtos
{
    public class TransactionRequestDto
    {
        [JsonProperty("card")]
        [Required(ErrorMessage = "Card is required")]
        public Card Card { get; set; }=new Card();

        [JsonProperty("to")]
        [Required(ErrorMessage = "Destination is required")]
        public Destination To { get; set; } = new Destination();

        [JsonProperty("value")]
        [Required(ErrorMessage = "The amount and the currency of the payment are required")]
        public Value Value { get; set; } = new Value();

        [JsonProperty("description")]
        [Required(ErrorMessage = "Description is required")]
        [StringLength(1, MinimumLength = 2000, ErrorMessage = "The maximum length is 2000")]
        public string Description { get; set; } = "";

    }
}
