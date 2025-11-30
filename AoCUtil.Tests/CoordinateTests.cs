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
}