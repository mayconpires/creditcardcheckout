module mpdesafio {

    export interface ISaleCtrl {
        creditCard: CreditCardTransaction;
        merchantKey: string;
        postSale(creditCard: CreditCardTransaction): void;
        creditCardSaleResponse: CreditCardSaleResponse;
        verifyRequiredData(): void;
        getCreditCardSaleResponse() : CreditCardSaleResponse;
        loadSaleConfirm(): void;
    }
  
    export class SaleCtrl implements ISaleCtrl {
        
        static $inject: any[] = ['gatewayService', '$location'];
        creditCard: CreditCardTransaction;
        merchantKey: string;
        creditCardSaleResponse: CreditCardSaleResponse;

        constructor(private gatewayService: IGatewayService, private $location: ng.ILocationService) {
            //TODO: Remover
            // this.creditCard = new CreditCardTransaction();
            // this.creditCard.ammount = 130;
            // this.creditCard.buyerEmail = 'email@email.com';
            // this.creditCard.buyerName = 'Name';
            // this.creditCard.creditCardBrand = 'VISA';
            // this.creditCard.creditCardNumber = '4111111111111111';
            // this.creditCard.expMonth = 10;
            // this.creditCard.expYear = 22;
            // this.creditCard.holderName = 'ABCD EF GHI JL';
            // this.creditCard.merchantKey =  'A013EC31-A612-4846-9F9b-38D3AE9372B2';
            // this.creditCard.securityCode = '123';  
        }

        postSale = (creditCard: CreditCardTransaction) => {

            creditCard.merchantKey = this.gatewayService.getMerchantKey();
            this.verifyRequiredData();

            this.gatewayService.doSale(creditCard).then((data) => {
                if(data.status == 200) {
                    this.creditCardSaleResponse = data.data;
                    this.gatewayService.setCreditCardSaleResponse(this.creditCardSaleResponse);
                    this.$location.path('/comprarealizada');
                } else {
                    alert('A Venda não foi aprovada.');
                }

            }).catch( (reason) => {
                alert('Erro no sistema.');
            });
        };

        verifyRequiredData = () : void => {
            let haveUserId = !angular.isUndefined(this.gatewayService.getUserId());
            let haveMerchantKey = !angular.isUndefined(this.gatewayService.getMerchantKey());

            if (!haveUserId) {
                alert('Efetue o Login antes de efetuar a compra.');
                this.$location.path('/accesstoken');
            } else if (!haveMerchantKey) {
                alert('Escolha a Loja antes efetuar a compra.');
                this.$location.path('/merchant');
            } 
        }

         getCreditCardSaleResponse = () : CreditCardSaleResponse => {
            this.creditCardSaleResponse = this.gatewayService.getCreditCardSaleResponse();
            return this.creditCardSaleResponse;
        };

        loadSaleConfirm = (): void => {
            this.getCreditCardSaleResponse();

            if (angular.isUndefined(this.creditCardSaleResponse)) {
                alert('Não há nenhuma confirmação de compra.')
                this.$location.path('/sale');
            }
        };

    }

    angular.module('mpdesafio').controller('SaleCtrl', SaleCtrl)
}