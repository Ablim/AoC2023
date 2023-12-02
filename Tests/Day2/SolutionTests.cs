using Solutions.Day2;

namespace Tests.Day2;

public class SolutionTests
{
    private const int Day = 2;
    private readonly string[] _input = File.ReadAllLines($"Day{Day}/Input.txt");
    
    [Theory]
    [InlineData("8")]
    public void Part1(string expected) => Assert.Equal(expected, Solution.SolvePart1(_input));
    
    [Theory]
    [InlineData("2286")]
    public void Part2(string expected) => Assert.Equal(expected, Solution.SolvePart2(_input));
}