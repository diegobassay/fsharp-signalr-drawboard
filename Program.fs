open Giraffe
open System.IO
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.SignalR
open Microsoft.Extensions.DependencyInjection

let webApp =
      choose [ route "/about"   >=> text "Tutorial F# and SignalR : diegobassay@gmail.com" ]

type InterfaceClientOperations = 
  abstract member ReceiveCoordenates :int * int * bool -> System.Threading.Tasks.Task

type DrawBoardHub () =
  inherit Hub<InterfaceClientOperations> ()
  // Passando mensagem para todos os clientes
  member this.SendCoordenates (x: int, y: int, isMousePressed: bool) = 
    printf "Enviando coordenadas %i %i para o cliente!\n" x y
    this.Clients.All.ReceiveCoordenates(x, y, isMousePressed)    

type Startup() =
    member __.ConfigureServices (services : IServiceCollection) =
        // Registrando serviços com as dependências do SignalR
        services.AddSignalR() |> ignore 
        // Registrando serviços com as dependências do Giraffe
        services.AddGiraffe() |> ignore
        
    member __.Configure (app : IApplicationBuilder) =
        app.UseDefaultFiles() |> ignore
        app.UseStaticFiles() |> ignore
        app.UseSignalR(fun routes -> 
          routes.MapHub<DrawBoardHub>(Microsoft.AspNetCore.Http.PathString("/drawBoardHub"))
        ) |> ignore
        app.UseGiraffe(webApp)

[<EntryPoint>]
let main _ =
    let contentRoot = Directory.GetCurrentDirectory()
    let webRoot     = Path.Combine(contentRoot, "web")
    WebHostBuilder()
        .UseKestrel()
        .UseContentRoot(contentRoot)
        .UseIISIntegration()
        .UseWebRoot(webRoot)
        .UseStartup<Startup>()
        .Build()
        .Run()
    0