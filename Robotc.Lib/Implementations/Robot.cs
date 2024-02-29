using System.Drawing;

namespace Robotc.Lib;

public class Robot : IRobot
{
    private static Size[] neighbourOffsets = 
    [
        new (-1, 0),  // WEST
        new (0, 1),   // NORTH
        new (1, 0),   // EAST
        new (0, -1)   // SOUTH
    ];

    public Robot(Point position, Direction heading) 
    {
        Position = position;
        Heading = heading;
    }

    private bool TryGetHeadingFromNeighbour(Point neighbour, out Direction heading)
    {
        heading = Heading;
        for (var index = 0; index < neighbourOffsets.Length; index++)
        {
            if (Position + neighbourOffsets[index] == neighbour)
            {
                heading = (Direction) index;
                return true;
            }
        }
        return false;
    }

    private Point NeighbourInDirection(Direction direction)
    {
        return Position + neighbourOffsets[(int)direction];
    }

    public Point Position { get; set; }
    public Direction Heading { get; set; }

    public void Rotate(Turn turn)
    {
        Heading = Heading.Rotate(turn);
    }

    public bool RotateTowards(Point neighbour, out Turn turn, out int count)
    {
        count = 0;
        turn = Turn.Right;
        if (!TryGetHeadingFromNeighbour(neighbour, out var newHeading))
            return false;

        count = newHeading - Heading;
        
        if (count < 0)
        {
            turn = Turn.Left;
            count = Math.Abs(count);
        }
        
        if (count > 2)
        {
            turn = turn.Opposite();
            count = 1;
        }

        Heading = newHeading;

        return CalcMove() == neighbour;
    }

    public IRobot Clone()
    {
        return new Robot(Position, Heading);
    }

    public Point CalcMove(int distance = 1)
    {
        return Heading switch //switch statement compiles to jump table, faster than array lookup
        {
            Direction.West => Position + new Size(-distance, 0),
            Direction.North => Position + new Size(0, distance),
            Direction.East => Position + new Size(distance, 0),
            Direction.South => Position + new Size(0, -distance),
            _ => Position
        };
    }
}