var mpdesafio;
(function (mpdesafio) {
    var AccessTokenCtrl = (function () {
        function AccessTokenCtrl(gatewayService, $location, appConfig) {
            var _this = this;
            this.gatewayService = gatewayService;
            this.$location = $location;
            this.appConfig = appConfig;
            this.getAccessToken = function (user) {
                _this.gatewayService.logIn(user).then(function (data) {
                    debugger;
                    if (data.status == 200) {
                        _this.token = data.data;
                        _this.gatewayService.setUserId(_this.token.userId);
                        _this.$location.path('/merchant');
                    }
                    else {
                        alert('Credenciais inválidas.');
                    }
                }).catch(function (reason) {
                    alert('Erro no sistema.');
                });
            };
        }
        return AccessTokenCtrl;
    }());
    AccessTokenCtrl.$inject = ['gatewayService', '$location', 'appConfig'];
    mpdesafio.AccessTokenCtrl = AccessTokenCtrl;
    angular.module('mpdesafio').controller('AccessTokenCtrl', AccessTokenCtrl);
})(mpdesafio || (mpdesafio = {}));
