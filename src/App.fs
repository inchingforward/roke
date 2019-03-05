module Roke

open Fable.Core.JsInterop
open Fable.Import.Pixi
open Fable.Import.Browser
open Fable.Import.Pixi.PIXI

let textInput = document.getElementById("tutorialText") :?> HTMLInputElement
let display = document.getElementById("display") :?> HTMLDivElement

let appOptions = jsOptions<PIXI.ApplicationOptions> (fun o ->
     o.backgroundColor <- Some 0x0C7ECF
)

let app = PIXI.Application(512., 512., appOptions)
display.appendChild(app.view) |> ignore

let renderer : PIXI.WebGLRenderer = !!app.renderer

let roadEW = PIXI.Sprite.fromImage("tiles/roadEW.png")

roadEW.x <- 100.
roadEW.y <- 200.

// height of tile: 100 x 58
let roadEW2 = PIXI.Sprite.fromImage("tiles/roadEW.png")
roadEW2.x <- roadEW.x + 48.
roadEW2.y <- roadEW.y - 24.

let grass = PIXI.Sprite.fromImage("tiles/grass.png")
grass.x <- roadEW.x + 48.
grass.y <- roadEW.y + 24.

let grass2 = PIXI.Sprite.fromImage("tiles/grass.png")
grass2.x <- roadEW.x + roadEW.x - 4.
grass2.y <- roadEW.y

let crossRoadESW = PIXI.Sprite.fromImage("tiles/crossroadESW.png")
crossRoadESW.x <- roadEW.x + roadEW.x - 4.
crossRoadESW.y <- roadEW.y - 48.

let endN = PIXI.Sprite.fromImage("tiles/endN.png")
endN.x <- roadEW2.x + 96.
endN.y <- roadEW2.y

app.stage.addChild crossRoadESW |> ignore
app.stage.addChild roadEW2 |> ignore
app.stage.addChild roadEW |> ignore
app.stage.addChild endN |> ignore
app.stage.addChild grass2 |> ignore
app.stage.addChild grass |> ignore

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