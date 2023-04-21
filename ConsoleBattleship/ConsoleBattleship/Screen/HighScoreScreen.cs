using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pastel;

namespace ConsoleBattleship.Screen
{
    internal class HighScoreScreen : BaseScreen
    {
        private static readonly int s_padding = 2;
        public static Score thingy = new Score();

        public static readonly List<string> highscoreItems = thingy.displayLeaderboard();


        private readonly int _highscoreItemCount = highscoreItems.Count();

        private int _selectedItem = 0;

        public delegate void HighScoreDelegate(string highscoreItem);
        public event HighScoreDelegate OnSelectedItem = delegate { };

        private static HighScoreScreen s_instance = new(
          Console.WindowWidth - (2 * s_padding) - 2,
          Console.WindowHeight - (3 * s_padding) - 4
        );


        // SINGLETON
        protected HighScoreScreen(int width, int height) : base(width, height)
        {
            OnRefresh += RefreshMenuFrame;

            OnKeyPressed += HandleArrowKeys;
            OnKeyPressed += HandleEnter;
            OnKeyPressed += HandleEscape;
        }

        // SINGLETON GETTERS

        public static HighScoreScreen GetScreen(int width, int height)
        {
            if (s_instance._height != height || s_instance._width != width)
            {
                s_instance = new(width, height);
            }

            return s_instance;
        }

        public static HighScoreScreen GetScreen()
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

            int middle = _height / _highscoreItemCount / 2;

            for (int i = 0; i < _highscoreItemCount; i++)
            {
                string line;
                for (int j = 0; j < _height / _highscoreItemCount; j++)
                {
                    Console.SetCursorPosition(s_padding, Console.GetCursorPosition().Top + 1);
                    line = ("│" + new string(' ', _width - s_padding * 2) + "│");



                    if (j == middle)
                    {
                        int spaceCount = (_width - s_padding * 2) / 2 - highscoreItems[i].Length / 2;
                        line =
                            "│"
                          + new string(' ', spaceCount)
                          + highscoreItems[i]
                          + new string(' ', spaceCount - (highscoreItems[i].Length % 2 == 0 ? 0 : 1))
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
                if (i == _highscoreItemCount - 1)
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
                OnSelectedItem(highscoreItems[_selectedItem]);
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
                    _selectedItem = (_selectedItem + highscoreItems.Count - 1) % _highscoreItemCount;
                    break;
                case ConsoleKey.DownArrow:
                    _selectedItem = (_selectedItem + 1) % _highscoreItemCount;
                    break;
                default: break;
            }
        }
    }
}
