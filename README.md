[![Discord](https://badgen.net/discord/online-members/UyecnhM)](https://discord.gg/UyecnhM)
[![license](https://img.shields.io/github/license/aardvark-platform/template.svg)](https://github.com/aardvark-platform/template/blob/master/LICENSE)

[The Aardvark Platform](https://aardvarkians.com/) |
[Gallery](https://github.com/aardvark-platform/aardvark.docs/wiki/Gallery) | 
[Packages&Repositories](https://github.com/aardvark-platform/aardvark.docs/wiki/Packages-and-Repositories)

This repository is part of the open-source [Aardvark Platform](https://github.com/aardvark-platform) for visual computing, real-time graphics, and visualization. This repository provides an empty template Aardvark project, which is bootstrapped by running `build.cmd` or `./build.sh`. 

You need the [.NET SDK](https://dotnet.microsoft.com/en-us/download) installed. Your editor (Visual Studio, Jetbrains Rider, VS Code) should have F# language support installed. Supported platforms are windows, linux, macOS.

# Building and Running

Invoking `build.cmd` or `./build.sh` runs an interactive shell script where you input name and type (Rendering/Media) of your Aardvark project. This generates a .sln file which is ready to be opened in your IDE of choice.

Build and run from command line:

``dotnet run -c Release -p .\src\PROJECTNAME\PROJECTNAME.fsproj``

# Resources and Literature

[A video walkthrough for linux (sound on)](https://www.youtube.com/watch?v=61WFmpmEg-M) and [a video walkthrough for windows (sound on)](https://www.youtube.com/watch?v=uFtN9J52-nw) are available. Further Aardvark documentation and walkthroughs are hosted [in the aardvark.docs wiki](https://github.com/aardvark-platform/aardvark.docs/wiki). An alternative way of creating an empty Aardvark project [is the dotnet template](https://github.com/aardvark-platform/aardvark.templates/edit/master/README.md).
