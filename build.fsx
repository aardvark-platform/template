#load @"paket-files/build/aardvark-platform/aardvark.fake/DefaultSetup.fsx"

open Fake
open System
open System.IO
open System.Diagnostics
open Aardvark.Fake

do Environment.CurrentDirectory <- __SOURCE_DIRECTORY__

DefaultSetup.install ["src/__SOLUTION_NAME__.sln"]

Target "Start" (fun() ->
    let param (p : DotNetCli.CommandParams) =
        { p with WorkingDir = Path.Combine("bin", "Release", "netcoreapp2.0") }

    DotNetCli.RunCommand param "__PROJECT_NAME__.dll"
)

Target "Run" (fun () -> Run "Start")
"Compile" ==> "Run"

entry()