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

using System.Diagnostics.CodeAnalysis;
using OneS.MediateS.Infrastructures;
using Spectre.Console;
using Spectre.Console.Cli;

public class RootCommand : Command<RootCommandSettings>
{
    public override int Execute([NotNull] CommandContext context, [NotNull] RootCommandSettings settings)
    {
        if(settings.Info)
        {
            PrintInfo();
        }

        return 0;
    }

    private static void PrintInfo()
    {
        // welcome
        AnsiConsole.Write(new Rule($"Welcome to [yellow bold]{MediateSCliConstants.Cli}[/] (repo: [underline blue]https://github.com/maple512/services[/])").RuleStyle("grey").LeftAligned());
        AnsiConsole.WriteLine();

        var info = CliInfo.Instance.Value;
        AnsiConsole.MarkupLine($"{nameof(info.Version)}: {info.Version}");
        AnsiConsole.MarkupLine($"{nameof(info.Commit)}: {info.Commit}");
        AnsiConsole.MarkupLine($"NET Version: {info.Framework}");
        AnsiConsole.MarkupLine($"{nameof(info.RootPath)}: {info.RootPath}");
    }
}

public class RootCommandSettings : CommandSettings
{
    [CommandOption("-i|--info")]
    public bool Info { get; set; }
}
