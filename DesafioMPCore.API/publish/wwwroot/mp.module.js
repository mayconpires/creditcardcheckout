/// <reference path="./_all.ts" />
var mpdesafio;
(function (mpdesafio) {
    angular.module('mpdesafio', ['ngRoute'])
        .config(function ($routeProvider) {
        $routeProvider
            .when('/accesstoken', {
            templateUrl: 'view/accesstoken.view.html',
            controller: 'AccessTokenCtrl',
            controllerAs: 'ctrl'
        })
            .when('/merchant', {
            templateUrl: 'view/merchant.view.html',
            controller: 'MerchantCtrl',
            controllerAs: 'ctrl'
        })
            .when('/sale', {
            templateUrl: 'view/sale.view.html',
            controller: 'SaleCtrl',
            controllerAs: 'ctrl'
        })
            .when('/comprarealizada', {
            templateUrl: 'view/compraefetuada.view.html',
            controller: 'SaleCtrl',
            controllerAs: 'ctrl'
        })
            .otherwise({ redirectTo: '/accesstoken' });
    })
        .constant('appConfig', {
        urlBase: 'http://192.168.99.100:5781/api/'
    });
})(mpdesafio || (mpdesafio = {}));
