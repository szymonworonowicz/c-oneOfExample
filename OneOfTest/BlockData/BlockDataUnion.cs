using OneOf;

namespace OneOfTest.BlockData;

public class BlockDataUnion :OneOfBase<Start, UserTask>, IEquatable<BlockDataUnion>
{
    public BlockDataUnion(Start _) : base(_)
    {
    }

    public BlockDataUnion(UserTask _) : base(_)
    {
    }

    protected BlockDataUnion(OneOf<Start, UserTask> input) : base(input)
    {
    }

    public static implicit operator BlockDataUnion(Start _) => new(_);
    public static implicit operator BlockDataUnion(UserTask _) => new(_);

    public T GetValue<T>() where T : BaseData, new()
    {
        var value = Match<T>(
            start => start as T,
            userTask => userTask as T
        );

        return value is T ? value : throw new Exception();
    }

    public bool Equals(BlockDataUnion? other)
    {
        return Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((BlockDataUnion)obj);
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}