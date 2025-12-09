namespace AoCUtil.Coordinates;

public class Coordinate3D
{
    private readonly (int x, int y, int z) _pos;

    public Coordinate3D(int x, int y, int z)
    {
        _pos = (x, y, z);
    }

    public Coordinate3D(Coordinate3D pos)
    {
        _pos = (pos.X, pos.Y, pos.Z);
    }

    public int X => _pos.x;

    public int Y => _pos.y;
    
    public int Z => _pos.z;

    public bool Equals(Coordinate3D? other)
    {
        return other != null && X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj) => Equals(obj as Coordinate3D);

    public override int GetHashCode() => _pos.GetHashCode();

    public override string ToString() => $"({X},{Y},{Z})";
    
    public static implicit operator Coordinate3D((int x, int y, int z) tuple)
    {
        return new Coordinate3D(tuple.x, tuple.y, tuple.z);
    }

    public void Deconstruct(out int x, out int y, out int z)
    {
        x = X;
        y = Y;
        z = Z;
    }
    
    public static Coordinate3D FromString(string s)
    {
        var parts = s.Replace("(", "").Replace(")", "").Split(',');

        return new Coordinate3D(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]));
    }

    public double EuclideanDistance(Coordinate3D point)
    {
        return Math.Sqrt(Math.Pow(X - point.X, 2) + Math.Pow(Y - point.Y, 2) + Math.Pow(Z - point.Z, 2));
    }
}