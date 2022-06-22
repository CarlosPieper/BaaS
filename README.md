para tudo funcionar:
1 - instalar o mosquitto no docker seguindo as instruções em
https://github.com/padoinedson/tips/blob/main/mptt_mosquitto.md

2 - tendo instalado o sdk do .net6, basta abrir um terminal na pasta leitor e 
outro na pasta escritor, 
e dentro de cada uma digitar 
dotnet run

3 - no terminal do escritor, digite a mensagem a ser enviada para o BaaS

4 - conferir o funcionamento em 
https://console.firebase.google.com/u/0/project/interoperabilidade-json-9eaa9/database/interoperabilidade-json-9eaa9-default-rtdb/data