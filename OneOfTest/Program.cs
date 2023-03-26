using OneOfTest.BlockData;

var x = new Start
{
    A = "123",
    B = 2,
    C = 234
};

BlockData blockData = x;
BlockDataGenerator blockDataGenerator = new(x);

var test2 = blockDataGenerator.GetValue<Start>();

var test = blockData.GetValue<UserTask>();

Console.WriteLine(test);