using Pastel;

namespace ConsoleBattleship.Screen
{
    internal class LoginScreen : BaseScreen
    {
        private static readonly int s_padding = 2;
        public static readonly List<string> MenuItems = new(){
      "LOGIN", "User", "Password", "Submit", "Register"
    };

        private string _user = "";
        private string _password = "";

        private readonly int _menuItemCount = MenuItems.Count();

        private int _selectedItem = 1;

        public delegate void MenuDelegate(string menuItem);
        public event MenuDelegate OnSelectedMenuItem = delegate { };

        public delegate void LoginDelegate(string user, string password, bool registering);
        public event LoginDelegate OnLoginAttempt = delegate { };

        private static LoginScreen s_instance = new(
          Console.WindowWidth - (2 * s_padding) - 2,
          Console.WindowHeight - (2 * s_padding) - 4
        );

        // SINGLETON
        protected LoginScreen(int width, int height) : base(width, height)
        {
            OnRefresh += RefreshMenuFrame;

            OnKeyPressed += HandleArrowKeys;
            OnKeyPressed += HandleEnter;
            OnKeyPressed += HandleEscape;
            OnKeyPressed += HandleAlphaNumeric;

            OnSelectedMenuItem += HandleSubmit;
        }

        // SINGLETON GETTERS

        public static LoginScreen GetScreen(int width, int height)
        {
            if (s_instance._height != height || s_instance._width != width)
            {
                s_instance = new(width, height);
            }

            return s_instance;
        }

        public static LoginScreen GetScreen()
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
                        if (MenuItems[i] == "User")
                        {
                            line = "│ User     : [ " + _user + " ]" + new string(' ', _width - s_padding * 2 - 16 - _user.Length) + "│";
                        }
                        else if (MenuItems[i] == "Password")
                        {
                            line = "│ Password : [ " + new string('*', _password.Length) + " ]" + new string(' ', _width - s_padding * 2 - 16 - _password.Length) + "│";
                        }
                        else
                        {
                            int spaceCount = (_width - s_padding * 2) / 2 - MenuItems[i].Length / 2;
                            line =
                                "│"
                              + new string(' ', spaceCount)
                              + MenuItems[i]
                              + new string(' ', spaceCount - (MenuItems[i].Length % 2 == 0 ? 0 : 1))
                              + "│";
                        }
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


        // On Key Press
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
                    if (_selectedItem == 0)
                    {
                        _selectedItem = MenuItems.Count - 1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    _selectedItem = (_selectedItem + 1) % _menuItemCount;
                    if (_selectedItem == 0)
                    {
                        _selectedItem = 1;
                    }
                    break;
                default: break;
            }

        }

        private void HandleAlphaNumeric(ConsoleKeyInfo key)
        {
            if (char.IsLetterOrDigit(key.KeyChar) || key.Key == ConsoleKey.Spacebar)
            {
                if (MenuItems[_selectedItem] == "User")
                {
                    if (_user.Length < _width - s_padding * 2 - 17)
                    {
                        _user += key.KeyChar;
                    }
                }
                if (MenuItems[_selectedItem] == "Password")
                {
                    if (_password.Length < _width - s_padding * 2 - 17)
                    {
                        _password += key.KeyChar;
                    }
                }
            }
            if (key.Key == ConsoleKey.Backspace)
            {
                if (MenuItems[_selectedItem] == "User")
                {
                    if (_user.Length > 0)
                    {
                        _user = _user.Remove(_user.Length - 1);
                    }
                }
                if (MenuItems[_selectedItem] == "Password")
                {
                    if (_password.Length > 0)
                    {
                        _password = _password.Remove(_password.Length - 1);
                    }
                }
            }
        }

        // On Selection
        private void HandleSubmit(string item)
        {
            if (item == "Submit")
            {
                if (DbHandler.Instance.isValidUser(_user,_password))
                {
                    OnLoginAttempt(_user, _password, false);
                }
                else
                {
                    Console.WriteLine("FAKE");
                }

            }
            else if (item == "Register")
            {
                OnLoginAttempt(_user, _password, true);
            }
        }
    }
}
