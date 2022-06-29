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

namespace OneS.ConfigS;

using System;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using OneF.Json;

/// <summary>
/// 配置 - 值
/// </summary>
[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public readonly struct ConfigValue : IEquatable<ConfigValue>
{
    public ConfigValue(ReadOnlyMemory<byte> data)
    {
        Data = data;
    }

    public ReadOnlyMemory<byte> Data { get; }

    public bool IsEmpty => Data.IsEmpty;

    public bool Equals(ConfigValue other)
        => Data.Length == other.Data.Length
        && MemoryExtensions.SequenceEqual(Data.Span, other.Data.Span);

    public override bool Equals(object? obj)
        => obj is not null and ConfigValue value
        && Equals(value);

    public static bool operator ==(ConfigValue x, ConfigValue y)
        => x.Equals(y);

    public static bool operator !=(ConfigValue x, ConfigValue y)
        => !x.Equals(y);

    public static implicit operator string(ConfigValue value)
        => Encoding.UTF8.GetString(value.Data.Span);

    public static implicit operator ConfigValue(string value)
        => new ConfigValue(Encoding.UTF8.GetBytes(value));

    public override int GetHashCode()
        => Data.GetHashCode();

    private string GetDebuggerDisplay()
        => JsonSerializer.Serialize(Data, JsonSerializerHelper.StandardOptions);
}
