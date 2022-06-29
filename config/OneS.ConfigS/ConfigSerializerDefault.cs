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
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using OneF.Moduleable.DependencyInjection;

public class ConfigSerializerDefault : IConfigSerializer, ISingletonService
{
    public virtual T? Deserialize<T>(ReadOnlySpan<byte> data, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Deserialize<T>(data, options);
    }

    public virtual ValueTask<T?> DeserializeAsync<T>(Stream data, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
    {
        return JsonSerializer.DeserializeAsync<T>(data, options, cancellationToken);
    }

    public virtual ReadOnlySpan<byte> Serialize<T>(T data, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.SerializeToUtf8Bytes(data, options);
    }

    public virtual async ValueTask<ReadOnlyMemory<byte>> SerializeAsync<T>(T data, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
    {
        await using var memory = new MemoryStream();

        await JsonSerializer.SerializeAsync(memory, data, options, cancellationToken);

        return memory.ToArray();
    }
}
