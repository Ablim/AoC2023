using Solutions.Day9;

namespace Tests.Day9;

public class SolutionTests
{
    private const int Day = 9;
    private readonly string[] _input = File.ReadAllLines($"Day{Day}/Input.txt");
    
    [Theory]
    [InlineData("114")]
    public void Part1(string expected) => Assert.Equal(expected, Solution.SolvePart1(_input));
    
    [Theory]
    [InlineData("6")]
    public void Part2(string expected) => Assert.Equal(expected, Solution.SolvePart2(_input));
}