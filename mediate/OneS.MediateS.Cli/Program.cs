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
using System.Text;
using OneS.MediateS.Commands;
using Spectre.Console.Cli;

if(Console.IsOutputRedirected)
{
    Console.OutputEncoding = Encoding.UTF8;
}

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var app = new CommandApp<RootCommand>();

app.Configure(config =>
{
#if DEBUG
    config.PropagateExceptions();
    config.ValidateExamples();
#endif
});

#if DEBUG
args = "--info".Split(' ');
#endif

await app.RunAsync(args);

#if DEBUG
Console.ReadKey();
#endif
