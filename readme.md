# Mass Target Framework

CLI to mass modify `.csproj` files to different version of .NET. Mass migrate projects to .NET version with a few lines. This is *not* an extension or addon to Visual Studio, it is an external executable modifying the project files of your solution.

This currently only works for .NET Framework projects - in future there may be support added for .NET Core/Standard projects, but I personally don't require this functionality at this point in time.

### How to

Either fork/clone the repository or grab a release from GitHub.

MassTargetFramework runs on .NET Core 2.2, there are self contained and framework dependent releases available. Please note, this has only been tested on Windows.

Run the executable with the following arguments:
- `sln` required path to the solution you want to migrate, e.g. `C:\Path\To\Solution\file.sln`
- `target` required name of the framework to update to, e.g. `v4.7.2`
- `exclude` optional argument to supply names of projects to ignore/exclude from migrating, space separated, e.g. `My.Project My.Other.Project My.Domain.Project`

Example usages

Migrate all projects in `Solution` to .NET Framework 4.7.2

```
MassTargetFramework.exe -sln="C:\Development\Solution.sln" -target="v4.7.2"
```

Ignore two projects from `Solution` and migrate others to .NET Framework 4.8

```
MassTargetFramework.exe -sln="C:\Development\Solution.sln" -target="v4.8" -exclude="My.Project.Tests My.Project.Tests.Integration"
```

### License

This is licensed under MIT. Steal it, fork it, clone it, sell it, break it, improve it.