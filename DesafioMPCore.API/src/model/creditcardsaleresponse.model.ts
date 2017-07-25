module mpdesafio {
    export class CreditCardSaleResponse {
        errorReport: string;
        requestKey: string;
        buyerKey: string;
        orderResult: Order;
        creditCardTransactionResult: Array<CreditCardTransactionResult>
    }
}