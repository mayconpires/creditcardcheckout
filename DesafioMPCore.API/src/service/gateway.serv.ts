
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

        static $inject = ['$http'];
        config: angular.IRequestShortcutConfig;
        userId: string;
        urlApi: string;
        merchantKey: string;
        creditCardSaleResponse: CreditCardSaleResponse;

        constructor(private $http : ng.IHttpService) {
            this.config =  {
                headers: {
                    "Content-Type":"application/json"
                }
            };
            this.urlApi = "http://localhost:57881/api/";
        }

        logIn = (user: User): ng.IPromise<any> => {
            var config: angular.IRequestShortcutConfig = {
                headers: {
                    "Content-Type":"application/json"
                }
            }

            return this.$http.post('http://localhost:57881/api/user/accesstoken', user, config );
        };

        seekMerchants = (): ng.IPromise<any> => {
            var urlMerchant = "http://localhost:57881/api/merchants/" + this.userId;
            
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