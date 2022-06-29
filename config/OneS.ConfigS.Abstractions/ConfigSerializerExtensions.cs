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

using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

public static class ConfigSerializerExtensions
{
    public static T? Deserialize<T>(this IConfigSerializer serializer, string data, JsonSerializerOptions? options = null)
        => serializer.Deserialize<T>(Encoding.UTF8.GetBytes(data), options);

    public static async ValueTask<T?> DeserializeAsync<T>(this IConfigSerializer serializer, string data, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
    {
        await using var memory = new MemoryStream(data.Length);

        await memory.WriteAsync(Encoding.UTF8.GetBytes(data), cancellationToken);

        // q: https://github.com/dotnet/runtime/issues/30945
        memory.Position = 0;

        return await serializer.DeserializeAsync<T>(memory, options, cancellationToken);
    }

    public static string Serialize<T>(this IConfigSerializer serializer, T data, JsonSerializerOptions? options = null)
        => Encoding.UTF8.GetString(serializer.Serialize(data, options));

    public static async ValueTask<string> SerializeAsync<T>(
        this IConfigSerializer serializer,
        T data,
        JsonSerializerOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        var result = await serializer.SerializeAsync(data, options, cancellationToken);

        return Encoding.UTF8.GetString(result.Span);
    }
}
