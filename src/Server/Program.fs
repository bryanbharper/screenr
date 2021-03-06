module Server.Program

open Fable.Remoting.Server
open Fable.Remoting.Giraffe
open Microsoft.Extensions.DependencyInjection
open Saturn

open Shared.ApiContract
open Server.Apis

let configureServices (services : IServiceCollection) =
    services
        .AddSingleton<IMovieDbApi, MovieDbApi>()

let webApp =
    Remoting.createApi()
    |> Remoting.withRouteBuilder Route.builder
    |> Remoting.fromReader InternalApi.reader
    |> Remoting.buildHttpHandler

let app =
    application {
        url "http://0.0.0.0:8085"
        use_router webApp
        service_config configureServices
        memory_cache
        use_static "public"
        use_gzip
    }

run app
