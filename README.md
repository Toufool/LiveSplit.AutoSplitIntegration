# AutoSplit Integration

Directly connects [AutoSplit](https://github.com/Toufool/Auto-Split) with [LiveSplit](https://github.com/LiveSplit/LiveSplit).

## Installation

- Close AutoSplit if it’s currently open
- Download [the `.dll` file](/update/Components/LiveSplit.AutoSplitIntegration.dll?raw=true) and place it into your `[...]\LiveSplit\Components` folder.
- Open LiveSplit -> Right Click -> Edit Layout -> Plus Button -> Control -> AutoSplit Integration.
- Click Layout Settings -> AutoSplit Integration
- Click the Browse buttons to locate your AutoSplit Path (path to `AutoSplit.exe`) and Profile Path (path to your AutoSplit `.toml` profile file) respectively.
  - If you have not yet set saved a profile, you can do so using AutoSplit, and then go back and set your Settings Path.
- Once set, click OK, and then OK again to close the Layout Editor. Right click LiveSplit -> Save Layout to save your layout. AutoSplit and your selected profile will now open automatically when opening that LiveSplit Layout `.lsl` file.

## Opening/Closing AutoSplit

- To reopen AutoSplit if you accidentally closed it, right click LiveSplit and choose Control → Start AutoSplit.
- If you want to close AutoSplit, just close its window. Alternatively, if AutoSplit is for some reason not responding, you can kill it in the same menu where you can reopen it. This way your settings won’t be saved though.
- You can also find the options to reopen/kill AutoSplit in the component’s settings.

## Compiling

Requirements:

- The [.NET SDK 10.x](https://dotnet.microsoft.com/download)
- The [.NET Framework 4.8.1 Developer Pack](https://dotnet.microsoft.com/download/dotnet-framework/net481)
- Optionally [Visual Studio 2022](https://visualstudio.microsoft.com/vs)

Then either:

- Run `dotnet build --configuration Release`, or
- Open `LiveSplit.AutoSplitIntegration.csproj` in Visual Studio 2022 and build.

The LiveSplit assemblies needed to compile (`LiveSplit.exe`, `LiveSplit.Core.dll`,
`UpdateManager.dll`) are vendored under [`lib/`](lib/) — you do **not** need a separate
LiveSplit checkout. See [`lib/README.md`](lib/README.md) to refresh them.

## Releasing a new version

The plugin uses LiveSplit's built-in component auto-updater: each LiveSplit install
polls the factory's `XMLURL`, and offers an update when a listed `<update version="…">`
is newer than the installed component's `Version`.

To publish a release:

1. Bump the version in **both**
   [`UI/Components/AutoSplitIntegrationFactory.cs`](UI/Components/AutoSplitIntegrationFactory.cs)
   (`Version`) and the `Version`/`AssemblyVersion`/`FileVersion` properties in
   [`LiveSplit.AutoSplitIntegration.csproj`](LiveSplit.AutoSplitIntegration.csproj).
2. Prepend a new `<update version="…">` entry, with a changelog, to
   [`update/Components/update.LiveSplit.AutoSplitIntegration.xml`](update/Components/update.LiveSplit.AutoSplitIntegration.xml).
3. Build `Release` — the DLL is produced under `update/Components/` (Debug) / the
   configured Release output. Commit the rebuilt
   `update/Components/LiveSplit.AutoSplitIntegration.dll`.
4. Commit and push to the branch the updater URLs point at
   (`Toufool/LiveSplit.AutoSplitIntegration`, `main`), then create a GitHub release.

## Resources

- Still need help? [Open an issue](../../issues)
- Join the [AutoSplit Discord](https://discord.gg/Qcbxv9y)
