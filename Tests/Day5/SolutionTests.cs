using Solutions.Day5;

namespace Tests.Day5;

public class SolutionTests
{
    private const int Day = 5;
    private readonly string[] _input = File.ReadAllLines($"Day{Day}/Input.txt");
    
    [Theory]
    [InlineData("35")]
    public void Part1(string expected) => Assert.Equal(expected, Solution.SolvePart1(_input));
    
    [Theory]
    [InlineData("46")]
    public void Part2(string expected) => Assert.Equal(expected, Solution.SolvePart2(_input));
}