module Roke

open Fable.Core.JsInterop
open Fable.Import.Pixi
open Fable.Import.Browser

let textInput = document.getElementById("tutorialText") :?> HTMLInputElement
let display = document.getElementById("display") :?> HTMLDivElement

let appOptions = jsOptions<PIXI.ApplicationOptions> (fun o ->
     o.backgroundColor <- Some 0x000000
)

let app = PIXI.Application(512., 512., appOptions)
display.appendChild(app.view) |> ignore

let renderer : PIXI.WebGLRenderer = !!app.renderer

let textStyle = jsOptions<PIXI.TextStyle>( fun o ->
    o.fontFamily<- !^"Arial"
    o.fontSize<- !^36.
    
    o.fill <- "#ffffff"
    o.strokeThickness<- 5.
)

let pixiText = PIXI.Text("", textStyle)
pixiText.x <- 30.
pixiText.y <- 30.

app.stage.addChild pixiText |> ignore

let textEntered text =
  pixiText.text <- text
  null

textInput.addEventListener_keyup (fun e -> 
  match int e.keyCode with
  | 13 -> textEntered textInput.value
  | _ -> null
)

textInput.focus ()