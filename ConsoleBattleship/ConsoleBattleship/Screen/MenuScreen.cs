using Pastel;

namespace ConsoleBattleship.Screen
{
  internal class MenuScreen : BaseScreen
  {
    private static readonly int s_padding = 2;
    public static readonly List<string> MenuItems = new(){
      "Host", "Join", "Options", "Quit"
    };


    private readonly int _menuItemCount = MenuItems.Count();

    private int _selectedItem = 0;

    public delegate void MenuDelegate(string menuItem);
    public event MenuDelegate OnSelectedMenuItem = delegate { };

    private static MenuScreen s_instance = new(
      Console.WindowWidth - (2 * s_padding) - 2,
      Console.WindowHeight - (2 * s_padding) - 4
    );

    // SINGLETON
    protected MenuScreen(int width, int height) : base(width, height)
    {
      OnRefresh += RefreshMenuFrame;

      OnKeyPressed += HandleArrowKeys;
      OnKeyPressed += HandleEnter;
      OnKeyPressed += HandleEscape;
    }

    // SINGLETON GETTERS

    public static MenuScreen GetScreen(int width, int height)
    {
      if (s_instance._height != height || s_instance._width != width)
      {
        s_instance = new(width, height);
      }

      return s_instance;
    }

    public static MenuScreen GetScreen()
    {
      return s_instance;
    }


    // EVENT HANDLERS
    // On Refresh
    private void RefreshMenuFrame()
    {
      SetCursorToBegin();
      Console.Write(
          (new string(' ', s_padding) + "┌" + new string('─', _width - s_padding * 2) + "┐")
            .Pastel(Colors.Border)
            .PastelBg(Colors.Background)
      );

      int middle = _height / _menuItemCount / 2;

      for (int i = 0; i < _menuItemCount; i++)
      {
        string line;
        for (int j = 0; j < _height / _menuItemCount; j++)
        {
          Console.SetCursorPosition(s_padding, Console.GetCursorPosition().Top + 1);
          line = ("│" + new string(' ', _width - s_padding * 2) + "│");



          if (j == middle)
          {
            int spaceCount = (_width - s_padding * 2) / 2 - MenuItems[i].Length / 2;
            line =
                "│"
              + new string(' ', spaceCount)
              + MenuItems[i]
              + new string(' ', spaceCount - (MenuItems[i].Length % 2 == 0 ? 0 : 1))
              + "│";
          }

          if (i == _selectedItem)
          {

            line = line
            .Pastel(Colors.HighlightedBorder)
            .PastelBg(Colors.HighlightedBackground);

          }
          else
          {
            line = line
               .Pastel(Colors.Border)
               .PastelBg(Colors.Background);
          }
          Console.Write(line);
        }


        Console.SetCursorPosition(s_padding, Console.GetCursorPosition().Top + 1);
        if (i == _menuItemCount - 1)
        {
          line = "└" + new string('─', _width - (s_padding * 2)) + "┘";
        }
        else
        {
          line = "├" + new string('─', _width - (s_padding * 2)) + "┤ ";
        }
        line = line
          .Pastel(Colors.Border)
          .PastelBg(Colors.Background);
        Console.Write(line);
      }
    }

    private void HandleEnter(ConsoleKeyInfo key)
    {
      if (key.Key == ConsoleKey.Enter)
      {
        OnSelectedMenuItem(MenuItems[_selectedItem]);
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
        case ConsoleKey.UpArrow:
          _selectedItem = (_selectedItem + MenuItems.Count - 1) % _menuItemCount;
          break;
        case ConsoleKey.DownArrow:
          _selectedItem = (_selectedItem + 1) % _menuItemCount;
          break;
        default: break;
      }
    }

  }
}
