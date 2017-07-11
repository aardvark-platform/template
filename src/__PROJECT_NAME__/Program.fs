open System
open Aardvark.Base
open Aardvark.Base.Incremental


[<EntryPoint;STAThread>]
let main argv = 
    Ag.initialize()
    Aardvark.Init()

    printfn "%A" V2i.OO
    0
