using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioMPCore.Domain.CheckOut
{
    public class CreditCardSaleResponse
    {
        public string ErrorReport { get; set; }

        public int InternalTime { get; set; }

        public string MerchantKey { get; set; }

        public string RequestKey { get; set; }

        public string BuyerKey { get; set; }

        public ICollection<CreditCardTransactionResult> CreditCardTransactionResultCollection { get; set; }

        public Order OrderResult { get; set; }
    }
}
