var mpdesafio;
(function (mpdesafio) {
    var MerchantCtrl = (function () {
        function MerchantCtrl(gatewayService, $location) {
            var _this = this;
            this.gatewayService = gatewayService;
            this.$location = $location;
            this.getMerchant = function () {
                var naoPossuiUserId = angular.isUndefined(_this.gatewayService.getUserId());
                if (naoPossuiUserId) {
                    alert('É necessário logar antes de ir buscar a loja');
                    _this.$location.path('/accesstoken');
                }
                _this.gatewayService.seekMerchants().then(function (data) {
                    if (data.status == 200) {
                        _this.merchants = data.data;
                    }
                    else {
                        alert('Nenhuma loja foi encontrada.');
                    }
                }).catch(function (reason) {
                    alert('Erro no sistema.');
                });
            };
            this.openSale = function (merchantKey) {
                _this.gatewayService.setMerchantKey(merchantKey);
                _this.$location.path('/sale');
            };
        }
        return MerchantCtrl;
    }());
    MerchantCtrl.$inject = ['gatewayService', '$location'];
    mpdesafio.MerchantCtrl = MerchantCtrl;
    angular.module('mpdesafio').controller('MerchantCtrl', MerchantCtrl);
})(mpdesafio || (mpdesafio = {}));
