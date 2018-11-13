[![Join the chat at https://gitter.im/aardvark-platform/Lobby](https://img.shields.io/badge/gitter-join%20chat-blue.svg)](https://gitter.im/aardvark-platform/Lobby)
[![license](https://img.shields.io/github/license/aardvark-platform/template.svg)](https://github.com/aardvark-platform/template/blob/master/LICENSE)

[Wiki](https://github.com/aardvarkplatform/aardvark.docs/wiki) | 
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

#### Important note for VisualStudio 2017 users

If you are using Visual Studio 2017, you might miss the `FSHARPINSTALLDIR` variable. In our [script](https://github.com/aardvark-platform/template/blob/master/build.cmd#L14) we use it to find fsi in order to bootstrap the project. As a workaround use visual studio 2017 developer command prompt to execute `build.cmd` the first time [seealso](https://github.com/Microsoft/visualfsharp/issues/55129).

A [video](https://www.youtube.com/watch?v=61WFmpmEg-M) shows how to use the template :)

A __visual studio solution__ inclusive project file is generated.

After successfully setting up your project, __build.cmd__ or __build.sh__ triggers our [build script](https://github.com/aardvark-platform/Aardvark.Fake).
