using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioMPCore.Domain.CheckOut
{
    public class TransactionCard
    {
        public string BuyerName { get; set; }

        public string BuyerEmail { get; set; }

        public string CreditCardNumber { get; set; }

        public string CreditCardNameOwner { get; set; }

        public DateTime CreditCardDataExpiration { get; set; }

        /// <summary>
        /// Credit Card Flag
        /// </summary>
        public string CreditCardBrand { get; set; }

        public string SecurityCode { get; set; }

        public decimal Ammount { get; set; }

    }
}
