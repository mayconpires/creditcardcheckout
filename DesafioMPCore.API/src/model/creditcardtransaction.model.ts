module mpdesafio {
    export class CreditCardTransaction {
        buyerName: string;
        buyerEmail: string;
        creditCardNumber: string;
        holderName: string;
        expMonth: number;
        expYear?: number;
        creditCardBrand: string;
        documentNumber: string;
        securityCode: string;
        ammount: number;
        merchantKey: string;
    }
}