using Pastel;
using System.Drawing;
using System.Text;

namespace ConsoleBattleship
{
  partial class Screen
  {
    private static readonly Color s_backgroundColor = Color.FromArgb(0, 10, 10); // Dark Cyan
    private static readonly Color s_backgroundCharColor = Color.FromArgb(0, 100, 100); // Cyan
    private static readonly Color s_crosshairColor = Color.FromArgb(250, 250, 120); // Yellow
    private static readonly Color s_borderColor = Color.FromArgb(165, 229, 250); // Blue
    private static readonly Color s_inputPromptColor = Color.FromArgb(250, 129, 120); // Red

    private static readonly int s_refreshRate = 10;
    private static readonly int s_padding = 2;

    public static readonly string s_background = "V".Pastel(s_backgroundCharColor).PastelBg(s_backgroundColor);

    private static Screen s_instance = new(
      Console.WindowWidth - (2 * s_padding) - 2,
      Console.WindowHeight - (2 * s_padding) - 4
    );

    private readonly int _height; //Height scales 2x as fast as width
    private readonly int _width;
    private readonly string _inputPrompt = "input: ";
    private readonly object _consoleLock = new object();

    private string _input = "";
    private (int, int) _crosshairLocation;
    private bool _running = true;

    public readonly Grid BattleshipGrid;

    public delegate void RefreshDelegate();
    public Action OnRefresh = delegate { };

    public delegate void InputDelegate(string input);
    public event InputDelegate OnInput = delegate { };

    public delegate void KeyPressedDelegate(ConsoleKeyInfo key);
    public event KeyPressedDelegate OnKeyPressed = delegate { };


    // SINGLETON CONSTRUCTOR
    private Screen(int width, int height)
    {
      Console.CursorVisible = false;
      Console.OutputEncoding = new UnicodeEncoding();

      _width = width;
      _height = height;

      _crosshairLocation = (width / 2, height / 2);

      BattleshipGrid = new Grid(width - s_padding * 2, height, s_background);

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
      string Top = new string(' ', s_padding)
        + "┌".Pastel(s_borderColor).PastelBg(s_backgroundColor)
        + new string('─', _width - s_padding * 2).Pastel(s_borderColor).PastelBg(s_backgroundColor)
        + "┐".Pastel(s_borderColor).PastelBg(s_backgroundColor);

      Console.Write(Top);
      for (int i = 0; i < _height; i++)
      {
        Console.SetCursorPosition(s_padding, Console.GetCursorPosition().Top + 1);
        Console.Write("│".Pastel(s_borderColor).PastelBg(s_backgroundColor));
        Console.SetCursorPosition(_width - s_padding + 1, Console.GetCursorPosition().Top);
        Console.Write("│".Pastel(s_borderColor).PastelBg(s_backgroundColor));
      }

      Console.SetCursorPosition(0, Console.GetCursorPosition().Top + 1);

      string Bottom = new string(' ', s_padding)
        + "└".Pastel(s_borderColor).PastelBg(s_backgroundColor)
        + new string('─', _width - (s_padding * 2)).Pastel(s_borderColor).PastelBg(s_backgroundColor)
        + "┘".Pastel(s_borderColor).PastelBg(s_backgroundColor);
      Console.Write(Bottom);

    }

    private void RefreshInput()
    {
        Console.SetCursorPosition(s_padding, _height + 2);
        Console.Write(_inputPrompt.Pastel(s_inputPromptColor) + _input + " ");
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
          rowCopy[_crosshairLocation.Item1 - 1 - s_padding] = "┼".Pastel(s_crosshairColor).PastelBg(s_backgroundColor);
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
        _running = false;
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
    public static Screen GetScreen(int width, int height)
    {
      if (s_instance._height != height || s_instance._width != width)
      {
        s_instance = new Screen(width, height);
      }

      return s_instance;
    }

    public static Screen GetScreen()
    {
      return s_instance;
    }


    // GAME LOOP HANDLING
    private void ReceiveInputContinuosly()
    {
      while (_running)
      {
        if (Console.KeyAvailable)
        {
          OnKeyPressed(Console.ReadKey(true));
        }
      }
    }

    private void StartReceivingInputContinuosly()
    {
      Thread In = new Thread(() => {
        ReceiveInputContinuosly();
      });

      In.Start();
    }

    private void UpdateContinuously()
    {
      while (_running)
      {
        Thread.Sleep(s_refreshRate);
        lock (_consoleLock)
        {
          OnRefresh.Invoke();
        }
      }
    }

    private void StartUpdatingContinuosly()
    {
      Thread Update = new Thread(() =>
      {
        UpdateContinuously();
      });

      Update.Start();
    }

    public void Start()
    {
      _running = true;
      StartReceivingInputContinuosly();
      StartUpdatingContinuosly();
    }

    public void Stop()
    {
        _running = false;
    }

    // HELPERS
    private (int, int) GetEnd()
    {
      int left = (_inputPrompt.Length + _input.Length) % Console.BufferWidth;
      int top = _height + 1 + ((_inputPrompt.Length + _input.Length) / Console.BufferWidth + left > 0 ? 1 : 0);


      return (left, top);
    }

    private void SetCursorToBegin()
    {
      Console.SetCursorPosition(0, 0);
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
