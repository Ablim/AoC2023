using Solutions.Day7;

namespace Tests.Day7;

public class SolutionTests
{
    private const int Day = 7;
    private readonly string[] _input = File.ReadAllLines($"Day{Day}/Input.txt");
    
    [Theory]
    [InlineData("6440")]
    public void Part1(string expected) => Assert.Equal(expected, Solution.SolvePart1(_input));
    
    [Theory]
    [InlineData("5905")]
    public void Part2(string expected) => Assert.Equal(expected, Solution.SolvePart2(_input));
}