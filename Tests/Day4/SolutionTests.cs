using Solutions.Day4;

namespace Tests.Day4;

public class SolutionTests
{
    private const int Day = 4;
    private readonly string[] _input = File.ReadAllLines($"Day{Day}/Input.txt");
    
    [Theory]
    [InlineData("13")]
    public void Part1(string expected) => Assert.Equal(expected, Solution.SolvePart1(_input));
    
    [Theory]
    [InlineData("30")]
    public void Part2(string expected) => Assert.Equal(expected, Solution.SolvePart2(_input));
}