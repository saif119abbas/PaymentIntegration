

namespace PaymentIntegration.Models
{
    public class AnswerTransactionRequest
    {
        public string Id { get; set; } = default!;
        public string Type { get; set; } = default!;
        public FromAccount From { get; set; } = default!;
        public TransactionDetails Details { get; set; } = default!;
        public List<string> Transaction_Ids { get; set; } = new List<string>();
        public string Status { get; set; } = default!;
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public Challenge Challenge { get; set; } = default!;
        public Charge Charge { get; set; } = default!;
    }

    public class FromAccount
    {
        public string Bank_Id { get; set; } = default!;
        public string Account_Id { get; set; } = default!;
    }
    public class Charge
    {
        public string Summary { get; set; } = default!;
        public ValueObject Value { get; set; } = new ValueObject();
    }
 
}
