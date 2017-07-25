module mpdesafio {

    export interface IAccessTokenCtrl {
        user: User;
        token: Token;
        
        getAccessToken(user: User): void;
    }
  
    export class AccessTokenCtrl implements IAccessTokenCtrl {
        static $inject: any[] = ['gatewayService', '$location'];
        
        constructor(private gatewayService: IGatewayService, private $location: ng.ILocationService) {
        }

        user: User;
        token: Token;
        
        getAccessToken = (user: User) => {
            console.log(user);
            this.gatewayService.logIn(user).then((data) => {
                if(data.status == 200) {
                    this.token = data.data;
                    this.gatewayService.setUserId(this.token.userId);
                    console.log(data);
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