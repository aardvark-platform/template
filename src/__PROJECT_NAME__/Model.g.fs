namespace __PROJECT_NAME__.Model

open System
open Aardvark.Base
open Aardvark.Base.Incremental
open __PROJECT_NAME__.Model

[<AutoOpen>]
module Mutable =

    
    
    type MModel(__initial : __PROJECT_NAME__.Model.Model) =
        inherit obj()
        let mutable __current : Aardvark.Base.Incremental.IModRef<__PROJECT_NAME__.Model.Model> = Aardvark.Base.Incremental.EqModRef<__PROJECT_NAME__.Model.Model>(__initial) :> Aardvark.Base.Incremental.IModRef<__PROJECT_NAME__.Model.Model>
        let _currentModel = ResetMod.Create(__initial.currentModel)
        let _cameraState = Aardvark.UI.Primitives.Mutable.MCameraControllerState.Create(__initial.cameraState)
        
        member x.currentModel = _currentModel :> IMod<_>
        member x.cameraState = _cameraState
        
        member x.Current = __current :> IMod<_>
        member x.Update(v : __PROJECT_NAME__.Model.Model) =
            if not (System.Object.ReferenceEquals(__current.Value, v)) then
                __current.Value <- v
                
                ResetMod.Update(_currentModel,v.currentModel)
                Aardvark.UI.Primitives.Mutable.MCameraControllerState.Update(_cameraState, v.cameraState)
                
        
        static member Create(__initial : __PROJECT_NAME__.Model.Model) : MModel = MModel(__initial)
        static member Update(m : MModel, v : __PROJECT_NAME__.Model.Model) = m.Update(v)
        
        override x.ToString() = __current.Value.ToString()
        member x.AsString = sprintf "%A" __current.Value
        interface IUpdatable<__PROJECT_NAME__.Model.Model> with
            member x.Update v = x.Update v
    
    
    
    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module Model =
        [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
        module Lens =
            let currentModel =
                { new Lens<__PROJECT_NAME__.Model.Model, __PROJECT_NAME__.Model.Primitive>() with
                    override x.Get(r) = r.currentModel
                    override x.Set(r,v) = { r with currentModel = v }
                    override x.Update(r,f) = { r with currentModel = f r.currentModel }
                }
            let cameraState =
                { new Lens<__PROJECT_NAME__.Model.Model, Aardvark.UI.Primitives.CameraControllerState>() with
                    override x.Get(r) = r.cameraState
                    override x.Set(r,v) = { r with cameraState = v }
                    override x.Update(r,f) = { r with cameraState = f r.cameraState }
                }
