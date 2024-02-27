using System.Drawing;

namespace Robotc.Test;

public class TableTopTests
{
    private readonly Mock<IRobotFactory> _factory = new (MockBehavior.Strict);
    private readonly Mock<IRobot> _robot = new (MockBehavior.Strict);
    private readonly Mock<IRobot> _nullRobot = new (MockBehavior.Strict);
    private readonly Point _validPosition = new (1, 1);

    public TableTopTests()
    {
        _factory.Setup(mock => mock.CreateRobot(It.IsAny<Point>(), It.IsAny<Direction>()))
            .Returns(_robot.Object);
        _factory.Setup(mock => mock.CreateNullRobot())
            .Returns(_nullRobot.Object);
    }

    [Fact]
    public void HasDefaultSize()
    {
        ITableTop sut = new TableTop(_factory.Object);

        Assert.Equal(new Size(5, 5), sut.Size);
        Assert.Equal(new Rectangle(0, 0, 5, 5), sut.Bounds);

    }

    [Theory]
    [InlineData(0, 0, true)] 
    [InlineData(-1, 0, false)] 
    [InlineData(0, -1, false)] 
    [InlineData(1, 1, true)] 
    [InlineData(2, 2, true)]
    [InlineData(5, 5, false)]
    [InlineData(0, 5, false)]
    [InlineData(5, 0, false)]
   public void ReturnsWhetherNewPositionIsValid(int x, int y, bool expected)
    {
        ITableTop sut = new TableTop(_factory.Object);

        Assert.Equal(expected, sut.IsValidPosition(new Point(x, y)));
    }

    [Fact]
    public void PlaceRobotCallsFactoryWithCorrectPosition()
    {

        ITableTop sut = new TableTop(_factory.Object);

        Assert.True(sut.PlaceRobot(_validPosition, Direction.North));

        _factory.Verify(mock => mock.CreateRobot(_validPosition, Direction.North), Times.Once);
    }

    [Fact]
    public void PlaceRobotFailsWhenInvalidPosition()
    {
        var invalidPosition = new Point(5, 5);

        ITableTop sut = new TableTop(_factory.Object);

        Assert.False(sut.PlaceRobot(invalidPosition, Direction.North));

        _factory.Verify(mock => mock.CreateRobot(invalidPosition, Direction.North), Times.Never);
    }

    [Fact]
    public void HasRobotReturnsTrueWhenRobotPlacedFalseOtherwise()
    {
        ITableTop sut = new TableTop(_factory.Object);

        Assert.False(sut.HasRobot);

        sut.PlaceRobot(_validPosition, Direction.North);

        Assert.True(sut.HasRobot);
    }

    [Fact]
    public void ConstructorCallsCreateNullRobot()
    {
        ITableTop sut = new TableTop(_factory.Object);

        Assert.Equal(_nullRobot.Object, sut.Robot);

        _factory.Verify(mock => mock.CreateNullRobot(), Times.Once);
    }

    [Fact]
    public void CanCreateAnObstacle()
    {
        ITableTop sut = new TableTop(_factory.Object);

        var position = new Point(2, 2);

        Assert.True(sut.IsValidPosition(position));

        _nullRobot.SetupGet(mock => mock.Position)
            .Returns(new Point(0, 0));

        Assert.True(sut.PlaceObstacle(position));

        Assert.False(sut.IsValidPosition(position));
    }

    [Theory]
    [InlineData(-1, -1)]
    [InlineData(-1, 2)]
    [InlineData(2, -1)]
    [InlineData(5, 5)]
    [InlineData(2, 5)]
    [InlineData(5, 2)]
    public void PlacingObstacleAtInvalidLocationFails(int x, int y)
    {
        ITableTop sut = new TableTop(_factory.Object);

        var position = new Point(x, y);

        Assert.False(sut.PlaceObstacle(position));
    }

    [Fact]
    public void PlactingObstacleOnSamePositionAsRobotFails()
    {
        ITableTop sut = new TableTop(_factory.Object);

        var position = new Point(1, 1);

        _robot.SetupGet(mock => mock.Position)
            .Returns(position);
        
        sut.PlaceRobot(position, Direction.North);

        Assert.False(sut.PlaceObstacle(position));

        _robot.VerifyGet(mock => mock.Position, Times.Once);
    }
  
}