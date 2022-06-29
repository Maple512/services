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

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// 配置管理
/// </summary>
public interface IConfigManager
{
    /// <summary>
    /// 获取完全符合指定<paramref name="key"/>的第一个配置
    /// </summary>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<ConfigItem?> GetAsync(ConfigKey key, CancellationToken cancellationToken = default);

    /// <summary>
    /// 获取完全符合指定<paramref name="key"/>的所有配置
    /// </summary>
    /// <param name="key"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<IEnumerable<ConfigItem>> GetAllAsync(ConfigKey? key, CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新配置
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns><see langword="true"/>: 已添加，<see langword="false"/>: 已更新</returns>
    ValueTask<bool> SetAsync(ConfigKey key, ConfigValue? value, CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新配置
    /// </summary>
    /// <param name="configs"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask SetManyAsync(IEnumerable<ConfigItem> configs, CancellationToken cancellationToken = default);
}
