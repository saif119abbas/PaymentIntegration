using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentIntegration.Models
{
    public class PaymentStatusData
    {
        public int InvoiceId { get; set; }
        public string InvoiceStatus { get; set; }
        public string InvoiceReference { get; set; }
        public string CustomerReference { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ExpiryDate { get; set; }
        public string ExpiryTime { get; set; }
        public decimal InvoiceValue { get; set; }
        public string Comments { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerEmail { get; set; }
        public string UserDefinedField { get; set; }
        public string InvoiceDisplayValue { get; set; }
        public decimal DueDeposit { get; set; }
        public string DepositStatus { get; set; }
        public List<object> InvoiceItems { get; set; } // Replace with a class if items have structure
        public List<InvoiceTransaction> InvoiceTransactions { get; set; }
        public List<object> Suppliers { get; set; }
    }
}
