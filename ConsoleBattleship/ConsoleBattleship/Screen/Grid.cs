﻿namespace ConsoleBattleship
{
  internal class Grid
  {
    private string[][] _gridArray;

    private readonly int _width;
    private readonly int _height;

    public delegate void ChangeDelegate(int row, int column, string previousValue, string newValue);
    public event ChangeDelegate OnChange = delegate { };

    public Grid(int width, int height, string initial) 
    { 
      _width = width;
      _height = height;
      _gridArray = new string[_height][];

      string[] row = new string[_width];

      for (int i = 0; i < _width; i++)
      {
        row[i] = initial; 
      }

      for (int i = 0;i < _height; i++)
      {
        _gridArray[i] = new string[_width];
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
        if (!(0 < row && row < _height && 0 < column && column < _width)) {
          row = Math.Abs(row % _height);
          column = Math.Abs(column % _width);
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
      for (int i = 0; i < _height; i++)
      {
        yield return _gridArray[i];
      }
    }

    /// <summary>
    /// Mutable Enumerable of each column from left to right
    /// </summary>
    public IEnumerable<String[]> GetAllColumns()
    {
      for (int i = 0; i < _width; i++)
      {
        string[] column = new string[_height];
        for (int j = 0; j < _height; j++)
        {
          column[j] = _gridArray[j][i];
        }
        yield return column;
      }
    }
  }
}