[![NuGet](https://img.shields.io/nuget/v/Droplex.svg)](https://nuget.org/packages/Droplex)

# Droplex: Installation when the app needs it

Droplex is a library that allows applications to install additional tools, programs and services when needed rather than have them bundled and installed along with the main app during initial setup. 

The goal is to give applications the ability to manage its own non-interactive installation of needed dependencies when dropped in to a vanilla windows environment with an internet connection, without the need to install and use package managers.

## Installation
Droplex is available as a NuGet [package](https://www.nuget.org/packages/Droplex/). You can [install](http://docs.nuget.org/docs/start-here/installing-nuget) it using the NuGet Package Console window:

```
PM> Install-Package Droplex
```

## Usage

`await DroplexPackage.Drop(App.python3_9_1).ConfigureAwait(false);`

and you are done. Or,

Run in background:
```
Task pyInstall = DroplexPackage.Drop(App.python3_9_1); 
...
pyInstall.Wait();
```

Or,

Run and forget:
`_= DroplexPackage.Drop(App.python3_9_1);`


## Add your own app
1. Add your app in [Droplex.Configuration.yml](https://github.com/jjw24/Droplex/blob/main/Droplex/Droplex.Configuration.yml) file
2. Add it also in the [App.cs](https://github.com/jjw24/Droplex/blob/main/Droplex/App.cs) list
3. Test it with [Droplex.Test.Console](https://github.com/jjw24/Droplex/tree/main/Droplex.Test.Console) to make sure it installs non-interactively or silently
4. Submit a PR
