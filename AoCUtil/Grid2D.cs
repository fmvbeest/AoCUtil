using System.Text;

namespace AoCUtil;

public class Grid2D
{
    public int X => _grid.GetLength(0);

    public int Y => _grid.GetLength(1);

    private readonly int[,] _grid;

    private readonly Dictionary<char, int> _charToIntMap;
    private readonly Dictionary<int, char> _intToCharMap;

    public Grid2D(IEnumerable<string> input, Dictionary<char, int> charToIntMap)
    {
        var array = input.ToArray();
        _grid = new int[array.Length, array[0].Length];
        _charToIntMap = charToIntMap;
        _intToCharMap = new Dictionary<int, char>();
        foreach (var (key, value) in _charToIntMap)
        {
            _intToCharMap.Add(value, key);
        }
        
        Init(array);
    }
    
    public Grid2D(int width, int height, Dictionary<char, int> charToIntMap, char initialChar)
    {
        _grid = new int[width, height];
        _charToIntMap = charToIntMap;
        _intToCharMap = new Dictionary<int, char>();
        foreach (var (key, value) in _charToIntMap)
        {
            _intToCharMap.Add(value, key);
        }
        
        for (var i = 0; i < X; i++)
        {
            for (var j = 0; j < Y; j++)
            {
                _grid[i, j] = _charToIntMap[initialChar];
            }
        }
    }

    public Grid2D(IEnumerable<string> input, char[] chars)
    {
        var array = input.ToArray();
        _grid = new int[array.Length, array[0].Length];
        _charToIntMap = new Dictionary<char, int>();
        _intToCharMap = new Dictionary<int, char>();
        for (var i = 0; i < chars.Length; i++)
        {
            _charToIntMap.Add(chars[i], i);
            _intToCharMap.Add(i, chars[i]);
        }
        
        Init(array);
    }

    public Grid2D(IEnumerable<string> input)
    {
        _charToIntMap = new Dictionary<char, int>();
        _intToCharMap = new Dictionary<int, char>();
        var array = input.ToArray();
        var n = array.Length;
        var m = array[0].Length;
        _grid = new int[n, m];
        
        var row = 0;
        foreach (var s in array)
        {
            var index = 0;
            foreach (var c in s)
            {
                var intValue = c - '0';
                _grid[row, index] = intValue;
                index++;
                _charToIntMap.TryAdd(c, intValue);
                _intToCharMap.TryAdd(intValue, c);
            }

            row++;
        }
    }

    private void Init(string[] input)
    {
        var array = input.ToArray();

        var row = 0;
        foreach (var s in array)
        {
            var index = 0;
            foreach (var c in s)
            {
                _grid[row, index] = _charToIntMap[c];
                index++;
            }

            row++;
        }
    }

    public int GetValue(int x, int y)
    {
        return _grid[x, y];
    }

    public int GetValue(Coordinate c)
    {
        return GetValue(c.X, c.Y);
    }

    public char GetCharValue(Coordinate c)
    {
        return _intToCharMap[GetValue(c)];
    }

    public IEnumerable<Coordinate> GetMappedValues(char c, bool singleValue = false)
    {
        var coordinates = new List<Coordinate>();
        
        for (var i = 0; i < X; i++)
        {
            for (var j = 0; j < Y; j++)
            {
                if (_grid[i, j] == _charToIntMap[c])
                {
                    if (singleValue) return new [] { new Coordinate(i, j) };
                    
                    coordinates.Add((i,j));
                }
            }
        }

        return coordinates;
    }

    public Coordinate GetSingleMappedValue(char c)
    {
        return GetMappedValues(c, singleValue: true).First();
    }

    public bool OnGrid(Coordinate coordinate)
    {
        return coordinate.X >= 0 && coordinate.X < X && coordinate.Y >= 0 && coordinate.Y < Y;
    }

    public IEnumerable<Coordinate> GetNeighboursWithValue(Coordinate coordinate, char c, bool diagonal = true)
    {
        return coordinate.Neighbours(diagonal).Where(OnGrid)
            .Where(coordinate1 => GetValue(coordinate1) == _charToIntMap[c]);
    }
    
    public IEnumerable<Coordinate> GetNeighboursDiagonalWithValue(Coordinate coordinate, char c, bool diagonal = true)
    {
        return coordinate.NeighboursDiagonal().Where(OnGrid)
            .Where(coordinate1 => GetValue(coordinate1) == _charToIntMap[c]);
    }

    public void SetGridValue(Coordinate coordinate, char c)
    {
        _grid[coordinate.X, coordinate.Y] = _charToIntMap[c];
    }

    public string GetStringValueInDirection(Coordinate coordinate, Coordinate direction, int length)
    {
        var result = string.Empty;

        for (var i = 1; i <= length; i++)
        {
            var pos = coordinate + (direction.X * i, direction.Y * i);
            result += OnGrid(pos) ? _intToCharMap[GetValue(pos)] : string.Empty;    
        }
        
        return result;
    }

    public char[] GetDistinctCharValues()
    {
        return _charToIntMap.Keys.ToArray();
    }

    public void Reset(char resetChar)
    {
        if (!_charToIntMap.ContainsKey(resetChar))
        {
            resetChar = _charToIntMap.First().Key;
        }
        
        for (var i = 0; i < X; i++)
        {
            for (var j = 0; j < Y; j++)
            {
                _grid[i, j] = _charToIntMap[resetChar];
            }
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var i = 0; i < X; i++)
        {
            for (var j = 0; j < Y; j++)
            {
                sb.Append(_intToCharMap[_grid[i, j]]);
            }
            sb.Append(Environment.NewLine);
        }
        return sb.ToString();
    }
}