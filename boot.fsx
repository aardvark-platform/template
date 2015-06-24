open System
open System.IO
open System.Diagnostics
open System.Text.RegularExpressions

do Environment.CurrentDirectory <- __SOURCE_DIRECTORY__


let ask (question : string) =
    printfn "%s" question
    Console.ReadLine()

let rec askYesNo (question : string) =
    printfn "%s (Y|n)" question
    let a = Console.ReadLine()
    if a = "" || a.[0] = 'Y' || a.[0] = 'y' then true 
    elif a.[0] = 'N' || a.[0] = 'n' then false
    else askYesNo question

let projectGuid = Guid.NewGuid() |> string
let solutionName = ask "Please enter a solution name"
let projectName = ask "Please enter a project name"
let wpf = askYesNo "Would you like to reference the WPF libraries?"
let winForms = if wpf then false else askYesNo "Would you like to reference the Windows.Forms libraries?"

printfn "creating template"

do Environment.CurrentDirectory <- __SOURCE_DIRECTORY__

let preprocess(file : string) =
    let ifRx = Regex @"[ \t]*#if[ \t](?<name>[a-zA-Z_]+)[ \t]*[\r\n]*(?<body>[^#]*?)[\r\n \t]*#endif"
    let f = File.ReadAllText file

    let cleaned = ifRx.Replace(f, MatchEvaluator(fun m ->
        match m.Groups.["name"].Value with
            | "__WPF__" when wpf -> m.Groups.["body"].Value
            | "__WinForms__" when winForms -> m.Groups.["body"].Value
            | _ -> ""
    ))

    let cleaned =
        cleaned
            .Replace("__PROJECT_NAME__", projectName)
            .Replace("__PROJECT_GUID__", projectGuid) 

    File.WriteAllText(file, cleaned)

let bootSolution() =
    File.Move(Path.Combine("src", "__SOLUTION_NAME__.sln"), Path.Combine("src", sprintf "%s.sln" solutionName))
    File.Move(Path.Combine("src", "__PROJECT_NAME__", "__PROJECT_NAME__.fsproj"), Path.Combine("src", "__PROJECT_NAME__", sprintf "%s.fsproj" projectName))
    Directory.Move(Path.Combine("src", "__PROJECT_NAME__"), Path.Combine("src", projectName))

preprocess <| "paket.dependencies"
preprocess <| "build.fsx"
preprocess <| Path.Combine("src", "__PROJECT_NAME__", "paket.references")
preprocess <| Path.Combine("src", "__PROJECT_NAME__", "__PROJECT_NAME__.fsproj")
preprocess <| Path.Combine("src", "__SOLUTION_NAME__.sln")
bootSolution()