namespace AoCUtil.Numeric;

public class Range(long start, long end) : IEquatable<Range>
{
    public long Start { get; set; } = start;
    public long End { get; set; } = end;

    private bool Overlap(Range range)
    {
        return (range.Start <= End && range.End >= Start)
               || (Start <= range.End && End >= range.Start);
    }
    
    public bool Merge(Range range)
    {
        var overlap = Overlap(range);

        if (!overlap) return false;
        Start =  Math.Min(Start, range.Start);
        End =   Math.Max(End, range.End);

        return true;
    }

    public bool Equals(Range? other)
    {
        return other != null && Start == other.Start && End == other.End;
    }
    
    public override bool Equals(object? obj) => Equals(obj as Range);

    public override int GetHashCode()
    {
        return HashCode.Combine(Start, End);
    }

    public bool InRange(long x)
    {
        return x >= Start && x <= End;
    }
    
    public bool InRange(int x)
    {
        return x >= Start && x <= End;
    }
}
