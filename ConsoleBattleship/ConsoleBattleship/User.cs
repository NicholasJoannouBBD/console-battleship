using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleBattleship.Ships;

namespace ConsoleBattleship
{
    internal class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set; }
        //Grid made of cells
        public GameBoard GameBoard { get; set; }
        public GameBoard FiringBoard { get; set; }
        public List<Ship> Ships { get; set; } 

        public bool HasLost
        {
            get
            {
                return Ships.All(x => x.IsSunk);
            }
        }

        public User(int userId, string userName, int wins, int loses)
        {
            UserId = userId;
            UserName = userName;
            Wins = wins;
            Loses = loses;
            Ships = new List<Ship>()
            {
                new BattleShip(),
                new Destroyer(),
                new Carrier(),
                new Cruiser(),
                new Submarine()
            };
            GameBoard = new GameBoard(10, 10);
            FiringBoard = new GameBoard(10, 10);
        }

        //Random placement of ships on the Gameboard
        //Two ships cannot occupy the same cell
        //Game board will have a list of cells
        public void SetupShips()
        {
            //place each ship on the player's board
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            foreach (var ship in Ships)
            {
                bool isOpen = true;
                while (isOpen) 
                {
                    var startcolumn = rnd.Next(1, GameBoard.Height + 1);
                    var startrow = rnd.Next(1, GameBoard.Width + 1);
                    int endrow = startrow, endcolumn = startcolumn;
                    var orientation = rnd.Next(1, 101) % 2; //0 for Horizontal

                    if (orientation == 0)
                    {
                        for (int i = 1; i < ship.Size; i++)
                        {
                            endrow++;
                        }
                    }
                    else
                    {
                        for (int i = 1; i < ship.Size; i++)
                        {
                            endcolumn++;
                        }
                    }

                    //We cannot place ships beyond the boundaries of the board
                    if (endrow > GameBoard.Width || endcolumn > GameBoard.Height)
                    {
                        isOpen = true;
                        continue; //Restart the while loop to select a new random cell
                    }

                    //check if specified cells are occupied 
                    var affectedCells = checkCellOccupation(GameBoard.Cells, startrow,startcolumn, endrow, endcolumn);

                    if(affectedCells.Any(x => x.IsOccupied))
                    {
                        isOpen = true;
                        continue;
                    }

                    foreach(var cell in affectedCells) 
                    {
                        cell.CellOccupation = ship.CellOccupation;
                    }
                    isOpen = false;
                }
            }
        }

        private List<Cell> checkCellOccupation(List<Cell> cells, int startRow, int startColumn, int endRow, int endColumn)
        {
            return cells.Where(x => x.Position.Row >= startRow && 
                                    x.Position.Column >= startColumn &&
                                    x.Position.Row <= endRow &&
                                    x.Position.Column <= endColumn).ToList();
        }

        private Cell At(List<Cell> cells, int row, int column)
        {
            return cells.Where(x => x.Position.Row == row && x.Position.Column == column).First();
        }

        public void OutputBoards(GameBoard board, GameBoard firingBoard)
        {
            Console.WriteLine(UserName);
            Console.WriteLine("Own Board:                 Firing Board:");
            
            for(int row = 1; row <= board.Width; row++)
            {
                for(int column = 1; column <= board.Height; column++) 
                {
                    Cell cellAt = At(board.Cells, row, column);
                    if (!cellAt.Status.Equals("o"))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(cellAt.Status + " ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write(cellAt.Status + " ");
                    }
                    
                }
                Console.Write("         ");

                for(int firingColumn = 1; firingColumn <= firingBoard.Height; firingColumn++)
                {
                    Cell cellAt = At(firingBoard.Cells, row, firingColumn);
                    Console.Write(cellAt.Status + " ");
                }
                Console.WriteLine(Environment.NewLine);
            }
            Console.WriteLine(Environment.NewLine);
        }
    }
}
