using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioMPCore.Domain.CheckOut
{
    public class CreditCardTransactionResult
    {
        public string AcquirerMessage { get; set; }

        public string AcquirerName { get; set; }

        public string AcquirerReturnCode { get; set; }

        public string AffiliationCode { get; set; }

        public long AmountInCents { get; set; }

        public string AuthorizationCode { get; set; }

        public long AuthorizedAmountInCents { get; set; }

        public long CapturedAmountInCents { get; set; }

        public DateTime CapturedDate { get; set; }

        public CreditCard CreditCard { get; set; }

        public string CreditCardOperation { get; set; }

        public string CreditCardTransactionStatus { get; set; }

        public DateTime? DueDate { get; set; }

        public string EstablishmentCode { get; set; }

        public int ExternalTime { get; set; }

        public string PaymentMethodName { get; set; }

        public long? RefundedAmountInCents { get; set; }

        public bool Success { get; set; }

        public string ThirdPartyAffiliationCode { get; set; }

        public string TransactionIdentifier { get; set; }

        public string TransactionKey { get; set; }

        public string TransactionKeyToAcquirer { get; set; }

        public string TransactionReference { get; set; }

        public string UniqueSequentialNumber { get; set; }

        public long? VoidedAmountInCents { get; set; }

        public int InstallmentCount { get; set; }
    }
}
