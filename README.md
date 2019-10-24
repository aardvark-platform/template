[![Join the chat at https://gitter.im/aardvark-platform/Lobby](https://img.shields.io/badge/gitter-join%20chat-blue.svg)](https://gitter.im/aardvark-platform/Lobby)
[![license](https://img.shields.io/github/license/aardvark-platform/template.svg)](https://github.com/aardvark-platform/template/blob/master/LICENSE)

[The Aardvark Platform](https://aardvarkians.com/) |
[Platform Wiki](https://github.com/aardvarkplatform/aardvark.docs/wiki) | 
[Gallery](https://github.com/aardvarkplatform/aardvark.docs/wiki/Gallery) | 
[Quickstart](https://github.com/aardvarkplatform/aardvark.docs/wiki/Quickstart-Windows) | 
[Status](https://github.com/aardvarkplatform/aardvark.docs/wiki/Status)

This repository is part of the open-source [Aardvark platform](https://github.com/aardvark-platform/aardvark.docs/wiki) for visual computing, real-time graphics and visualization.

## Template

This template contains tools for quickly setting up your own interactive visualization app powerd by __Aardvark__.

### Requirement

Our **quickstart** for [Windows](https://github.com/aardvark-platform/aardvark.docs/wiki/Visual-Studio) and [Linux](https://github.com/aardvark-platform/aardvark.docs/wiki/Linux-Support) smoothes the path for your Aardvark experience! 

### How to start

To create a new project run __build.cmd__ or __build.sh__ and follow the instructions.

To run the example use:
``dotnet run -c Release -p .\src\{name of the project name you used}\{name of the project name you used}.fsproj``

If you want to run the compiled version directly, make sure to setup the working directory or `cd` to it:
``cd bin\Release\netcoreapp2.0\{name of the project name you used}
dotnet {name of the project name you used}.dll
``


A [video(linux)](https://www.youtube.com/watch?v=61WFmpmEg-M) [video(windows)](https://www.youtube.com/watch?v=uFtN9J52-nw) shows how we used this repository to setup everything including vscode.
A __visual studio solution__ inclusive project file is generated.

After successfully setting up your project, __build.cmd__ or __build.sh__ triggers our [build script](https://github.com/aardvark-platform/Aardvark.Fake).
