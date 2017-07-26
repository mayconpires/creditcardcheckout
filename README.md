# creditcardcheckout
Project uses Asp.Net Core 1.1, WebApi in Backend with Unit Test, and uses Angular 1.6, Typescript and Bootstrap in Client Side.

For deploy uses Docker and Docker Compose.

IDE Tools: Visual Studio 2017 Community, Visual Code

Steps to Deploy:

1) Access the folder DesafioMPCore.API\wwwroot
2) Open the file mp.module.js, change variable urlBase: 'http://localhost:57881/api/' to 'ip from your docker'
Example: urlBase: 'http://192.168.99.100:57881/api/'
3) Open Command Prompter and Access the folder DesafioMPCore.API
4) Type: dotnet restore 
5) Type: dotnet publish -o ./publish
6) Open Docker
7) Access the folder DesafioMPCore.API
8) Type: docker build -t desafiompcore.api .
9) Type: docker run -p 57881:80 desafiompcore.api
The port 57881 can be changed, but you have configure the same port from the item "2"
10) Discovery the ip from your docker serve
This ip is the same the item "2"
 


Created By: Maycon Pires
LinkedIn: https://www.linkedin.com/in/mayconpires/
