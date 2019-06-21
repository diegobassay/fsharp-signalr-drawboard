# fsharp-signalr-drawboard
Script que demonstra a fácil criação de uma prancheta remota para desenhar em tempo real com outros usuários usando .Net Core 3.0, F#, Giraffe e SignalR.

## Pré-requisitos

* .Net Core = v3.0.0-preview6

## Preparando o projeto
Para construir o projeto executar o seguinte comando na raiz do projeto:
```
dotnet build
```
## Para executar o projeto
Depois de executar o passo acima executar o seguinte comando na raiz do projeto:
```
dotnet run
```
## Para testar
Usar o seguinte endereço:
```
http://localhost:5000/about
```
Deve aparecer na tela o seguinte texto:
Tutorial F# and SignalR : diegobassay@gmail.com

## Para executar
Acessar a url abaixo em dois browsers diferentes (Firefox/Chrome/IE/Opera) ao mesmo tempo:
```
http://localhost:5000/
```
## Demonstração

![desenhando](https://github.com/diegobassay/fsharp-signalr-drawboard/blob/master/web/demo.gif)