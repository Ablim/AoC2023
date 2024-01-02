namespace Solutions.Day25;

public record Edge
{
    public Edge(string nodeA, string nodeB)
    {
        NodeA = nodeA;
        NodeB = nodeB;
    }
    
    public string NodeA { get; }
    public string NodeB { get; }
}