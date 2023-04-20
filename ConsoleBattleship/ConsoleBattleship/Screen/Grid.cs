namespace ConsoleBattleship
{
  public class Grid
  {
    private string[][] _gridArray;

    public readonly int Width;
    public readonly int Height;

    public delegate void ChangeDelegate(int row, int column, string previousValue, string newValue);
    public event ChangeDelegate OnChange = delegate { };

    public Grid(int width, int height, string initial) 
    { 
      Width = width;
      Height = height;
      _gridArray = new string[Height][];

      string[] row = new string[Width];

      for (int i = 0; i < Width; i++)
      {
        row[i] = initial; 
      }

      for (int i = 0;i < Height; i++)
      {
        _gridArray[i] = new string[Width];
        row.CopyTo(_gridArray[i], 0);
      }
    }

    /// <returns>The previous character in that position</returns>
    public string ReplaceChar(int row, int column, string c)
    {
      string old;
      lock (_gridArray)
      {
        // May want to note this later on
        if (!(0 < row && row < Height && 0 < column && column < Width)) {
          row = Math.Abs(row % Height);
          column = Math.Abs(column % Width);
        }
        old = _gridArray[row][column];
      }
        _gridArray[row][column] = c;
        OnChange(row, column, old, c);
        return old;
    }

    public string GetChar(int row, int column)
    {
      return _gridArray[row][column];
    }
    
    public string GetRow(int row) 
    { 
      return string.Join("", _gridArray[row]);
    }

    /// <summary>
    /// Mutable Enumerable of each row from top to bottom
    /// </summary>
    public IEnumerable<String[]> GetAllRows()
    {
      for (int i = 0; i < Height; i++)
      {
        yield return _gridArray[i];
      }
    }

    /// <summary>
    /// Mutable Enumerable of each column from left to right
    /// </summary>
    public IEnumerable<String[]> GetAllColumns()
    {
      for (int i = 0; i < Width; i++)
      {
        string[] column = new string[Height];
        for (int j = 0; j < Height; j++)
        {
          column[j] = _gridArray[j][i];
        }
        yield return column;
      }
    }
  }
}
