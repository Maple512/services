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

using System.Threading.Tasks;
using Shouldly;
using Xunit;

public class ConfigManager_Test : TestBase
{
    private readonly IConfigManager _configManager;

    public ConfigManager_Test()
    {
        _configManager = GetRequiredService<IConfigManager>();
    }

    [Fact]
    public async Task Set_configAsync()
    {
        ConfigKey key = "a.b.c.d";

        var result = await _configManager.SetAsync(key, null);

        result.ShouldBeTrue();

        var config = await _configManager.GetAsync(key);

        config.HasValue.ShouldBeTrue();

        config.Value.Value.ShouldBeNull();

        var value = new ConfigValue();

        value.IsEmpty.ShouldBeTrue();

        result = await _configManager.SetAsync(key, value);

        result.ShouldBeFalse();
    }
}
