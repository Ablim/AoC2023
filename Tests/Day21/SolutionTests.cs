using Solutions.Day21;

namespace Tests.Day21;

public class SolutionTests
{
    private const int Day = 21;
    private readonly string[] _input = File.ReadAllLines($"Day{Day}/Input.txt");
    
    [Theory]
    [InlineData("16")]
    public void Part1(string expected) => Assert.Equal(expected, Solution.TestSolvePart1(_input));
    
    [Theory]
    [InlineData("525152")]
    public void Part2(string expected) => Assert.Equal(expected, Solution.SolvePart2(_input));
}