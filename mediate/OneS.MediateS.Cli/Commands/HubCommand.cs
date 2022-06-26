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
using Spectre.Console.Cli;

[Description("包含一些与 Hub 相关的功能")]
public sealed class HubCommand : Command<HubCommandSettings>
{
    public override int Execute([NotNull] CommandContext context, [NotNull] HubCommandSettings settings)
    {
        throw new NotImplementedException();
    }
}

public sealed class HubCommandSettings : CommandSettings
{

}
