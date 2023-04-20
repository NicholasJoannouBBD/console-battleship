using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBattleship.UserServices
{
    class Menu
    {
        private List<string> menuOptions;

        public Menu(List<string> options)
        {
            menuOptions = options;
        }

        public void displayMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome to BattleShip!");
            Console.WriteLine("------------------------");
            for (int i = 0; i < menuOptions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {menuOptions[i]}");
            }
            Console.WriteLine("------------------------");
            GetUserChoice();
        }

        public int GetUserChoice()
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > menuOptions.Count)
            {
                Console.WriteLine("Invalid input. Please enter a valid option:");
            }
            return choice;
        }


    }
}
