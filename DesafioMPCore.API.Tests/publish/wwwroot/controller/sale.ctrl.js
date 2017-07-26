var mpdesafio;
(function (mpdesafio) {
    var SaleCtrl = (function () {
        function SaleCtrl(gatewayService, $location) {
            var _this = this;
            this.gatewayService = gatewayService;
            this.$location = $location;
            this.postSale = function (creditCard) {
                creditCard.merchantKey = _this.gatewayService.getMerchantKey();
                _this.verifyRequiredData();
                _this.gatewayService.doSale(creditCard).then(function (data) {
                    if (data.status == 200) {
                        _this.creditCardSaleResponse = data.data;
                        _this.gatewayService.setCreditCardSaleResponse(_this.creditCardSaleResponse);
                        _this.$location.path('/comprarealizada');
                    }
                    else {
                        alert('A Venda não foi aprovada.');
                    }
                }).catch(function (reason) {
                    alert('Erro no sistema.');
                });
            };
            this.verifyRequiredData = function () {
                var haveUserId = !angular.isUndefined(_this.gatewayService.getUserId());
                var haveMerchantKey = !angular.isUndefined(_this.gatewayService.getMerchantKey());
                if (!haveUserId) {
                    alert('Efetue o Login antes de efetuar a compra.');
                    _this.$location.path('/accesstoken');
                }
                else if (!haveMerchantKey) {
                    alert('Escolha a Loja antes efetuar a compra.');
                    _this.$location.path('/merchant');
                }
            };
            this.getCreditCardSaleResponse = function () {
                _this.creditCardSaleResponse = _this.gatewayService.getCreditCardSaleResponse();
                return _this.creditCardSaleResponse;
            };
            this.loadSaleConfirm = function () {
                _this.getCreditCardSaleResponse();
                if (angular.isUndefined(_this.creditCardSaleResponse)) {
                    alert('Não há nenhuma confirmação de compra.');
                    _this.$location.path('/sale');
                }
            };
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
        return SaleCtrl;
    }());
    SaleCtrl.$inject = ['gatewayService', '$location'];
    mpdesafio.SaleCtrl = SaleCtrl;
    angular.module('mpdesafio').controller('SaleCtrl', SaleCtrl);
})(mpdesafio || (mpdesafio = {}));
