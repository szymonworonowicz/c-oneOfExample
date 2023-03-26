using FluentAssertions;
using Newtonsoft.Json;
using OneOfTest.BlockData;
using OneOfJsonConverter = OneOfTest.Json.OneOfJsonConverter;

namespace OneOfTest.Tests;

public class OneOfTypeJsonConverterTests
{
    [Fact]
    public void Should_convert_one_of_union_to_json()
    {
        var obj = new Start
        {
            A = "123",
            B = 2,
            C = 234
        };

        var objectJson = JsonConvert.SerializeObject(obj);
        var union = new BlockDataUnion(obj);

        var unionJson = JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
        {
            Converters = new List<JsonConverter>()
            {
                new OneOfJsonConverter()
            }
        });

        unionJson.Should().BeEquivalentTo(objectJson);
    }

    [Fact]
    public void Should_convert_from_raw_json_to_union()
    {
        var obj = new Start
        {
            A = "123",
            B = 2,
            C = 234
        };

        var objectJson = JsonConvert.SerializeObject(obj);

        BlockDataUnion? union = JsonConvert.DeserializeObject<BlockDataUnion>(objectJson, new JsonSerializerSettings()
        {
            Converters = new List<JsonConverter>()
            {
                new OneOfJsonConverter()
            }
        });

        var dataFromUnion = union.GetValue<Start>();

        union.Should().NotBeNull();
        union.Should().BeOfType<BlockDataUnion>();
        dataFromUnion.Should().BeEquivalentTo(obj);

    }

}