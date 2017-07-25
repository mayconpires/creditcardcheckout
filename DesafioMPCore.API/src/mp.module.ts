/// <reference path="./_all.ts" />

 module mpdesafio {
     
     angular.module('mpdesafio', ['ngRoute'])
        .config(($routeProvider: angular.route.IRouteProvider): void => {
            $routeProvider
                .when('/accesstoken',
                {
                    templateUrl: 'view/accesstoken.view.html',
                    controller: 'AccessTokenCtrl',
                    controllerAs: 'ctrl'
                })
                .when('/merchant',
                {
                    templateUrl: 'view/merchant.view.html',
                    controller: 'MerchantCtrl',
                    controllerAs: 'ctrl'
                })
                .when('/sale',
                {
                    templateUrl: 'view/sale.view.html',
                    controller: 'SaleCtrl',
                    controllerAs: 'ctrl'
                })
                .when('/comprarealizada',
                {
                    templateUrl: 'view/compraefetuada.view.html',
                    controller: 'SaleCtrl',
                    controllerAs: 'ctrl'
                })
                .otherwise({ redirectTo: '/accesstoken' })
            }
        );
}