# AutoSplit Integration [![CI](/../../actions/workflows/ci.yml/badge.svg)](/../../actions/workflows/build.yml) [![SemVer](https://badgen.net/badge/_/SemVer%20compliant/grey?label)](https://semver.org/)

Directly connects [AutoSplit](https://github.com/Toufool/Auto-Split) with [LiveSplit](https://github.com/LiveSplit/LiveSplit).

## Installation

- Close AutoSplit if it’s currently open
- Download [the `.dll` file](../../releases/latest/download/LiveSplit.AutoSplitIntegration.dll) and place it into your `[...]\LiveSplit\Components` folder.
  - Optionally also download the matching [`.pdb` file](../../releases/latest/download/LiveSplit.AutoSplitIntegration.pdb) into the same folder. It adds source line numbers to crash reports, which helps when reporting bugs.
- Open LiveSplit -> Right Click -> Edit Layout -> Plus Button -> Control -> AutoSplit Integration.
- Click Layout Settings -> AutoSplit Integration
- Click the Browse buttons to locate your AutoSplit Path (path to `AutoSplit.exe`) and Profile Path (path to your AutoSplit `.toml` profile file) respectively.
  - If you have not yet set saved a profile, you can do so using AutoSplit, and then go back and set your Settings Path.
- Once set, click OK, and then OK again to close the Layout Editor. Right click LiveSplit -> Save Layout to save your layout. AutoSplit and your selected profile will now open automatically when opening that LiveSplit Layout `.lsl` file.

## Opening/Closing AutoSplit

- To reopen AutoSplit if you accidentally closed it, right click LiveSplit and choose Control → Start AutoSplit.
- If you want to close AutoSplit, just close its window. Alternatively, if AutoSplit is for some reason not responding, you can kill it in the same menu where you can reopen it. This way your settings won’t be saved though.
- You can also find the options to reopen/kill AutoSplit in the component’s settings.

## Contributing

Building from source and contributing guidelines are documented in [`CONTRIBUTING.md`](CONTRIBUTING.md).

## Resources

Still need help?

<!-- open issues sorted by reactions -->

- [Check if your issue already exists](../../../AutoSplit/issues?q=is%3Aissue+is%3Aopen+sort%3Areactions-%2B1-desc)
  - If it does, upvote it 👍
  - If it doesn't, create a new one
- Join the [AutoSplit Discord\
  ![AutoSplit Discord](https://badgen.net/discord/members/Qcbxv9y)](https://discord.gg/Qcbxv9y)
