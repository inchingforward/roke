# Roke

An experiment in teaching F#.

Latest build deployed [here](http://roke.mikejanger.net).

## Building and running the app

* Move to the project's root directory: `cd MyCoolProject`
* Install JS dependencies: `yarn install`
* Move to `src` folder: `cd src`
* Install F# dependencies: `dotnet restore`
* Start Fable daemon and [Webpack](https://webpack.js.org/) dev server: `dotnet fable yarn-start`
* In your browser, open: http://localhost:8080/

## Making a production build

    dotnet fable npm-run build
