# creditcardcheckout
Project uses Asp.Net Core 1.1, WebApi in Backend with Unit Test, and uses Angular 1.6, Typescript and Bootstrap in Client Side.

For deploy uses Docker and Docker Compose.

IDE Tools: Visual Studio 2017 Community, Visual Code

Steps to Deploy:

1) Access the folder DesafioMPCore.API\wwwroot
2) Open the file mp.module.js, change variable urlBase: 'http://localhost:57881/api/' to 'ip from your docker'
Example: urlBase: 'http://192.168.99.100:5781/api/'
3) Open Command Prompter and Access the folder DesafioMPCore.API
4) Type: dotnet restore 
5) Type: dotnet publish -o ./publish
6) Open Docker
7) Access the folder DesafioMPCore.API
8) Type: docker compose up
9) Access http://IpFromYourDockerServer:5781/ to see the FrontEnd
This ip is the same the item "2"
10) To call WebApi direct there are the urls:

[POST]api/user/accesstoken
Body Content:
{
  "username": "teste",
  "password": "123456"
}

[GET]api/merchants/{userId}

[POST]api/CreditCardTransaction
Body Content:
{
	"BuyerName": "Name Buyer",
	"BuyerEmail": "email@email.com",
	"CreditCardNumber": "4111111111111111",
	"HolderName": "ABCD EF GHI JL",
	"ExpMonth": 10,
	"ExpYear": 22,
	"CreditCardBrand": "VISA",
	"SecurityCode": "123",
	"Ammount": 130,
	"MerchantKey": "A013EC31-A612-4846-9F9b-38D3AE9372B2"
}
 


Created By: Maycon Pires
LinkedIn: https://www.linkedin.com/in/mayconpires/
