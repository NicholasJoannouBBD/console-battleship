using Pastel;

namespace ConsoleBattleship.Screen
{

  internal class GameScreen : BaseScreen
  {
    public static readonly string BackgroundChar = "V"
      .Pastel(Colors.BackgroundChar)
      .PastelBg(Colors.Background);
    private static readonly int s_padding = 2;
    private static readonly string s_inputPrompt = "input: ";
    private static GameScreen s_instance = new(
      Console.WindowWidth - (2 * s_padding) - 2,
      Console.WindowHeight - (2 * s_padding) - 4
    );

    public readonly Grid BattleshipGrid;

    public delegate void InputDelegate(string input);
    public event InputDelegate OnInput = delegate { };

    private string _input = "";
    private (int, int) _crosshairLocation;

    //SINGLETON CONSTRUCTOR
    protected GameScreen(int width, int height) : base(width, height)
    {
      _crosshairLocation = (width / 2, height / 2);

      BattleshipGrid = new Grid(width - s_padding * 2, height, BackgroundChar);

      AddAllDelegates();
    }
    private void AddAllDelegates()
    {
      OnRefresh += RefreshGrid;
      OnRefresh += RefreshInput;
      OnRefresh += RefreshBorder;

      OnKeyPressed += HandleBackspace;
      OnKeyPressed += HandleEnter;
      OnKeyPressed += HandleAlphaNumeric;
      OnKeyPressed += HandleEscape;
      OnKeyPressed += HandleArrowKeys;
    }


    // EVENT HANDLERS
    // On Refresh
    private void RefreshBorder()
    {
      SetCursorToBegin();
      Console.Write(
          (new string(' ', s_padding) + "┌" + new string('─', _width - s_padding * 2) + "┐")
            .Pastel(Colors.Border)
            .PastelBg(Colors.Background)
      );
      for (int i = 0; i < _height; i++)
      {
        Console.SetCursorPosition(s_padding, Console.GetCursorPosition().Top + 1);
        Console.Write("│".Pastel(Colors.Border).PastelBg(Colors.Background));
        Console.SetCursorPosition(_width - s_padding + 1, Console.GetCursorPosition().Top);
        Console.Write("│".Pastel(Colors.Border).PastelBg(Colors.Background));
      }

      Console.SetCursorPosition(0, Console.GetCursorPosition().Top + 1);
      Console.Write(
          (new string(' ', s_padding) + "└" + new string('─', _width - s_padding * 2) + "┘")
            .Pastel(Colors.Border)
            .PastelBg(Colors.Background)
      );
    }

    private void RefreshInput()
    {
      Console.SetCursorPosition(s_padding, _height + 2);
      Console.Write(s_inputPrompt.Pastel(Colors.InputPrompt) + _input + " ");
    }

    private void RefreshGrid()
    {
      int i = 0;
      foreach (string[] row in BattleshipGrid.GetAllRows())
      {
        Console.SetCursorPosition(s_padding + 1, i + 1);

        string[] rowCopy = new string[row.Length];

        row.CopyTo(rowCopy, 0);

        if (i == _crosshairLocation.Item2 - 1)
        {
          rowCopy[_crosshairLocation.Item1 - 1 - s_padding] = "┼".Pastel(Colors.Crosshair).PastelBg(Colors.Background);
        }

        string rowString = string.Join("", rowCopy);

        Console.Write(rowString);
        i++;
      }
    }

    // On Key Pressed
    private void HandleBackspace(ConsoleKeyInfo key)
    {
      if (key.Key == ConsoleKey.Backspace)
      {
        if (_input.Length > 0)
        {
          lock (_consoleLock)
          {
            Console.SetCursorPosition(GetEnd().Item1, GetEnd().Item2);
            Console.Write(" ");
          }
          _input = _input.Remove(_input.Length - 1);
        }
      }
    }

    private void HandleEnter(ConsoleKeyInfo key)
    {
      if (key.Key == ConsoleKey.Enter)
      {
        if (_input.Length > 0)
        {
          lock (_consoleLock)
          {
            Console.SetCursorPosition(GetEnd().Item1, GetEnd().Item2);
            Console.Write(new string('\b', _input.Length - 1) + new string(' ', _input.Length + 5));
          }
        }
        OnInput(_input);
        _input = "";
      }
    }

    private void HandleAlphaNumeric(ConsoleKeyInfo key)
    {
      if (char.IsLetterOrDigit(key.KeyChar) || key.Key == ConsoleKey.Spacebar)
      {
        _input += key.KeyChar;
      }
    }

    private void HandleEscape(ConsoleKeyInfo key)
    {
      if (key.Key == ConsoleKey.Escape)
      {
        Stop();
      }
    }

    private void HandleArrowKeys(ConsoleKeyInfo key)
    {
      switch (key.Key)
      {
        case ConsoleKey.LeftArrow:
          MoveCrosshairBy(-1, 0);
          break;
        case ConsoleKey.RightArrow:
          MoveCrosshairBy(1, 0);
          break;
        case ConsoleKey.UpArrow:
          MoveCrosshairBy(0, -1);
          break;
        case ConsoleKey.DownArrow:
          MoveCrosshairBy(0, 1);
          break;
        default: break;
      }
    }
    // SINGLETON GETTERS

    public static GameScreen GetScreen(int width, int height)
    {
      if (s_instance._height != height || s_instance._width != width)
      {
        s_instance = new GameScreen(width, height);
      }

      return s_instance;
    }

    public static GameScreen GetScreen()
    {
      return s_instance;
    }


    // HELPERS
    private (int, int) GetEnd()
    {
      int left = (s_inputPrompt.Length + _input.Length) % Console.BufferWidth;
      int top = _height + 1 + ((s_inputPrompt.Length + _input.Length) / Console.BufferWidth + left > 0 ? 1 : 0);


      return (left, top);
    }

    private void MoveCrosshairBy(int x, int y)
    {
      (int, int) CrosshairOldLocation = _crosshairLocation;

      _crosshairLocation.Item1 += x;
      _crosshairLocation.Item2 += y;

      if (0 + s_padding >= _crosshairLocation.Item1 || _crosshairLocation.Item1 > _width - s_padding)
      {
        _crosshairLocation.Item1 = CrosshairOldLocation.Item1;
      }

      if (0 >= _crosshairLocation.Item2 || _crosshairLocation.Item2 > _height)
      {
        _crosshairLocation.Item2 = CrosshairOldLocation.Item2;
      }
    }
  }
}
