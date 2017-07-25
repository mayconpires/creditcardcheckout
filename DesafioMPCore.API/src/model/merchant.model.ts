module mpdesafio {
    export class Merchant {
        merchantKey: string;
        publicMerchantKey: string;
        merchantName: string;
        corporateName: string;
        isDeleted?: boolean;
        isEnabled?: boolean;
        merchantStatus: string;
        documentNumber: string;
    }
}