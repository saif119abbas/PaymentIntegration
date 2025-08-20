using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentIntegration.Models
{
    using System;
    using System.Collections.Generic;

    public class GetCurrentUserResponse
    {
        public string user_id { get; set; }
        public string email { get; set; }
        public string provider_id { get; set; }
        public string provider { get; set; }
        public string username { get; set; }
        public Entitlements entitlements { get; set; }
        public Views views { get; set; }
    }

    public class Views
    {
        public List<ViewItem> list { get; set; }
    }

    public class ViewItem
    {
        public string bank_id { get; set; }
        public string account_id { get; set; }
        public string view_id { get; set; }
    }

}
