using Solutions.Day13;

namespace Tests.Day13;

public class SolutionTests
{
    private const int Day = 13;
    private readonly string[] _input = File.ReadAllLines($"Day{Day}/Input.txt");
    
    [Theory]
    [InlineData("405")]
    public void Part1(string expected) => Assert.Equal(expected, Solution.SolvePart1(_input));
    
    [Theory]
    [InlineData("405")]
    public void Part2(string expected) => Assert.Equal(expected, Solution.SolvePart2(_input));
}