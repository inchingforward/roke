module Roke

open Fable.Core.JsInterop
open Fable.Import.Pixi
open Fable.Import.Browser

let textInput = document.getElementById("tutorialText") :?> HTMLInputElement
let display = document.getElementById("display") :?> HTMLDivElement

let options = jsOptions<PIXI.ApplicationOptions> (fun o ->
     o.backgroundColor <- Some 0x000000
)

let app = PIXI.Application(512., 512., options)
display.appendChild(app.view) |> ignore

// create a new Sprite from an image path
let bunny = PIXI.Sprite.fromImage("fable_logo_small.png")

let renderer : PIXI.WebGLRenderer = !!app.renderer

// center the sprite's anchor point
bunny.anchor.set(0.5)
bunny.x <- renderer.width * 0.5
bunny.y <- renderer.height * 0.5

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
  console.log(text)
  //display.innerHTML <- text
  //let pixiText = PIXI.Text(text, textStyle)
  //pixiText.x <- 30.
  //pixiText.y <- 30.

  //app.stage.addChild pixiText |> ignore
  pixiText.text <- text
  null

textInput.addEventListener_keyup (fun e -> 
  match int e.keyCode with
  | 13 -> textEntered textInput.value
  | _ -> null
)

textInput.focus ()