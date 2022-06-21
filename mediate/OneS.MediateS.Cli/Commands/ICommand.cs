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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using OneF.Moduleable.DependencyInjection;

public interface ICommandInfo : ITransientService
{
    /// <summary>
    /// 名称
    /// </summary>
    string Name { get; }

    /// <summary>
    /// 描述
    /// </summary>
    string? Description { get; }

    /// <summary>
    /// 示例
    /// </summary>
    string? Example { get; }

    /// <summary>
    /// 更多信息描述
    /// </summary>
    string? MoreInfo { get; }
}

public interface ICommand : ICommandInfo
{
    /// <summary>
    /// 执行
    /// </summary>
    /// <returns></returns>
    ValueTask ExecuteAsync();
}

public interface ICommand<in TOptions> : ICommandInfo
    where TOptions : class, ICommandOptions, new()
{
    /// <summary>
    /// 执行
    /// </summary>
    /// <param name="arguments"></param>
    /// <returns></returns>
    ValueTask ExecuteAsync(IEnumerable<string> arguments);
}

public abstract class CommandInfo : ICommandInfo
{
    public abstract string Name { get; }

    public virtual string? Description { get; }

    public virtual string? Example { get; }

    public virtual string? MoreInfo { get; }
}

public abstract class CommandBase : CommandInfo, ICommand
{
    public abstract ValueTask ExecuteAsync();
}

public abstract class CommandBase<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TOptions> : CommandInfo, ICommand<TOptions>
    where TOptions : class, ICommandOptions, new()
{
    public virtual async ValueTask ExecuteAsync(IEnumerable<string> arguments)
    {
        var options = Parser(arguments);

        await ExecuteCoreAsync(options);
    }

    protected abstract ValueTask ExecuteCoreAsync(TOptions options);

    protected TOptions Parser(IEnumerable<string> arguments)
    {
        var options = new TOptions();

        while(arguments.Any())
        {
            var optionName = ParseOptionName(arguments.First());


        }

        return options;
    }

    private static bool IsOptionName(string argument)
    {
        return argument.StartsWith("-") || argument.StartsWith("--");
    }

    private static string ParseOptionName(string argument)
    {
        if(argument.StartsWith("--"))
        {
            if(argument.Length <= 2)
            {
                throw new ArgumentException("Should specify an option name after '--' prefix!");
            }

            return argument[2..];
        }

        if(argument.StartsWith("-"))
        {
            if(argument.Length <= 1)
            {
                throw new ArgumentException("Should specify an option name after '-' prefix!");
            }

            return argument[1..];
        }

        throw new ArgumentException("Option names should start with '-' or '--'.");
    }
}
