using System.Drawing;

namespace Robotc.Lib;

public class Robot : IRobot
{
    private static Size[] neighbourOffsets = 
    [
        new (-1, 0),
        new (0, 1),
        new (1, 0),
        new (0, -1)
    ];

    public Robot(Point position, Direction heading) 
    {
        Position = position;
        Heading = heading;
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
        if (Math.Abs(Position.X - neighbour.X) 
            + Math.Abs(Position.Y - neighbour.Y) != 1)
            return false;

        if (CalcMove() == neighbour)
            return true;

        var newHeading = Heading;
        newHeading--;
        if (NeighbourInDirection(newHeading) == neighbour)
            turn = Turn.Left;

        Rotate(turn);
        count++;
        if (CalcMove() != neighbour)
        {
            Rotate(turn);
            count++;
        }
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