using Solutions.Day8;

namespace Tests.Day8;

public class SolutionTests
{
    private const int Day = 8;
    private readonly string[] _input = File.ReadAllLines($"Day{Day}/Input.txt");
    private readonly string[] _input2 = File.ReadAllLines($"Day{Day}/Input2.txt");
    
    [Theory]
    [InlineData("2")]
    public void Part1(string expected) => Assert.Equal(expected, Solution.SolvePart1(_input));
    
    [Theory]
    [InlineData("6")]
    public void Part2(string expected) => Assert.Equal(expected, Solution.SolvePart2(_input2));
}