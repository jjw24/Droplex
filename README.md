# Droplex: Installation when the app needs it

Droplex is a library that allows applications to install additional tools, programs and services when needed rather than have them bundled and installed along with the main app during initial setup. 

The goal is to give applications the ability to manage its own non-interactive installation of needed dependencies when dropped in to a vanilla windows environment with an internet connection, without the need to install and use package managers.

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

### Currently WIP, will publish to NuGet when done
