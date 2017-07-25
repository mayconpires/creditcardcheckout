using AutoMapper;
using DesafioMPCore.Domain.CheckOut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioMPCore.API.Models
{
    public class CreditCardTransaction
    {
        public string BuyerName { get; set; }

        public string BuyerEmail { get; set; }

        public string CreditCardNumber { get; set; }

        public string HolderName { get; set; }

        public int ExpMonth { get; set; }

        public int ExpYear { get; set; }

        public string CreditCardBrand { get; set; }
        
        public string SecurityCode { get; set; }

        public decimal Ammount { get; set; }

        public string MerchantKey { get; set; }

        public Domain.CheckOut.SaleByCreditCard ToDomain()
        {
            var creditCard = new CreditCard();
            creditCard.CreditCardBrand = CreditCardBrand;
            creditCard.CreditCardNumber = CreditCardNumber;
            creditCard.ExpMonth = ExpMonth;
            creditCard.ExpYear = ExpYear;
            creditCard.HolderName = HolderName;
            creditCard.SecurityCode = SecurityCode;

            var creditCardTransaction = new Domain.CheckOut.CreditCardTransaction();
            creditCardTransaction.AmountInCents = (long)Ammount * 100;
            creditCardTransaction.InstallmentCount = 1;
            creditCardTransaction.CreditCard = creditCard;

            SaleByCreditCard creditCardSale = new SaleByCreditCard();
            creditCardSale.CreditCardTransactionCollection.Add(creditCardTransaction);
            creditCardSale.MerchantKey = MerchantKey;

            return creditCardSale;
        }

        public bool IsValid()
        {
            return (!String.IsNullOrEmpty(BuyerName) && !String.IsNullOrEmpty(BuyerEmail) && !String.IsNullOrEmpty(CreditCardNumber) && !String.IsNullOrEmpty(HolderName)
                    && !(default(int) == (ExpMonth)) && !(default(int) == (ExpYear)) && !String.IsNullOrEmpty(CreditCardBrand) && !String.IsNullOrEmpty(SecurityCode)
                    && !(default(int) == (Ammount)) && !String.IsNullOrEmpty(MerchantKey) );
                    
        }

    }
}
