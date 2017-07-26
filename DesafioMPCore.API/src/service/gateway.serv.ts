
module mpdesafio {
    'use strict';

    export interface IGatewayService {
        logIn(user: User): ng.IPromise<any>;
        seekMerchants(): ng.IPromise<any>;
        doSale(creditCard: CreditCardTransaction): ng.IPromise<any>;
        urlApi: string;
        
        userId: string;
        setUserId(userId: string) : void;
        getUserId() : string;
        config: angular.IRequestShortcutConfig;
        
        merchantKey: string;
        setMerchantKey(merchantKey: string) : void;
        getMerchantKey() : string;

        creditCardSaleResponse: CreditCardSaleResponse;
        setCreditCardSaleResponse(creditCardSaleResponse: CreditCardSaleResponse) : void;
        getCreditCardSaleResponse() : CreditCardSaleResponse;
    }

    export class GatewayService implements IGatewayService {

        static $inject = ['$http', 'appConfig'];
        config: angular.IRequestShortcutConfig;
        userId: string;
        urlApi: string;
        merchantKey: string;
        creditCardSaleResponse: CreditCardSaleResponse;

        constructor(private $http : ng.IHttpService, private appConfig: any) {
            this.config =  {
                headers: {
                    "Content-Type":"application/json"
                }
            };
            this.urlApi =  appConfig.urlBase;
        }

        logIn = (user: User): ng.IPromise<any> => {
            var urlLogin = this.urlApi + "user/accesstoken";
            
            return this.$http.post(urlLogin, user, this.config );
        };

        seekMerchants = (): ng.IPromise<any> => {
            var urlMerchant = this.urlApi + "merchants/" + this.userId;
            
            return this.$http.get(urlMerchant, this.config );
        };

        doSale = (creditCard: CreditCardTransaction): ng.IPromise<any> => {
            let urlCreditCardTransaction = this.urlApi + "CreditCardTransaction" ;
            
            return this.$http.post(urlCreditCardTransaction, creditCard, this.config);
        };

        setUserId = (userId: string) : void => {
            this.userId = userId;
        };

        getUserId = () : string => {
            return this.userId;
        };

        setMerchantKey = (merchantKey: string) : void => {
            this.merchantKey = merchantKey;
        };

        getMerchantKey = () : string => {
            return this.merchantKey;
        };

        setCreditCardSaleResponse = (creditCardSaleResponse: CreditCardSaleResponse) : void => {
            this.creditCardSaleResponse = creditCardSaleResponse;
        };

        getCreditCardSaleResponse = () : CreditCardSaleResponse => {
            return this.creditCardSaleResponse;
        };

    }

    angular.module('mpdesafio').service('gatewayService', mpdesafio.GatewayService);
}