using AoCUtil.Coordinates;

namespace Tests.Coordinates;

public class CoordinateTests
{
    [Fact]
    public void Constructor_ShouldReturnCoordinate()
    {
        var c = new Coordinate(0, 0);

        Assert.NotNull(c);
    }
    
    [Theory]
    [MemberData(nameof(Data))]
    public void Constructor_ShouldReturnCoordinateWithInitialValues(int x, int y)
    {
        var c = new Coordinate(x, y);

        Assert.Equal(x, c.X);
        Assert.Equal(y, c.Y);
    }
    
    [Theory]
    [MemberData(nameof(Data))]
    public void ConstructorWithTuple_ShouldReturnCoordinate(int x, int y)
    {
        var c = new Coordinate((x,y));

        Assert.NotNull(c);
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
    [MemberData(nameof(Data))]
    public void ToString_ShouldReturnFormattedCoordinateValuesAsString(int x, int y)
    {
        var c = new Coordinate(x, y);

        Assert.Equal($"({c.X},{c.Y})", c.ToString());
    }
    
    [Theory]
    [MemberData(nameof(NeighbourData))]
    public void Neighbours_ShouldReturnAllNeighbours(int x, int y, List<Coordinate> expected)
    {
        var neighbours = new Coordinate(x, y).Neighbours().ToArray();
        
        Assert.Equal(8, neighbours.Length);
        foreach (var c in expected)
        {
            Assert.Contains(c, neighbours);
        }
    }
    
    [Theory]
    [MemberData(nameof(OrthogonalNeighbourData))]
    public void NeighboursWithoutDiagonal_ShouldReturnHorizontalAndVerticalNeighbours(int x, int y, List<Coordinate> expected)
    {
        var neighbours = new Coordinate(x, y).Neighbours(NeighbourOptions.Orthogonal).ToArray();

        Assert.Equal(4, neighbours.Length);
        foreach (var c in expected)
        {
            Assert.Contains(c, neighbours);
        }
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
    
    [Theory]
    [MemberData(nameof(DiagonalNeighbourData))]
    public void NeighboursDiagonal_ShouldReturnDiagonalNeighbours(int x, int y, List<Coordinate> expected)
    {
        var neighbours = new Coordinate(x, y).Neighbours(NeighbourOptions.Diagonal).ToArray();

        Assert.Equal(4, neighbours.Length);
        foreach (var c in expected)
        {
            Assert.Contains(c, neighbours);
        }
    }
    
    [Theory]
    [MemberData(nameof(Data))]
    public void Deconstruct_ShouldReturnIndividualIntValues(int x, int y)
    {
        new Coordinate(x, y).Deconstruct(out var cx, out var cy);
        
        Assert.Equal(x, cx);
        Assert.Equal(y, cy);
    }
    
    [Theory]
    [MemberData(nameof(HorizontalRangeData))]
    public void HorizontalRange_ShouldReturnHorizontalLine(int x, int y, int stepsize, int n, List<Coordinate> expected)
    {
        var range = Coordinate.HorizontalRange(new Coordinate(x, y), stepsize, n).ToArray();
        
        Assert.Equal(n, range.Length);
        foreach (var c in expected)
        {
            Assert.Contains(c, range);
        }
    }
    
    public static IEnumerable<object[]> Data =>
        new List<object[]>
        {
            new object[] {  1,  1 },
            new object[] { -1,  1 },
            new object[] {  1, -1 },
            new object[] { -1, -1 },
        };
    
    public static IEnumerable<object[]> HorizontalRangeData =>
        new List<object[]>
        {
            new object[] { 0, 0, 1, 3, new List<Coordinate> { (1,0), (2,0), (3,0) } },
            new object[] { 0, 0, 2, 3, new List<Coordinate> { (2,0), (4,0), (6,0) } },
            new object[] { -1, -4, 1, 5, new List<Coordinate> { (0, -4), (1, -4), (2, -4), (3, -4), (4, -4) } },
        };
    
    public static IEnumerable<object[]> NeighbourData =>
        new List<object[]>
        {
            new object[] { 0, 0, DefaultNeighbours }
        };
    
    public static IEnumerable<object[]> DiagonalNeighbourData =>
        new List<object[]>
        {
            new object[] { 0, 0, new List<Coordinate> { (1, -1), (-1, -1), (-1, 1), (1, 1) } }
        };
    
    public static IEnumerable<object[]> OrthogonalNeighbourData =>
        new List<object[]>
        {
            new object[] { 0, 0, new List<Coordinate> { (1, 0), (0, -1), (-1, 0), (0, 1) } }
        };

    private static readonly List<Coordinate> DefaultNeighbours =
        [(1, 0), (1, -1), (0, -1), (-1, -1), (-1, 0), (-1, 1), (0, 1), (1, 1)];
}