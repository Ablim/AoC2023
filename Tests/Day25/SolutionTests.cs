using Solutions.Day25;

namespace Tests.Day25;

public class SolutionTests
{
    private const int Day = 25;
    private readonly string[] _input = File.ReadAllLines($"Day{Day}/Input.txt");
    
    [Theory]
    [InlineData("54")]
    public void Part1(string expected) => Assert.Equal(expected, Solution.SolvePart1(_input));
    
    // [Theory]
    // [InlineData("16733044")]
    // public void Part2(string expected) => Assert.Equal(expected, Solution.SolvePart2(_input));
}