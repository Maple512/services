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
using OneF;

[DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
public readonly struct ConfigItem : IEquatable<ConfigItem>
{
    public ConfigItem(ConfigKey key, ConfigValue? value)
    {
        Key = Check.NotNull(key);

        Value = value;
    }

    public ConfigKey Key { get; }

    public ConfigValue? Value { get; }

    public bool Equals(ConfigItem other)
        => Key.Equals(other.Key);

    public override bool Equals(object? obj)
        => obj is ConfigItem item
        && Equals(item);

    public override int GetHashCode()
        => Key.GetHashCode();

    private string GetDebuggerDisplay()
    {
        return $"{Key}: {Value} [Config]";
    }
}
