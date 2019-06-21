open Giraffe
open System.IO
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection

let webApp =
    choose [ route "/about"   >=> text "Tutorial F# and SignalR : diegobassay@gmail.com" ]

type Startup() =
    member __.ConfigureServices (services : IServiceCollection) =
        // Registrando serviços com as depedências do Giraffe
        services.AddGiraffe() |> ignore
        
    member __.Configure (app : IApplicationBuilder) =
        app.UseDefaultFiles() |> ignore
        app.UseStaticFiles() |> ignore
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
