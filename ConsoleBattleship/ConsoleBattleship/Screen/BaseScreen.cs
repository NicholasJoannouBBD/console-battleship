using System.Text;

namespace ConsoleBattleship.Screen
{
  public class BaseScreen
  {
    private static readonly int s_refreshRate = 10;

    protected readonly int _height; //Height scales 2x as fast as width
    protected readonly int _width;
    protected readonly object _consoleLock = new object();

    private bool _running = true;

    public delegate void RefreshDelegate();
    public Action OnRefresh = delegate { };

    public delegate void KeyPressedDelegate(ConsoleKeyInfo key);
    public event KeyPressedDelegate OnKeyPressed = delegate { };


    // SINGLETON CONSTRUCTOR
    protected BaseScreen(int width, int height)
    {
      Console.CursorVisible = false;
      Console.OutputEncoding = new UnicodeEncoding();

      _width = width;
      _height = height;
    }


    // SCREEN LOOP HANDLING
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
        Thread.Sleep(s_refreshRate*2);
        Console.Clear();
    } 


    // HELPERS
    protected void SetCursorToBegin()
    {
      Console.SetCursorPosition(0, 0);
    }

  }
}
