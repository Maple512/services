// Copyright 2021 Maple512 and Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License"),
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace OneS.MediateS.Commands;

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using OneF.Shells;
using OneS.MediateS.Infrastructure;
using Spectre.Console;
using Spectre.Console.Cli;

[Description("包含一些与本CLI相关的功能（检查版本，更新，卸载等）")]
public sealed class CliCommand : Command<CliCommandSettings>
{
    public override int Execute([NotNull] CommandContext context, [NotNull] CliCommandSettings settings)
    {
        if(settings.Remove)
        {
            Printer.Loading("卸载中...", () => RemoveAsync().Wait());
        }
        else if(settings.Check)
        {
            Printer.Loading("检查中...", () => CheckVersionAsync().Wait());
        }
        else if(settings.Update)
        {
            Printer.Loading("更新中...", () => UpdateAsync(settings.Prerelease).Wait());
        }

        return 0;
    }

    private const string _nugetUrl = "https://api.nuget.org/v3/index.json";

    private static async Task CheckVersionAsync()
    {
        var cancellationToken = CancellationToken.None;
        var cache = new SourceCacheContext();
        var repository = Repository.Factory.GetCoreV3(_nugetUrl);
        var resource = repository.GetResource<FindPackageByIdResource>();

        AnsiConsole.WriteLine("Nuget:");

        var versions = await resource.GetAllVersionsAsync(
            MediateSCliConstants.CliFullName,
            cache,
            NugetLogger.Instance,
            CancellationToken.None);

        var latestVersion = versions.Max(ver => ver.Version)!;

        var currentVersion = Version.Parse(CliInfo.Instance.Value.Version);

        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine($"当前版本: {currentVersion}");
        AnsiConsole.WriteLine($"最新版本: {latestVersion}");
        AnsiConsole.WriteLine($"使用命令 `dotnet tool update {MediateSCliConstants.CliFullName} -g` 来更新到最新版本");
    }

    private static async Task RemoveAsync()
    {
        AnsiConsole.MarkupLine($"running: [underline]dotnet tool search {MediateSCliConstants.CliFullName}[/]");
        AnsiConsole.WriteLine();

        _ = await ProcessRunner.Start("dotnet", $"tool search {MediateSCliConstants.CliFullName}")
            .WithOutput(WriteMessage)
            .RunAsync();
    }

    private static async Task UpdateAsync(bool prerelease)
    {
        string? preStr = null;

        if(prerelease)
        {
            preStr = "--prerelease";
        }

        var args = $"tool update {MediateSCliConstants.CliFullName} -g {preStr}";

        AnsiConsole.MarkupLine($"running: [underline]dotnet {args}[/]");
        AnsiConsole.WriteLine();

        _ = await ProcessRunner.Start("dotnet", args)
           .WithOutput(WriteMessage)
           .RunAsync();
    }

    private static void WriteMessage(bool isError, string? msg)
            => AnsiConsole.WriteLine(msg ?? string.Empty);
}

public sealed class CliCommandSettings : CommandSettings
{
    [Description("检查CLI的版本")]
    [CommandOption("-c|--check-version")]
    public bool Check { get; set; }

    [Description("更新CLI到最新或指定版本")]
    [CommandOption("-u|--update")]
    public bool Update { get; set; }

    [Description("指定版本，默认使用最新版本（更新时可用）")]
    [CommandArgument(1, "[version]")]
    public string? Version { get; set; }

    [DefaultValue(false)]
    [Description("是否更新到预览版（更新时可用）")]
    [CommandOption("-p|--prerelease")]
    public bool Prerelease { get; set; } = false;

    [Description("卸载CLI")]
    [CommandOption("-r|--remove")]
    public bool Remove { get; set; }
}
