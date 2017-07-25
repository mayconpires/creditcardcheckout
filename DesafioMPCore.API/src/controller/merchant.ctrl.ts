module mpdesafio {

    export interface IMerchantCtrl {
        token: Token;
        merchantKey: string;
        merchants: Array<Merchant>;
        getMerchant(): void;
        openSale(merchantKey: string): void;
        
    }
  
    export class MerchantCtrl implements IMerchantCtrl {
        static $inject: any[] = ['gatewayService', '$location'];
        
        constructor(private gatewayService: IGatewayService, private $location: ng.ILocationService) {
        }

        token: Token;
        merchantKey: string;
        merchants: Array<Merchant>;
        
        getMerchant = () => {
            
            let naoPossuiUserId = angular.isUndefined(this.gatewayService.getUserId());
            if (naoPossuiUserId) {
                alert('É necessário logar antes de ir buscar a loja');
                this.$location.path('/accesstoken');
            }
            
            this.gatewayService.seekMerchants().then((data) => {
                if(data.status == 200) {
                    this.merchants = data.data;
                } else {
                    alert('Nenhuma loja foi encontrada.');
                }

            }).catch( (reason) => {
                alert('Erro no sistema.');
            });
        };

        openSale = (merchantKey: string): void => {
            this.gatewayService.setMerchantKey(merchantKey);
            this.$location.path('/sale');
        };
    }

    angular.module('mpdesafio').controller('MerchantCtrl', MerchantCtrl)
}