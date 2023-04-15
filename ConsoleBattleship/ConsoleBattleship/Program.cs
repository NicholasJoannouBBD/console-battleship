using ConsoleBattleship;
using System.Data.SQLite;

DbHandler dbHandler = new DbHandler();

string url = @"URI=file:battleship.db";
using var connection = new SQLiteConnection(url);
connection.Open();