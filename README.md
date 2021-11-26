# AutoSplit Integration

Directly connects [AutoSplit](https://github.com/Toufool/Auto-Split) with [LiveSplit](https://github.com/LiveSplit/LiveSplit).

## Installation

- Close AutoSplit if it’s currently open
- Download [the `.dll` file](/update/Components/LiveSplit.AutoSplitIntegration.dll?raw=true) and move it into LiveSplit’s `Components` folder
- Edit your layout, click the plus and choose Control → AutoSplit Integration
- Click Layout Settings → AutoSplit Integration → Browse... and select your `AutoSplit.exe`. You can find compatible AutoSplit versions [here](https://github.com/Toufool/Auto-Split)

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
