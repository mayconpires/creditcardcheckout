var mpdesafio;
(function (mpdesafio) {
    'use strict';
    var GatewayService = (function () {
        function GatewayService($http, appConfig) {
            var _this = this;
            this.$http = $http;
            this.appConfig = appConfig;
            this.logIn = function (user) {
                var urlLogin = _this.urlApi + "user/accesstoken";
                return _this.$http.post(urlLogin, user, _this.config);
            };
            this.seekMerchants = function () {
                var urlMerchant = _this.urlApi + "merchants/" + _this.userId;
                return _this.$http.get(urlMerchant, _this.config);
            };
            this.doSale = function (creditCard) {
                var urlCreditCardTransaction = _this.urlApi + "CreditCardTransaction";
                return _this.$http.post(urlCreditCardTransaction, creditCard, _this.config);
            };
            this.setUserId = function (userId) {
                _this.userId = userId;
            };
            this.getUserId = function () {
                return _this.userId;
            };
            this.setMerchantKey = function (merchantKey) {
                _this.merchantKey = merchantKey;
            };
            this.getMerchantKey = function () {
                return _this.merchantKey;
            };
            this.setCreditCardSaleResponse = function (creditCardSaleResponse) {
                _this.creditCardSaleResponse = creditCardSaleResponse;
            };
            this.getCreditCardSaleResponse = function () {
                return _this.creditCardSaleResponse;
            };
            this.config = {
                headers: {
                    "Content-Type": "application/json"
                }
            };
            this.urlApi = appConfig.urlBase;
        }
        return GatewayService;
    }());
    GatewayService.$inject = ['$http', 'appConfig'];
    mpdesafio.GatewayService = GatewayService;
    angular.module('mpdesafio').service('gatewayService', mpdesafio.GatewayService);
})(mpdesafio || (mpdesafio = {}));
