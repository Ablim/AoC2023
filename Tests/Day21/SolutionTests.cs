using Solutions.Day21;

namespace Tests.Day21;

public class SolutionTests
{
    private const int Day = 21;
    private readonly string[] _input = File.ReadAllLines($"Day{Day}/Input.txt");
    
    [Theory]
    [InlineData("16")]
    public void Part1(string expected) => Assert.Equal(expected, Solution.SolvePart1(_input, 6));
    
    // [Theory]
    // [InlineData("16733044")]
    // public void Part2(string expected) => Assert.Equal(expected, Solution.SolvePart2(_input, 5000));
}