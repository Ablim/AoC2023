namespace Solutions.Day11;

public struct Point
{
    public Point(int row, int column)
    {
        Row = row;
        Column = column;
    }
    
    public int Row { get; }
    public int Column { get; }
}