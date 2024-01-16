using Solutions.Day11;

namespace Tests.Day11;

public class SolutionTests
{
    private const int Day = 11;
    private readonly string[] _input = File.ReadAllLines($"Day{Day}/Input.txt");
    
    [Theory]
    [InlineData("374")]
    public void Part1(string expected) => Assert.Equal(expected, Solution.SolvePart1(_input));
    
    [Theory]
    [InlineData("8410")]
    public void Part2(string expected) => Assert.Equal(expected, Solution.SolvePart2(_input, 100));
}