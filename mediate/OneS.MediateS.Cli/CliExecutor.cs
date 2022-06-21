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

namespace OneS.MediateS;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OneF.Moduleable.DependencyInjection;
using OneS.MediateS.Commands;
using Spectre.Console;

public class CliExecutor : ITransientService
{
    private readonly ILogger _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CliExecutor(ILogger<CliExecutor> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async ValueTask ExecuteAsync(string[] args)
    {
        Welcome();

        _logger.LogInformation(args.JoinAsString());

        //var commandName = args

        await using var scope = _serviceScopeFactory.CreateAsyncScope();

        var commands = scope.ServiceProvider.GetServices<ICommandInfo>();


    }

    private static void Welcome()
    {
        AnsiConsole.Write(new Rule($"Welcome to [yellow bold]{MediateSCliConstants.Cli}[/] (repo: [underline blue]https://github.com/maple512/services[/])").RuleStyle("grey").LeftAligned());
    }
}
