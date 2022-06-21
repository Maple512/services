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

using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

public class ServiceCommand : CommandBase<ServiceCommandOptions>
{
    private readonly ILogger _logger;

    public ServiceCommand(ILogger<ServiceCommand> logger)
    {
        _logger = logger;
    }

    public override string Name { get; } = "service";

    protected override ValueTask ExecuteCoreAsync(ServiceCommandOptions options)
    {
        return ValueTask.CompletedTask;
    }
}

public class ServiceCommandOptions : CommandOptions
{
    [CommandOptions("name", "n")]
    public string Name { get; set; }
}
