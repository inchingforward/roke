module Roke

open Fable.Core.JsInterop
open Fable.Import.Pixi
open Fable.Import.Browser
open Fable.Import.Pixi.PIXI

let blockWidth = 64.
let blockHeight = 100.

let textInput = document.getElementById("tutorialText") :?> HTMLInputElement
let leftButton = document.getElementById("leftButton") :?> HTMLButtonElement
let rightButton = document.getElementById("rightButton") :?> HTMLButtonElement
let upButton = document.getElementById("upButton") :?> HTMLButtonElement
let downButton = document.getElementById("downButton") :?> HTMLButtonElement
let display = document.getElementById("display") :?> HTMLDivElement

let appOptions = jsOptions<PIXI.ApplicationOptions> (fun o ->
     o.backgroundColor <- Some 0x0C7ECF
)

let app = PIXI.Application(512., 512., appOptions)
display.appendChild(app.view) |> ignore

let renderer : PIXI.WebGLRenderer = !!app.renderer

let grassTile = PIXI.Sprite.fromImage("blocks/tileGrass.png")
grassTile.x <- 100.
grassTile.y <- 200.

let grassTile2 = PIXI.Sprite.fromImage("blocks/tileGrass.png")
grassTile2.x <- grassTile.x + blockWidth
grassTile2.y <- grassTile.y

let grassTile3 = PIXI.Sprite.fromImage("blocks/tileGrass.png")
grassTile3.x <- grassTile2.x + blockWidth
grassTile3.y <- grassTile2.y

let grassTile4 = PIXI.Sprite.fromImage("blocks/tileGrass.png")
grassTile4.x <- grassTile3.x + blockWidth
grassTile4.y <- grassTile3.y

let waterTile1 = PIXI.Sprite.fromImage("blocks/tileWater_1.png")
waterTile1.x <- grassTile.x
waterTile1.y <- grassTile.y - blockWidth

let waterTile2 = PIXI.Sprite.fromImage("blocks/tileWater_2.png")
waterTile2.x <- waterTile1.x + blockWidth
waterTile2.y <- waterTile1.y

let waterTile3 = PIXI.Sprite.fromImage("blocks/tileWater_3.png")
waterTile3.x <- waterTile2.x + blockWidth
waterTile3.y <- waterTile2.y

let waterTile4 = PIXI.Sprite.fromImage("blocks/tileWater_4.png")
waterTile4.x <- waterTile3.x + blockWidth
waterTile4.y <- waterTile3.y

let grassTile5 = PIXI.Sprite.fromImage("blocks/tileGrass.png")
grassTile5.x <- waterTile1.x
grassTile5.y <- waterTile1.y - blockWidth

let grassTile6 = PIXI.Sprite.fromImage("blocks/tileGrass.png")
grassTile6.x <- grassTile5.x + blockWidth
grassTile6.y <- grassTile5.y

let grassTile7 = PIXI.Sprite.fromImage("blocks/tileGrass.png")
grassTile7.x <- grassTile6.x + blockWidth
grassTile7.y <- grassTile6.y

let grassTile8 = PIXI.Sprite.fromImage("blocks/tileGrass.png")
grassTile8.x <- grassTile7.x + blockWidth
grassTile8.y <- grassTile7.y

let wizard = PIXI.Sprite.fromImage("blocks/character_wizard.png")
wizard.x <- grassTile.x 
wizard.y <- grassTile.y - 48.

// Must be built from top down
app.stage.addChild grassTile5 |> ignore
app.stage.addChild grassTile6 |> ignore
app.stage.addChild grassTile7 |> ignore
app.stage.addChild grassTile8 |> ignore

app.stage.addChild waterTile1 |> ignore
app.stage.addChild waterTile2 |> ignore
app.stage.addChild waterTile3 |> ignore
app.stage.addChild waterTile4 |> ignore

app.stage.addChild grassTile |> ignore
app.stage.addChild grassTile2 |> ignore
app.stage.addChild grassTile3 |> ignore
app.stage.addChild grassTile4 |> ignore

app.stage.addChild wizard |> ignore


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

rightButton.addEventListener_click (fun e ->
  wizard.x <- wizard.x + blockWidth
  null
)

leftButton.addEventListener_click (fun e ->
  wizard.x <- wizard.x - blockWidth
  null
)

upButton.addEventListener_click (fun e -> 
  wizard.y <- wizard.y - blockWidth
  null
)

downButton.addEventListener_click ( fun e ->
  wizard.y <- wizard.y + blockWidth
  null
)

let textEntered text =
  pixiText.text <- text
  null

textInput.addEventListener_keyup (fun e -> 
  match int e.keyCode with
  | 13 -> textEntered textInput.value
  | _ -> null
)

window.addEventListener_keyup (fun e ->
  match int e.keyCode with
  | 37 -> wizard.x <- wizard.x - blockWidth // Left
  | 39 -> wizard.x <- wizard.x + blockWidth // Right
  | 40 -> wizard.y <- wizard.y + blockWidth // Down
  | 38 -> wizard.y <- wizard.y - blockWidth // Up
  | _ -> ()

  null
)

let animationSpeed = 0.08
let animationUpdateTime = 1.0 / animationSpeed
let mutable timeSinceLastFrameSwap = 0.0

let tick delta = 
  timeSinceLastFrameSwap <- timeSinceLastFrameSwap + delta

  if timeSinceLastFrameSwap > animationUpdateTime then
    timeSinceLastFrameSwap <- 0.
    let tempTexture = waterTile1.texture
    waterTile1.texture <- waterTile2.texture
    waterTile2.texture <- waterTile3.texture
    waterTile3.texture <- waterTile4.texture
    waterTile4.texture <- tempTexture
  ()

app.ticker.add(tick) |> ignore

//textInput.focus ()