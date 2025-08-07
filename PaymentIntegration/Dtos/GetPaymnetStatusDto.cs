using System.ComponentModel.DataAnnotations;
namespace PaymentIntegration.Dtos
{
    public class GetPaymnetStatusDto
    {
        [Required(ErrorMessage ="Please provide the key")]
        public string Key { get; set; } = "";
        [Required(ErrorMessage = "Please provide the key type")]
        public string KeyType { get; set; } = "PaymentId";

    }
}
