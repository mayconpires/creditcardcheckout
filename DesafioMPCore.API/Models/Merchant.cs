using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioMPCore.API.Models
{
    public class Merchant
    {
        public string MerchantKey { get; set; }

        public string PublicMerchantKey { get; set; }

        public string MerchantName { get; set; }

        public string DocumentNumber { get; set; }

        public string CorporateName { get; set; }

        public bool? IsRetryEnabled { get; set; }

        public bool? IsAntiFraudEnabled { get; set; }

        public bool? IsDeleted { get; set; }

        public bool? IsEnabled { get; set; }

        public string MerchantStatus { get; set; }

    }
}
