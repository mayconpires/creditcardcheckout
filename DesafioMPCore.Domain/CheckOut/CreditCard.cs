using System;

namespace DesafioMPCore.Domain.CheckOut
{
    public class CreditCard
    {
        public string CreditCardNumber { get; set; }

        public string HolderName { get; set; }

        public int ExpMonth { get; set; }

        public int ExpYear { get; set; }

        public string CreditCardBrand { get; set; }

        public string SecurityCode { get; set; }

        //public string InstantBuyKey { get; set; }

        public bool IsExpiredCreditCard { get; set; }

        public string MaskedCreditCardNumber { get; set; }

    }
}