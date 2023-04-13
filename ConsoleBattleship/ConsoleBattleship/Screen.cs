using Pastel;
using System.Drawing;

namespace ConsoleBattleship
{
  partial class Screen
  {
    private static Screen Instance = new(Console.WindowWidth - (2 * Padding)-2, Console.WindowHeight - (2 * Padding)-4);
    private static readonly int Padding = 2;

    private readonly int Height; //Height scales 2x as fast as width
    private readonly int Width;
    private readonly int InitialScreenSize;
    private readonly string InputPrompt = "input: ";

    private bool running = true;
    private string ScreenString;
    private string Input = "";


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

      this.Width = width;
      this.Height = height;

      this.ScreenString = CreateScreenString();
      this.InitialScreenSize = ScreenString.Length;

      AddAllDelegates();
    }

    private string CreateScreenString()
    {
      String Top    = new String(' ', Padding) 
        + "┌".Pastel(Color.FromArgb(165, 229, 250)) 
        + new String('─', Width).Pastel(Color.FromArgb(165, 229, 250)) 
        + "┐\n".Pastel(Color.FromArgb(165, 229, 250));

      String Middle = new String(' ', Padding) 
        + "│".Pastel(Color.FromArgb(165, 229, 250)) 
        + new String(' ', Width) 
        + "│\n".Pastel(Color.FromArgb(165, 229, 250));

      String Bottom = new String(' ', Padding) 
        + "└".Pastel(Color.FromArgb(165, 229, 250)) 
        + new String('─', Width).Pastel(Color.FromArgb(165, 229, 250)) 
        + "┘\n".Pastel(Color.FromArgb(165, 229, 250));

      return Top + string.Concat(Enumerable.Repeat(Middle, Height)) + Bottom + InputPrompt.Pastel(Color.FromArgb(250, 129, 120));
    }

    private void AddAllDelegates()
    {
      OnRefresh += RefreshScreen;

      OnKeyPressed += HandleBackspace;
      OnKeyPressed += HandleEnter;
      OnKeyPressed += HandleAlphaNumeric;
      OnKeyPressed += HandleEscape;
    }


    // EVENT HANDLERS
      // On Refresh
    private void RefreshScreen()
    {
      Console.SetCursorPosition(0, 0);
      Console.Write(ScreenString);
    }

      // On Key Pressed
    private void HandleBackspace(ConsoleKeyInfo key)
    {
      if (key.Key == ConsoleKey.Backspace)
      {
        if (Input.Length != 0)
        {
          Console.Write("\b ");
          ScreenString = ScreenString.Remove(ScreenString.Length - 1);
          Input = Input.Remove(Input.Length - 1);
          OnRefresh();
        }
      }
    }

    private void HandleEnter(ConsoleKeyInfo key)
    {
      if (key.Key == ConsoleKey.Enter)
      {
        Console.Write(new string('\b', Input.Length));
        Console.Write(new string(' ', Input.Length));
        ScreenString = ScreenString[..InitialScreenSize];
        OnInput(Input);
        Input = "";
        OnRefresh();
      }
    }

    private void HandleAlphaNumeric(ConsoleKeyInfo key)
    {
      if (char.IsLetterOrDigit(key.KeyChar))
      {
        Input += key.KeyChar;
        ScreenString += key.KeyChar;
        OnRefresh();
      }
    }

    private void HandleEscape(ConsoleKeyInfo key)
    {
      if (key.Key == ConsoleKey.Escape)
      {
        running = false;
      }
    }
  

    // SINGLETON GETTERS
    public static Screen GetScreen(int width, int height)
    {
      if (Instance.Height != height || Instance.Width != width)
      {
        Instance = new Screen(width, height);
      }

      return Instance;
    }

    public static Screen GetScreen()
    {
      return Instance;
    }


    // GAME LOOP HANDLING
    private void ReceiveInputContinuosly()
    {
      while (running)
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
      running = true;
      OnRefresh();
      StartReceivingInputContinuosly();
    }

    public void Stop()
    {
        running = false;
    }


  }
}
