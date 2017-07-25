using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioMPCore.Domain.CheckOut
{
    public class SaleByCreditCard
    {
        public ICollection<CreditCardTransaction> CreditCardTransactionCollection { get; set; } = new List<CreditCardTransaction>();

        public Order Order { get; set; }

        public string MerchantKey { get; set; }
    }
}
