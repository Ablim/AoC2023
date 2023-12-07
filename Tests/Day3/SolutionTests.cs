using Solutions.Day3;

namespace Tests.Day3;

public class SolutionTests
{
    private const int Day = 3;
    private readonly string[] _input = File.ReadAllLines($"Day{Day}/Input.txt");
    
    [Theory]
    [InlineData("4361")]
    public void Part1(string expected) => Assert.Equal(expected, Solution.SolvePart1(_input));
    
    [Theory]
    [InlineData("2286")]
    public void Part2(string expected) => Assert.Equal(expected, Solution.SolvePart2(_input));
}