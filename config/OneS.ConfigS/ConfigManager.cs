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

using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OneF.Moduleable.DependencyInjection;

public class ConfigManager : IConfigManager, ISingletonService
{
    private static readonly ConcurrentDictionary<ConfigKey, ConfigItem> _cache = new();

    public async ValueTask<IEnumerable<ConfigItem>> GetAllAsync(ConfigKey? key, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await ValueTask.CompletedTask;

        if(key is null)
        {
            return _cache.Select(x => x.Value);
        }

        return _cache.Where(x => key.Value.Contains(x.Key))
            .Select(x => x.Value);
    }

    public async ValueTask<ConfigItem?> GetAsync(ConfigKey key, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await ValueTask.CompletedTask;

        return _cache.GetValueOrDefault(key);
    }

    public async ValueTask<bool> SetAsync(ConfigKey key, ConfigValue? value, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await ValueTask.CompletedTask;

        var item = new ConfigItem(key, value);

        if(_cache.TryAdd(key, item))
        {
            return true;
        }

        _cache[key] = item;

        return false;
    }

    public ValueTask SetManyAsync(IEnumerable<ConfigItem> configs, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        foreach(var item in configs)
        {
            _cache.TryAdd(item.Key, item);
        }

        return ValueTask.CompletedTask;
    }
}
