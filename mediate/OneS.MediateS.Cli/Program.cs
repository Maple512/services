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

using System;
using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using OneF.Moduleable;
using OneS.MediateS;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

Console.OutputEncoding = Encoding.UTF8;

const string logTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} [{SourceContext}]{NewLine}{Exception}";

Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Information()
             .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
             .WriteTo.Console(outputTemplate: logTemplate, theme: SystemConsoleTheme.Literate, applyThemeToRedirectedOutput: true)
             .WriteTo.File(path: Path.Combine(CliPath.Log, ".log"), rollingInterval: RollingInterval.Day, outputTemplate: logTemplate)
             .CreateLogger();

var services = new ServiceCollection();

ModuleFactory.ConfigureServices<CliExecutor>(services);

services.AddLogging(
    builder =>
    {
        builder.AddSerilog();
    });

var serviceProvider = services.BuildServiceProvider();

await serviceProvider.ConfigureAsync();

await serviceProvider.GetRequiredService<CliExecutor>()
    .ExecuteAsync(args);

#if DEBUG
Console.ReadKey();
#endif
