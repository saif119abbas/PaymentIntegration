using Newtonsoft.Json;

namespace PaymentIntegration.Models
{
    public class CardCreateResponse
    {
        [JsonProperty("card_id")]
        public string CardId { get; set; }

        [JsonProperty("bank_id")]
        public string BankId { get; set; }

        [JsonProperty("card_number")]
        public string CardNumber { get; set; }

        [JsonProperty("card_type")]
        public string CardType { get; set; }

        [JsonProperty("name_on_card")]
        public string NameOnCard { get; set; }

        [JsonProperty("issue_number")]
        public string IssueNumber { get; set; }

        [JsonProperty("serial_number")]
        public string SerialNumber { get; set; }

        [JsonProperty("valid_from_date")]
        public string ValidFromDate { get; set; }

        [JsonProperty("expires_date")]
        public string ExpiresDate { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("cancelled")]
        public bool Cancelled { get; set; }

        [JsonProperty("on_hot_list")]
        public bool OnHotList { get; set; }

        [JsonProperty("technology")]
        public string Technology { get; set; }

        [JsonProperty("networks")]
        public List<string> Networks { get; set; } = new List<string>();

        [JsonProperty("allows")]
        public List<string> Allows { get; set; } = new List<string>();

        [JsonProperty("account")]
        public AccountDetails Account { get; set; }

        [JsonProperty("replacement")]
        public ReplacementInfo Replacement { get; set; }

        [JsonProperty("pin_reset")]
        public List<PinReset> PinResets { get; set; } = new List<PinReset>();

        [JsonProperty("collected")]
        public string Collected { get; set; }

        [JsonProperty("posted")]
        public string Posted { get; set; }

        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("cvv")]
        public string Cvv { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }
    }

    public class AccountDetails
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("views_available")]
        public List<AccountView> ViewsAvailable { get; set; } = new List<AccountView>();

        [JsonProperty("bank_id")]
        public string BankId { get; set; }
    }

    public class AccountView
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("short_name")]
        public string ShortName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("is_public")]
        public bool IsPublic { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("hide_metadata_if_alias_used")]
        public bool HideMetadataIfAliasUsed { get; set; }

        // Permissions
        [JsonProperty("can_add_comment")]
        public bool CanAddComment { get; set; }

        [JsonProperty("can_add_corporate_location")]
        public bool CanAddCorporateLocation { get; set; }

        [JsonProperty("can_add_image")]
        public bool CanAddImage { get; set; }

        [JsonProperty("can_add_image_url")]
        public bool CanAddImageUrl { get; set; }

        [JsonProperty("can_add_more_info")]
        public bool CanAddMoreInfo { get; set; }

        [JsonProperty("can_add_open_corporates_url")]
        public bool CanAddOpenCorporatesUrl { get; set; }

        [JsonProperty("can_add_physical_location")]
        public bool CanAddPhysicalLocation { get; set; }

        [JsonProperty("can_add_private_alias")]
        public bool CanAddPrivateAlias { get; set; }

        [JsonProperty("can_add_public_alias")]
        public bool CanAddPublicAlias { get; set; }

        [JsonProperty("can_add_tag")]
        public bool CanAddTag { get; set; }

        [JsonProperty("can_add_url")]
        public bool CanAddUrl { get; set; }

        [JsonProperty("can_add_where_tag")]
        public bool CanAddWhereTag { get; set; }

        [JsonProperty("can_delete_comment")]
        public bool CanDeleteComment { get; set; }

        [JsonProperty("can_delete_corporate_location")]
        public bool CanDeleteCorporateLocation { get; set; }

        [JsonProperty("can_delete_image")]
        public bool CanDeleteImage { get; set; }

        [JsonProperty("can_delete_physical_location")]
        public bool CanDeletePhysicalLocation { get; set; }

        [JsonProperty("can_delete_tag")]
        public bool CanDeleteTag { get; set; }

        [JsonProperty("can_delete_where_tag")]
        public bool CanDeleteWhereTag { get; set; }

        [JsonProperty("can_edit_owner_comment")]
        public bool CanEditOwnerComment { get; set; }

        // View permissions
        [JsonProperty("can_see_bank_account_balance")]
        public bool CanSeeBankAccountBalance { get; set; }

        [JsonProperty("can_see_bank_account_bank_name")]
        public bool CanSeeBankAccountBankName { get; set; }

        [JsonProperty("can_see_bank_account_currency")]
        public bool CanSeeBankAccountCurrency { get; set; }

        [JsonProperty("can_see_bank_account_iban")]
        public bool CanSeeBankAccountIban { get; set; }

        [JsonProperty("can_see_bank_account_label")]
        public bool CanSeeBankAccountLabel { get; set; }

        [JsonProperty("can_see_bank_account_national_identifier")]
        public bool CanSeeBankAccountNationalIdentifier { get; set; }

        [JsonProperty("can_see_bank_account_number")]
        public bool CanSeeBankAccountNumber { get; set; }

        [JsonProperty("can_see_bank_account_owners")]
        public bool CanSeeBankAccountOwners { get; set; }

        [JsonProperty("can_see_bank_account_swift_bic")]
        public bool CanSeeBankAccountSwiftBic { get; set; }

        [JsonProperty("can_see_bank_account_type")]
        public bool CanSeeBankAccountType { get; set; }

        [JsonProperty("can_see_comments")]
        public bool CanSeeComments { get; set; }

        [JsonProperty("can_see_corporate_location")]
        public bool CanSeeCorporateLocation { get; set; }

        [JsonProperty("can_see_image_url")]
        public bool CanSeeImageUrl { get; set; }

        [JsonProperty("can_see_images")]
        public bool CanSeeImages { get; set; }

        [JsonProperty("can_see_more_info")]
        public bool CanSeeMoreInfo { get; set; }

        [JsonProperty("can_see_open_corporates_url")]
        public bool CanSeeOpenCorporatesUrl { get; set; }

        [JsonProperty("can_see_other_account_bank_name")]
        public bool CanSeeOtherAccountBankName { get; set; }

        [JsonProperty("can_see_other_account_iban")]
        public bool CanSeeOtherAccountIban { get; set; }

        [JsonProperty("can_see_other_account_kind")]
        public bool CanSeeOtherAccountKind { get; set; }

        [JsonProperty("can_see_other_account_metadata")]
        public bool CanSeeOtherAccountMetadata { get; set; }

        [JsonProperty("can_see_other_account_national_identifier")]
        public bool CanSeeOtherAccountNationalIdentifier { get; set; }

        [JsonProperty("can_see_other_account_number")]
        public bool CanSeeOtherAccountNumber { get; set; }

        [JsonProperty("can_see_other_account_swift_bic")]
        public bool CanSeeOtherAccountSwiftBic { get; set; }

        [JsonProperty("can_see_owner_comment")]
        public bool CanSeeOwnerComment { get; set; }

        [JsonProperty("can_see_physical_location")]
        public bool CanSeePhysicalLocation { get; set; }

        [JsonProperty("can_see_private_alias")]
        public bool CanSeePrivateAlias { get; set; }

        [JsonProperty("can_see_public_alias")]
        public bool CanSeePublicAlias { get; set; }

        [JsonProperty("can_see_tags")]
        public bool CanSeeTags { get; set; }

        [JsonProperty("can_see_transaction_amount")]
        public bool CanSeeTransactionAmount { get; set; }

        [JsonProperty("can_see_transaction_balance")]
        public bool CanSeeTransactionBalance { get; set; }

        [JsonProperty("can_see_transaction_currency")]
        public bool CanSeeTransactionCurrency { get; set; }

        [JsonProperty("can_see_transaction_description")]
        public bool CanSeeTransactionDescription { get; set; }

        [JsonProperty("can_see_transaction_finish_date")]
        public bool CanSeeTransactionFinishDate { get; set; }

        [JsonProperty("can_see_transaction_metadata")]
        public bool CanSeeTransactionMetadata { get; set; }

        [JsonProperty("can_see_transaction_other_bank_account")]
        public bool CanSeeTransactionOtherBankAccount { get; set; }

        [JsonProperty("can_see_transaction_start_date")]
        public bool CanSeeTransactionStartDate { get; set; }

        [JsonProperty("can_see_transaction_this_bank_account")]
        public bool CanSeeTransactionThisBankAccount { get; set; }

        [JsonProperty("can_see_transaction_type")]
        public bool CanSeeTransactionType { get; set; }

        [JsonProperty("can_see_url")]
        public bool CanSeeUrl { get; set; }

        [JsonProperty("can_see_where_tag")]
        public bool CanSeeWhereTag { get; set; }
    }

    public class ReplacementInfo
    {
        [JsonProperty("requested_date")]
        public string RequestedDate { get; set; }

        [JsonProperty("reason_requested")]
        public string ReasonRequested { get; set; }
    }

    public class PinReset
    {
        [JsonProperty("requested_date")]
        public string RequestedDate { get; set; }

        [JsonProperty("reason_requested")]
        public string ReasonRequested { get; set; }
    }
}