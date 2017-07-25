using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioMPCore.Domain.CheckOut
{
    public class CreditCardTransaction
    {
        public long AmountInCents { get; set; }

        public CreditCard CreditCard { get; set; }

        public int InstallmentCount { get; set; }
    }
}
