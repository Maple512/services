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

namespace OneS.MediateS.Infrastructure;

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.Json;
using OneF.Json;

[Serializable]
public class CliInfo
{
    public static readonly Lazy<CliInfo> Instance = new(Initializer);

    public string Version { get; init; }

    public string Commit { get; init; }

    public string Framework { get; init; }

    public string RootPath { get; init; }

    private const string _infoFileName = ".info";

    public static CliInfo Initializer()
    {
        var rootPath = Environment.CurrentDirectory;

        var infoFile = Path.Combine(rootPath, _infoFileName);

        CliInfo cli;

        if(File.Exists(infoFile))
        {
            var fileContent = File.ReadAllBytes(infoFile);

            cli = JsonSerializer.Deserialize<CliInfo>(fileContent)!;
        }
        else
        {
            var assembly = typeof(CliInfo).GetTypeInfo().Assembly;

            var fileVersion = FileVersionInfo.GetVersionInfo(assembly.Location).ProductVersion?.Split('+');

            var version = fileVersion?.FirstOrDefault();
            var commandSha = fileVersion?.LastOrDefault();

            cli = new CliInfo
            {
                RootPath = rootPath,
                Version = version ?? "N/A",
                Commit = commandSha ?? "N/A",
                Framework = RuntimeInformation.FrameworkDescription
            };

            File.WriteAllBytes(infoFile, JsonSerializer.SerializeToUtf8Bytes(cli, JsonSerializerHelper.StandardOptions));
        }

        return cli;
    }
}
