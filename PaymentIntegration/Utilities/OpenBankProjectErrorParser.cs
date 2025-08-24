namespace PaymentIntegration.Utilities
{
    public static class OpenBankProjectErrorParser
    {
        private static readonly Dictionary<string, string> ErrorMappings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            // Authentication errors
            ["OBP-20001"] = "Please log in to continue with your payment.",

            // Invalid input errors
            ["OBP-30111"] = "The bank ID you entered is invalid. Please check and try again.",
            ["OBP-30110"] = "The account ID you entered is invalid. Please check and try again.",
            ["OBP-10001"] = "Invalid data format. Please check your information and try again.",
            ["OBP-40001"] = "Invalid transaction type selected.",
            ["OBP-10002"] = "Invalid amount entered. Please enter a valid number.",
            ["OBP-10035"] = "Invalid card.",

            // Not found errors
            ["OBP-30001"] = "Bank not found. Please check the bank details.",
            ["OBP-30003"] = "Account not found. Please check the account details.",
            ["OBP-30018"] = "Bank account not found. Please verify both bank and account details.",

            // Authorization errors
            ["OBP-40002"] = "You don't have permission to make this transaction. Please contact your bank.",

            // Business rule errors
            ["OBP-40008"] = "Payment amount must be greater than zero.",
            ["OBP-40003"] = "Currency mismatch. Please ensure the payment currency matches your account currency.",
            ["OBP-00003"] = "Transaction services are currently unavailable. Please try again later.",

            // Generic errors
            ["OBP-50000"] = "An unexpected error occurred. Please try again or contact support."
        };

        public static string GetUserFriendlyErrorMessage(string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
                return "An unknown error occurred.";

            // Try to extract the OBP error code
            var colonIndex = errorMessage.IndexOf(':');
            if (colonIndex > 0 && colonIndex < errorMessage.Length - 1)
            {
                var potentialErrorCode = errorMessage.Substring(0, colonIndex).Trim();

                // Check if we have a mapping for this error code
                if (ErrorMappings.TryGetValue(potentialErrorCode, out var friendlyMessage))
                {
                    return friendlyMessage;
                }
            }

            // If no specific mapping found, try to extract the useful part after the colon
            if (colonIndex > 0 && colonIndex < errorMessage.Length - 1)
            {
                var usefulMessage = errorMessage.Substring(colonIndex + 1).Trim();
                return CapitalizeFirstLetter(usefulMessage);
            }

            // Fallback: return the original message without OBP codes if they exist
            return CleanErrorMessage(errorMessage);
        }

        private static string CleanErrorMessage(string errorMessage)
        {
            // Remove OBP-XXXXX: patterns
            var obpPatternIndex = errorMessage.IndexOf("OBP-");
            if (obpPatternIndex >= 0)
            {
                var colonIndex = errorMessage.IndexOf(':', obpPatternIndex);
                if (colonIndex > 0 && colonIndex < errorMessage.Length - 1)
                {
                    return CapitalizeFirstLetter(errorMessage.Substring(colonIndex + 1).Trim());
                }
            }

            return CapitalizeFirstLetter(errorMessage);
        }

        private static string CapitalizeFirstLetter(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            if (text.Length == 1)
                return text.ToUpper();

            return char.ToUpper(text[0]) + text.Substring(1);
        }
    }
}
