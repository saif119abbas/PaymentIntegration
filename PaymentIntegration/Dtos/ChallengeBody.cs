using System.ComponentModel.DataAnnotations;

namespace PaymentIntegration.Dtos
{
    public class ChallengeBody
    {
        [Required(ErrorMessage = "Id is required.")]
        public string Id { get; set; } = default!;
        [Required(ErrorMessage = "Answer is required.")]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "Answer must be between 3 or 4 characters.")]
        public string Answer { get; set; } = default!;

        [Required(ErrorMessage = "AccountId is required.")]
        public string AccountId { get; set; } = default!;

        [Required(ErrorMessage = "BankId is required.")]
        public string BankId { get; set; } = default!;

        [Required(ErrorMessage = "TransactionRequestId is required.")]

        public string TransactionRequestId { get; set; } = default!;
        public string ReasonCode { get; set; } = "";
        public string AdditionalInformation { get; set; } = "";
    }
}
