using Solutions.Day24;

namespace Tests.Day24;

public class SolutionTests
{
    private const int Day = 24;
    private readonly string[] _input = File.ReadAllLines($"Day{Day}/Input.txt");
    
    [Theory]
    [InlineData("2")]
    public void Part1(string expected) => Assert.Equal(expected, Solution.SolvePart1(_input, 7, 27));
    
    // [Theory]
    // [InlineData("16733044")]
    // public void Part2(string expected) => Assert.Equal(expected, Solution.SolvePart2(_input));
}