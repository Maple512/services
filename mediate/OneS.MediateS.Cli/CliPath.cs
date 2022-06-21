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

using System;
using System.IO;

public static class CliPath
{
#if DEBUG
    public static readonly string Root = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), MediateSCliConstants.RootFolderName, "DEBUG");
#else
    public static readonly string Root = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), MediateSCliConstants.RootFolderName);
#endif
    public static readonly string Cli = Path.Combine(Root, "cli");

    public static readonly string Log = Path.Combine(Cli, "logs");

    public static readonly string SettingFile = Path.Combine(Cli, MediateSCliConstants.SettingsFileName);
}
