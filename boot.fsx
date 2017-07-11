open System
open System.IO
open System.Diagnostics
open System.Text.RegularExpressions

do Environment.CurrentDirectory <- __SOURCE_DIRECTORY__


let ask (question : string) (defaultAnswer : string) =
    printfn "%s" question
    let s = Console.ReadLine()
    match s with
        | "" -> defaultAnswer
        | _ -> s

let rec askYesNo (question : string) =
    printfn "%s (Y|n)" question
    let a = Console.ReadLine()
    if a = "" || a.[0] = 'Y' || a.[0] = 'y' then true 
    elif a.[0] = 'N' || a.[0] = 'n' then false
    else askYesNo question

printfn ""
let projectGuid = Guid.NewGuid() |> string
let solutionName = ask "Please enter a solution name [Aardvark]" "Aardvark"
let projectName = ask "Please enter a project name [Example]" "Example"

let media = askYesNo "Would you want to reference the aardvark-media libraries?"
let winForms = not media && askYesNo "Would you like to reference the Windows.Forms libraries? (If you want to spawn windows to render to them via aardvark.rendering you need those libraries)" 
let wpf = if winForms || media then false else askYesNo "Would you like to reference the WPF libraries?"



let preprocess(file : string) =
    let ifRx = Regex @"[ \t]*#if[ \t](?<name>[a-zA-Z_]+)[ \t]*[\r\n]*(?<body>[^#]*?)[\r\n \t]*#endif"
    let f = File.ReadAllText file

    let cleaned = ifRx.Replace(f, MatchEvaluator(fun m ->
        match m.Groups.["name"].Value with
            | "__WPF__" when wpf -> m.Groups.["body"].Value
            | "__WinForms__" when winForms -> m.Groups.["body"].Value
            | "__media__" when media -> m.Groups.["body"].Value
            | _ -> ""
    ))

    let cleaned =
        cleaned
            .Replace("__SOLUTION_NAME__", solutionName)
            .Replace("__PROJECT_NAME__", projectName)
            .Replace("__PROJECT_GUID__", projectGuid) 

    File.WriteAllText(file, cleaned)

let bootSolution() =
    File.Move(Path.Combine("src", "__SOLUTION_NAME__.sln"), Path.Combine("src", sprintf "%s.sln" solutionName))
    File.Move(Path.Combine("src", "__PROJECT_NAME__", "__PROJECT_NAME__.fsproj"), Path.Combine("src", "__PROJECT_NAME__", sprintf "%s.fsproj" projectName))

    let target = Path.Combine("src", projectName)
    if Directory.Exists target then
        Directory.Delete(target,true)

    let deleteInPr files =
        try
            for f in files do
                let f = Path.Combine("src", "__PROJECT_NAME__",f)
                File.Delete f
        with e -> printfn "could not clean up dir: %A" e

    if media then
        File.Delete(Path.Combine("src", "__PROJECT_NAME__","Program.fs"))
        File.Move(Path.Combine("src", "__PROJECT_NAME__","MediaUI.fs"), Path.Combine("src", "__PROJECT_NAME__","Program.fs"))
        deleteInPr ["WPF.fs";"WinForms.fs"]
    elif wpf then
        File.Delete(Path.Combine("src", "__PROJECT_NAME__","Program.fs"))
        File.Move(Path.Combine("src", "__PROJECT_NAME__","WPF.fs"), Path.Combine("src", "__PROJECT_NAME__","Program.fs"))
        deleteInPr ["MediaUI.fs";"RenderModel.fs";"RenderModelApp.fs"; "WinForms.fs"]
    elif winForms then
        File.Delete(Path.Combine("src", "__PROJECT_NAME__","Program.fs"))
        File.Move(Path.Combine("src", "__PROJECT_NAME__","WinForms.fs"), Path.Combine("src", "__PROJECT_NAME__","Program.fs"))
        deleteInPr ["MediaUI.fs";"RenderModel.fs";"RenderModelApp.fs"; "WPF.fs"]

    Directory.Move(Path.Combine("src", "__PROJECT_NAME__"), target)


printfn "creating template"


preprocess <| "paket.dependencies"
preprocess <| "build.fsx"
preprocess <| Path.Combine("src", "__PROJECT_NAME__", "paket.references")
preprocess <| Path.Combine("src", "__PROJECT_NAME__", "Program.fs")
preprocess <| Path.Combine("src", "__PROJECT_NAME__", "__PROJECT_NAME__.fsproj")
preprocess <| Path.Combine("src", "__SOLUTION_NAME__.sln")
bootSolution()
