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

- Clone/download LiveSplit and this repository
- Open it in [Visual Studio 2019](https://visualstudio.microsoft.com/vs)

## Resources
- Still need help? [Open an issue](../../issues)
- Join the [AutoSplit Discord](https://discord.gg/Qcbxv9y)
