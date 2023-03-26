using OneOf;

namespace OneOfTest.BlockData;

public class BlockData : OneOfBase<Start, UserTask>
{
    protected BlockData(OneOf<Start, UserTask> input) : base(input)
    {
    }

    public static implicit operator BlockData(Start _) => new(_);
    public static implicit operator BlockData(UserTask _) => new(_);
    
    public T GetValue<T>() where T: BaseData, new()
    {
        var value =  Match<T>(
            start => start as T,
            userTask => userTask as T
        );

        return value is T ? value : throw new ArgumentException();
    }  
}

[GenerateOneOf]
public class BlockDataGenerator : OneOfBase<Start, UserTask>
{
    public BlockDataGenerator(OneOf<Start, UserTask> input) : base(input)
    {
    }
    public T GetValue<T>() where T : BaseData, new()
    {
        return Match<T>(
            start => start as T  ?? throw new ArgumentException(),
            userTask => userTask as T ?? throw new ArgumentException()
        );

    }
}
//
// public static class BlockDataGeneratorExtensions
// {
//     public static T GetValue<T>(this BlockDataGenerator data) where T : BaseData, new()
//     {
//         var value =  data.Match<T>(
//             start => start as T,
//             userTask => userTask as T
//         );
//
//         return value is T ? value : throw new ArgumentException();
//     }
// }