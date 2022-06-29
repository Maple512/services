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
using OneF;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public readonly struct ConfigKey : IEquatable<ConfigKey>, IComparable<ConfigKey>
{
    public const char Separator = '.';

    public ConfigKey(string key)
    {
        Key = Check.NotNullOrWhiteSpace(key);
    }

    public string Key { get; }

    /// <summary>
    /// bytes length, see <see cref="UTF8Encoding.GetByteCount(string)"/>
    /// </summary>
    public int Length => Encoding.UTF8.GetByteCount(Key);

    public int CompareTo(ConfigKey other)
        => StringComparer.OrdinalIgnoreCase.Compare(Key, other.Key);

    public bool Equals(ConfigKey other)
        => StringComparer.OrdinalIgnoreCase.Equals(Key, other.Key);

    public override bool Equals(object? obj) => obj switch
    {
        null => false,
        ConfigKey c => Equals(c),
        string s => StringComparer.OrdinalIgnoreCase.Equals(Key, s),
        byte[] bs => MemoryExtensions.SequenceEqual((ReadOnlySpan<byte>)bs, (byte[])this),
        _ => false,
    };

    public override int GetHashCode()
        => HashCode.Combine(Key);

    public bool Contains(ConfigKey other)
        => Key.Contains(other.Key);

    public static implicit operator string(ConfigKey key)
        => key.Key;

    public static implicit operator ConfigKey(string data)
        => new(data);

    public static implicit operator byte[](ConfigKey key)
        => Encoding.UTF8.GetBytes(key.Key);

    public static implicit operator ConfigKey(byte[] data)
        => new(Encoding.UTF8.GetString(data));

    public static bool operator ==(ConfigKey left, ConfigKey right)
          => left.Equals(right);

    public static bool operator !=(ConfigKey left, ConfigKey right)
          => !left.Equals(right);

    private string GetDebuggerDisplay()
    {
        return $"{nameof(Key)}: {Key} [{nameof(ConfigKey)}]";
    }
}
