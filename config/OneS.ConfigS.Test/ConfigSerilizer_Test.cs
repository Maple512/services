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

namespace OneS.ConfigS.Test;

using System;
using System.Text;
using OneF.Json;
using Xunit;
using Shouldly;
using System.Threading.Tasks;

public class ConfigSerilizer_Test : TestBase
{
    private static readonly Model1 _data = new(1, "Maple512");

    private readonly IConfigSerializer _serializer;

    public ConfigSerilizer_Test()
    {
        _serializer = GetRequiredService<IConfigSerializer>();
    }

    [Fact]
    public void Conventional_test1()
    {
        var bytes = _serializer.Serialize(_data, JsonSerializerHelper.StandardOptions);

        bytes.IsEmpty.ShouldBeFalse();

        var json = Encoding.UTF8.GetString(bytes);

        json.ShouldContain("Maple512");
    }

    [Fact]
    public async Task Conventional_test2()
    {
        var memory = await _serializer.SerializeAsync(_data, JsonSerializerHelper.StandardOptions);

        memory.IsEmpty.ShouldBeFalse();

        var json = Encoding.UTF8.GetString(memory.Span);

        json.ShouldContain("Maple512");
    }

    [Fact]
    public void Conventional_test3()
    {
        var span = _serializer.Serialize(_data, JsonSerializerHelper.StandardOptions);

        span.IsEmpty.ShouldBeFalse();

        var json = Encoding.UTF8.GetString(span);

        json.ShouldContain("Maple512");

        var data = _serializer.Deserialize<Model1>(json);

        data.ShouldBeEquivalentTo(_data);
    }

    [Fact]
    public async Task Conventional_test4()
    {
        var memory = await _serializer.SerializeAsync(_data, JsonSerializerHelper.StandardOptions);

        memory.IsEmpty.ShouldBeFalse();

        var json = Encoding.UTF8.GetString(memory.Span);

        json.ShouldContain("Maple512");

        var data = await _serializer.DeserializeAsync<Model1>(json);

        data.ShouldBeEquivalentTo(_data);
    }

    [Serializable]
    private class Model1
    {
        public Model1(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public long Id { get; }
        public string Name { get; }
    }
}
