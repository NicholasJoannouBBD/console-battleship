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

    private static readonly int s_padding = 2;

    public static readonly string s_background = "V".Pastel(s_backgroundCharColor).PastelBg(s_backgroundColor);

    private static Screen s_instance = new(
      Console.WindowWidth - (2 * s_padding) - 2,
      Console.WindowHeight - (2 * s_padding) - 4
    );

    private readonly int _height; //Height scales 2x as fast as width
    private readonly int _width;
    private readonly string _inputPrompt = "input: ";

    private string _input = "";
    private (int, int) _cursorLocation = Console.GetCursorPosition();
    private (int, int) _crosshairLocation;
    private bool _movingCursor = false;
    private bool _cursorLocked = false;
    private bool _running = true;
    
    public readonly Grid BattleshipGrid;

    public delegate void RefreshDelegate();
    public event RefreshDelegate OnRefresh = delegate { };

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
      OnRefresh += RefreshCrosshair;

      OnKeyPressed += HandleBackspace;
      OnKeyPressed += HandleEnter;
      OnKeyPressed += HandleAlphaNumeric;
      OnKeyPressed += HandleEscape;
      OnKeyPressed += HandleArrowKeys;

      BattleshipGrid.OnChange += (int row, int column, string oldValue, string newValue) => { OnRefresh(); };
    }


    // EVENT HANDLERS
    // On Refresh
    private void RefreshBorder()
    {
      GetCursor();
      SetCursorToBegin();
      string Top = new string(' ', s_padding)
        + "┌".Pastel(s_borderColor).PastelBg(s_backgroundColor)
        + new string('─', _width - s_padding * 2).Pastel(s_borderColor).PastelBg(s_backgroundColor)
        + "┐".Pastel(s_borderColor).PastelBg(s_backgroundColor);

      Console.Write(Top);
      for (int i = 0; i < _height; i++)
      {
        SetCursorTo(s_padding, Console.GetCursorPosition().Top + 1);
        Console.Write("│".Pastel(s_borderColor).PastelBg(s_backgroundColor));
        SetCursorTo(_width - s_padding + 1, Console.GetCursorPosition().Top);
        Console.Write("│".Pastel(s_borderColor).PastelBg(s_backgroundColor));
      }

      SetCursorTo(0, Console.GetCursorPosition().Top + 1);

      string Bottom = new string(' ', s_padding)
        + "└".Pastel(s_borderColor).PastelBg(s_backgroundColor)
        + new string('─', _width - (s_padding * 2)).Pastel(s_borderColor).PastelBg(s_backgroundColor)
        + "┘".Pastel(s_borderColor).PastelBg(s_backgroundColor);
      Console.Write(Bottom);

      ReturnCursor();
    }

    private void RefreshInput()
    {
      GetCursor();
      SetCursorTo(s_padding, _height + 2);
      Console.Write(_inputPrompt.Pastel(s_inputPromptColor) + _input);
      ReturnCursor();
    }

    private void RefreshGrid()
    {
      GetCursor();
      SetCursorToBegin();
      foreach (string[] row in BattleshipGrid.GetAllRows())
      {
        SetCursorTo(s_padding + 1, Console.GetCursorPosition().Top + 1);

        //This is to reduce stuttering
        if (Console.GetCursorPosition().Top == _crosshairLocation.Item2)
        {
          Console.Write(string.Join("", row[..(_crosshairLocation.Item1 - 3)]));
          SetCursorTo(_crosshairLocation.Item1 + 1 , Console.GetCursorPosition().Top);
          Console.Write(string.Join("", row[(_crosshairLocation.Item1 - 2)..]));
        }
        else
        {
          Console.Write(string.Join("", row));
        }
        
      }
      ReturnCursor();
    }

    private void RefreshCrosshair()
    {
      GetCursor();
      SetCursorTo(_crosshairLocation.Item1, _crosshairLocation.Item2);
      Console.Write("┼".Pastel(s_crosshairColor).PastelBg(s_backgroundColor));

      // Bigger Crosshair
      /*SetCursorTo(CrosshairLocation.Item1, CrosshairLocation.Item2);
      Console.Write("╿".Pastel(CrosshairColor));
      SetCursorTo(CrosshairLocation.Item1 - 2, CrosshairLocation.Item2 - 1);
      Console.Write("─╼".Pastel(CrosshairColor));
      SetCursorTo(CrosshairLocation.Item1 + 1, CrosshairLocation.Item2 - 1);
      Console.Write("╾─".Pastel(CrosshairColor));
      SetCursorTo(CrosshairLocation.Item1, CrosshairLocation.Item2 - 2);
      Console.Write("╽".Pastel(CrosshairColor));*/

      ReturnCursor();
    }

    // On Key Pressed
    private void HandleBackspace(ConsoleKeyInfo key)
    {
      if (key.Key == ConsoleKey.Backspace)
      {
        if (_input.Length != 0)
        {
          GetCursor();
          SetCursorToEnd();
          Console.Write("\b ");
          ReturnCursor();

          _input = _input.Remove(_input.Length - 1);
          OnRefresh();
        }
      }
    }

    private void HandleEnter(ConsoleKeyInfo key)
    {
      if (key.Key == ConsoleKey.Enter)
      {
        GetCursor();
        SetCursorToEnd();
        Console.Write(new string('\b', _input.Length));
        Console.Write(new string(' ', _input.Length));
        ReturnCursor();

        OnInput(_input);
        _input = "";
        OnRefresh();
      }
    }

    private void HandleAlphaNumeric(ConsoleKeyInfo key)
    {
      if (char.IsLetterOrDigit(key.KeyChar))
      {
        _input += key.KeyChar;
        OnRefresh();
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

    public void Start()
    {
      _running = true;
      OnRefresh();
      StartReceivingInputContinuosly();
      // Doesn't really need to be refreshed more than once
      RefreshBorder();
    }

    public void Stop()
    {
        _running = false;
    }

    // HELPERS

    private void GetCursor()
    {
      while (_cursorLocked) { }

      _cursorLocked = true;
    }

    private void SetCursorToEnd()
    {
      int left  = (_inputPrompt.Length + _input.Length) % Console.BufferWidth;
      int top   = _height + 1 + ((_inputPrompt.Length + _input.Length) / Console.BufferWidth + left > 0 ? 1 : 0);

      SetCursorTo(left, top);
    }

    private void SetCursorToBegin()
    {
      SetCursorTo(0, 0);
    }

    private void SetCursorTo(int left, int top)
    {
      if (!_movingCursor)
      {
        _cursorLocation = Console.GetCursorPosition();
        _movingCursor = true;
      }

      Console.SetCursorPosition(left, top);
    }

    private void ReturnCursor()
    {      
      _movingCursor = false;
      Console.SetCursorPosition(_cursorLocation.Item1, _cursorLocation.Item2);
      _cursorLocked = false;
    }

    private void MoveCrosshairBy(int x, int y)
    {
      (int, int) CrosshairOldLocation = _crosshairLocation;

      _crosshairLocation.Item1 += x;
      _crosshairLocation.Item2 += y;

      int CrosshairSize = 0; // equal to crosshair size / 2 rounded down

      if (0 + s_padding + CrosshairSize >= _crosshairLocation.Item1 || _crosshairLocation.Item1 > _width - s_padding - CrosshairSize)
      {
        _crosshairLocation.Item1 = CrosshairOldLocation.Item1;
      }

      if (0 +  CrosshairSize >= _crosshairLocation.Item2 || _crosshairLocation.Item2 > _height)
      {
        _crosshairLocation.Item2 = CrosshairOldLocation.Item2;
      }


      OnRefresh();
    }
  }
}
