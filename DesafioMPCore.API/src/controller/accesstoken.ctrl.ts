module mpdesafio {

    export interface IAccessTokenCtrl {
        user: User;
        token: Token;
        
        getAccessToken(user: User): void;
    }
  
    export class AccessTokenCtrl implements IAccessTokenCtrl {
        static $inject: any[] = ['gatewayService', '$location', 'appConfig'];
        
        constructor(private gatewayService: IGatewayService, private $location: ng.ILocationService, private appConfig: any) {
        }

        user: User;
        token: Token;
        
        getAccessToken = (user: User) => {
            this.gatewayService.logIn(user).then((data) => {
                debugger;
                if(data.status == 200) {
                    this.token = data.data;
                    this.gatewayService.setUserId(this.token.userId);
                    this.$location.path('/merchant');
                } else {
                    alert('Credenciais inválidas.');
                }

            }).catch( (reason) => {
                alert('Erro no sistema.');
            });
        };

    }

    angular.module('mpdesafio').controller('AccessTokenCtrl', AccessTokenCtrl)
}