var mpdesafio;
(function (mpdesafio) {
    var AccessTokenCtrl = (function () {
        function AccessTokenCtrl(gatewayService, $location) {
            var _this = this;
            this.gatewayService = gatewayService;
            this.$location = $location;
            this.getAccessToken = function (user) {
                console.log(user);
                _this.gatewayService.logIn(user).then(function (data) {
                    if (data.status == 200) {
                        _this.token = data.data;
                        _this.gatewayService.setUserId(_this.token.userId);
                        console.log(data);
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
    AccessTokenCtrl.$inject = ['gatewayService', '$location'];
    mpdesafio.AccessTokenCtrl = AccessTokenCtrl;
    angular.module('mpdesafio').controller('AccessTokenCtrl', AccessTokenCtrl);
})(mpdesafio || (mpdesafio = {}));
