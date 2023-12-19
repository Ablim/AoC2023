using Solutions.Day12;

namespace Tests.Day12;

public class SolutionTests
{
    private const int Day = 12;
    private readonly string[] _input = File.ReadAllLines($"Day{Day}/Input.txt");
    
    [Theory]
    [InlineData("21")]
    public void Part1(string expected) => Assert.Equal(expected, Solution.SolvePart1(_input));
    
    [Theory]
    [InlineData("2")]
    public void Part2(string expected) => Assert.Equal(expected, Solution.SolvePart2(_input));
}