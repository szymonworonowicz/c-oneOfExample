using FluentAssertions;
using OneOfTest.BlockData;

namespace OneOfTest.Tests;

public class OneOfTests
{
    [Fact]
    public void Should_return_object_when_matched_type()
    {
        var obj = new Start
        {
            A = "123",
            B = 2,
            C = 234
        };

        var data = new BlockDataUnion(obj);
        var fromUnion = data.GetValue<Start>();

        fromUnion.Should().NotBeNull();
        fromUnion.Should().BeOfType<Start>();
    }

    [Fact]
    public void Should_throw_Exception_when_given_type_in_not_match()
    {
        var obj = new Start
        {
            A = "123",
            B = 2,
            C = 234
        };

        var data = new BlockDataUnion(obj);
        var fromUnionAct = () => data.GetValue<UserTask>();

        fromUnionAct.Should().Throw<Exception>();
    }
}
