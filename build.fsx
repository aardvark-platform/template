#r "paket: groupref Build //"
#load ".fake/build.fsx/intellisense.fsx"
#load @"paket-files/build/aardvark-platform/aardvark.fake/DefaultSetup.fsx"

open System
open System.IO
open System.Diagnostics
open Aardvark.Fake
open Fake.Core
open Fake.Core.TargetOperators
open Fake.DotNet

do Environment.CurrentDirectory <- __SOURCE_DIRECTORY__

DefaultSetup.install ["src/__SOLUTION_NAME__.sln"]

Target.create "Start" (fun _ ->
    let param (p : DotNet.Options) =
        { p with WorkingDirectory = Path.Combine("bin", "Release", "netcoreapp2.0") }

    DotNet.exec param "" "__PROJECT_NAME__.dll" |> ignore
)

Target.create "Run" (fun _ -> Target.run 1 "Start" [])
"Compile" ==> "Run"

entry()