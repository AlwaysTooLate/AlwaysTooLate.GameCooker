# AlwaysTooLate.GameCooker
AlwaysTooLate's game cooking utility. Provides per-platform build settings and more.

# Installation

Before installing this module, be sure to have installed these:

- [AlwaysTooLate.Core](https://github.com/AlwaysTooLate/AlwaysTooLate.Core)

Open your target project in Unity and use the Unity Package Manager (`Window` -> `Package Manager` -> `+` -> `Add package from git URL`) and paste the following URL:
https://github.com/AlwaysTooLate/AlwaysTooLate.GameCooker.git

# Basic Usage

In order to open the GameCooker's window, select `Tools` -> `Always Too Late` -> `GameCooker` -> `Open`. From there, you can browse, create, edit or delete Presets and Targets.

Presents are used for grouping Targets. Targets are be used for configuring the build's target platform, type (Debug or Release) and define symbols, plus some additional settings.

After selecting a Target from the window, you can start building by selecting `Tools` -> `Always Too Late` -> `GameCooker` -> `Build` or pressing `Alt`+`B`. Alternatively, you may select the `Scripts only` build (`Alt`+`Shift`+`B`) - warning, it is not a working feature yet.

![cooker](https://user-images.githubusercontent.com/7634316/79242148-3ba1ff80-7e74-11ea-8e11-c04fc93b135c.png)

# Continuous integration
You can use script like this, to commit builds by your CI.
```CSharp
public static class JenkinsBuild
{
	public const int ReleasePreset = 1;

	public static void Build()
	{
		try
		{
			// We could actually use the cooker directly in Jenkins configuration,
			// but Erdroy is lazy.
			// And this gives us some additional space for additional features.
			CookerWindow.Build(ReleasePreset);
		}
		catch (Exception e)
		{
			// Print out the exception, and exit with fail result (-1)
			Debug.LogException(e);
			//Environment.Exit(-1); // Note: This is causing Unity to hang...
		}
	}
}
```
Then invoke Unity from CI like in every other solution: 
`-quit -batchmode -nographics -projectPath YOUR PROJECT -executeMethod JenkinsBuild.Build`

# Contribution

We do accept PR. Feel free to contribute. If you want to find us, look for the ATL staff on the official [AlwaysTooLate Discord Server](https://discord.alwaystoolate.com/)

*AlwaysTooLate.Logging (c) 2018-2020 Always Too Late.*
