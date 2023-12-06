using Solutions.Day6;

namespace Tests.Day6;

public class SolutionTests
{
    private const int Day = 6;
    private readonly string[] _input = File.ReadAllLines($"Day{Day}/Input.txt");
    
    [Theory]
    [InlineData("288")]
    public void Part1(string expected) => Assert.Equal(expected, Solution.SolvePart1(_input));
    
    [Theory]
    [InlineData("71503")]
    public void Part2(string expected) => Assert.Equal(expected, Solution.SolvePart2(_input));
}