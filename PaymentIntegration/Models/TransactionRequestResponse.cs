using Newtonsoft.Json;

namespace PaymentIntegration.Dtos
{
    public class TransactionRequestResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("from")]
        public TransactionParty From { get; set; }

        [JsonProperty("details")]
        public TransactionDetails Details { get; set; }

        [JsonProperty("transaction_ids")]
        public List<string> TransactionIds { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; }

        [JsonProperty("end_date")]
        public DateTime EndDate { get; set; }

        [JsonProperty("challenges")]
        public List<Challenge> Challenges { get; set; }

        [JsonProperty("charge")]
        public TransactionCharge Charge { get; set; }

        [JsonProperty("attributes")]
        public List<TransactionAttribute> Attributes { get; set; }
    }

    public class TransactionParty
    {
        [JsonProperty("bank_id")]
        public string BankId { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }
    }

    public class TransactionDetails
    {
        [JsonProperty("to_sandbox_tan")]
        public SandboxTanRecipient ToSandboxTan { get; set; }

        [JsonProperty("to_sepa")]
        public SepaRecipient ToSepa { get; set; }

        [JsonProperty("to_counterparty")]
        public CounterpartyRecipient ToCounterparty { get; set; }

        [JsonProperty("to_simple")]
        public SimpleTransferRecipient ToSimple { get; set; }

        [JsonProperty("to_transfer_to_phone")]
        public PhoneTransfer ToTransferToPhone { get; set; }

        [JsonProperty("to_transfer_to_account")]
        public AccountTransfer ToTransferToAccount { get; set; }

        [JsonProperty("to_transfer_to_atm")]
        public AtmTransfer ToTransferToAtm { get; set; }

        [JsonProperty("to_sepa_credit_transfers")]
        public SepaCreditTransfer ToSepaCreditTransfers { get; set; }

        [JsonProperty("to_agent")]
        public AgentTransfer ToAgent { get; set; }

        [JsonProperty("value")]
        public MoneyValue Value { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }

    public class SandboxTanRecipient
    {
        [JsonProperty("bank_id")]
        public string BankId { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }
    }

    public class SepaRecipient
    {
        [JsonProperty("iban")]
        public string Iban { get; set; }
    }

    public class CounterpartyRecipient
    {
        [JsonProperty("counterparty_id")]
        public string CounterpartyId { get; set; }
    }

    public class SimpleTransferRecipient
    {
        [JsonProperty("otherBankRoutingScheme")]
        public string OtherBankRoutingScheme { get; set; }

        [JsonProperty("otherBankRoutingAddress")]
        public string OtherBankRoutingAddress { get; set; }

        [JsonProperty("otherBranchRoutingScheme")]
        public string OtherBranchRoutingScheme { get; set; }

        [JsonProperty("otherBranchRoutingAddress")]
        public string OtherBranchRoutingAddress { get; set; }

        [JsonProperty("otherAccountRoutingScheme")]
        public string OtherAccountRoutingScheme { get; set; }

        [JsonProperty("otherAccountRoutingAddress")]
        public string OtherAccountRoutingAddress { get; set; }

        [JsonProperty("otherAccountSecondaryRoutingScheme")]
        public string OtherAccountSecondaryRoutingScheme { get; set; }

        [JsonProperty("otherAccountSecondaryRoutingAddress")]
        public string OtherAccountSecondaryRoutingAddress { get; set; }
    }

    public class PhoneTransfer
    {
        [JsonProperty("value")]
        public MoneyValue Value { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("from")]
        public PhoneTransferParty From { get; set; }

        [JsonProperty("to")]
        public PhoneTransferParty To { get; set; }
    }

    public class PhoneTransferParty
    {
        [JsonProperty("mobile_phone_number")]
        public string MobilePhoneNumber { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }
    }

    public class AtmTransfer
    {
        [JsonProperty("value")]
        public MoneyValue Value { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("from")]
        public PhoneTransferParty From { get; set; }

        [JsonProperty("to")]
        public AtmRecipient To { get; set; }
    }

    public class AtmRecipient
    {
        [JsonProperty("legal_name")]
        public string LegalName { get; set; }

        [JsonProperty("date_of_birth")]
        public string DateOfBirth { get; set; }

        [JsonProperty("mobile_phone_number")]
        public string MobilePhoneNumber { get; set; }

        [JsonProperty("kyc_document")]
        public KycDocument KycDocument { get; set; }
    }

    public class KycDocument
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }
    }

    public class AccountTransfer
    {
        [JsonProperty("value")]
        public MoneyValue Value { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("transfer_type")]
        public string TransferType { get; set; }

        [JsonProperty("future_date")]
        public string FutureDate { get; set; }

        [JsonProperty("to")]
        public AccountTransferRecipient To { get; set; }
    }

    public class AccountTransferRecipient
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("bank_code")]
        public string BankCode { get; set; }

        [JsonProperty("branch_number")]
        public string BranchNumber { get; set; }

        [JsonProperty("account")]
        public AccountDetails Account { get; set; }
    }

    public class AccountDetails
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("iban")]
        public string Iban { get; set; }
    }

    public class SepaCreditTransfer
    {
        [JsonProperty("debtorAccount")]
        public SepaAccount DebtorAccount { get; set; }

        [JsonProperty("instructedAmount")]
        public MoneyValue InstructedAmount { get; set; }

        [JsonProperty("creditorAccount")]
        public SepaAccount CreditorAccount { get; set; }

        [JsonProperty("creditorName")]
        public string CreditorName { get; set; }
    }

    public class SepaAccount
    {
        [JsonProperty("iban")]
        public string Iban { get; set; }
    }

    public class AgentTransfer
    {
        [JsonProperty("bank_id")]
        public string BankId { get; set; }

        [JsonProperty("agent_number")]
        public string AgentNumber { get; set; }
    }

    public class MoneyValue
    {
        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }
    }

    public class Challenge
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }

        [JsonProperty("allowed_attempts")]
        public int AllowedAttempts { get; set; }

        [JsonProperty("challenge_type")]
        public string ChallengeType { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }
    }

    public class TransactionCharge
    {
        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("value")]
        public MoneyValue Value { get; set; }
    }

    public class TransactionAttribute
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}