using AoCUtil;

namespace Tests;

public class CoordinateTests
{
    [Fact]
    public void Create_ShouldReturnCoordinate()
    {
        var c = new Coordinate(0, 0);

        Assert.NotNull(c);
    }
    
    [Theory]
    [InlineData(1, 1)]
    [InlineData(-1, 1)]
    [InlineData(1, -1)]
    [InlineData(-1, -1)]
    public void Create_ShouldReturnCoordinateWithInitialValues(int x, int y)
    {
        var c = new Coordinate(x, y);

        Assert.Equal(x, c.X);
        Assert.Equal(y, c.Y);
    }

    [Fact]
    public void Equals_ShouldReturnTrueWhenEqual()
    {
        var c1 = new Coordinate(0, 0);
        var c2 = new Coordinate(0, 0);
        Assert.True(c1.Equals(c2));
        Assert.True(c2.Equals(c1));
    }
    
    [Fact]
    public void Equals_ShouldReturnFalseWhenNotEqual()
    {
        var c1 = new Coordinate(1, 1);
        var c2 = new Coordinate(0, 0);
        Assert.False(c1.Equals(c2));
        Assert.False(c2.Equals(c1));
    }
    
    [Theory]
    [InlineData(1, 1)]
    [InlineData(-1, 1)]
    [InlineData(1, -1)]
    [InlineData(-1, -1)]
    public void ToString_ShouldReturnFormattedCoordinateValuesAsString(int x, int y)
    {
        var c = new Coordinate(x, y);

        Assert.Equal($"({c.X},{c.Y})", c.ToString());
    }
    
    [Fact]
    public void Neighbours_ShouldReturnAllNeighbours()
    {
        var neighbours = new Coordinate(0, 0).Neighbours().ToArray();

        Assert.Equal(8, neighbours.Length);
        Assert.Contains(new Coordinate( 1,  0), neighbours);
        Assert.Contains(new Coordinate( 1, -1), neighbours);
        Assert.Contains(new Coordinate( 0, -1), neighbours);
        Assert.Contains(new Coordinate(-1, -1), neighbours);
        Assert.Contains(new Coordinate(-1,  0), neighbours);
        Assert.Contains(new Coordinate(-1,  1), neighbours);
        Assert.Contains(new Coordinate( 0,  1), neighbours);
        Assert.Contains(new Coordinate( 1,  1), neighbours);
    }
    
    [Fact]
    public void NeighboursWithoutDiagonal_ShouldReturnHorizontalAndVerticalNeighbours()
    {
        var neighbours = new Coordinate(0, 0).Neighbours(diagonal:false).ToArray();

        Assert.Equal(4, neighbours.Length);
        Assert.Contains(new Coordinate( 1,  0), neighbours);
        Assert.Contains(new Coordinate( 0, -1), neighbours);
        Assert.Contains(new Coordinate(-1,  0), neighbours);
        Assert.Contains(new Coordinate( 0,  1), neighbours);
    }

    [Theory]
    [InlineData(1, 1, 0, 0, true)]
    [InlineData(0, 0, 0, 1, true)]
    [InlineData(0, 0, 1, 0, true)]
    [InlineData(0, 0, 0, 0, false)]
    [InlineData(0, 0, 2, 2, false)]
    public void IsAdjacent_ShouldReturnTrueWhenAdjacent(int x1, int y1, int x2, int y2, bool expected)
    {
        Assert.Equal(expected, new Coordinate(x1, y1).IsAdjacentTo(new Coordinate(x2, y2)));
    }
    
}