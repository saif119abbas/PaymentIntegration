using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentIntegration.Models
{
    public class Balance
    {
        public string currency { get; set; }
        public string amount { get; set; }
    }

    public class AccountRouting
    {
        public string scheme { get; set; }
        public string address { get; set; }
    }

    public class CreateAccountResponse
    {
        public string account_id { get; set; }
        public string user_id { get; set; }
        public string label { get; set; }
        public string product_code { get; set; }
        public Balance balance { get; set; }
        public string branch_id { get; set; }
        public List<AccountRouting> account_routings { get; set; }
        public List<object> account_attributes { get; set; }
    }
}
