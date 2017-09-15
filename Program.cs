using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace Gruppinlämning2GruppF
{
    class Program
    {
        static void Main(string[] args)
        {
            // Kolla vilken sudokunivå som ska visas
            Console.WriteLine("Välj sudokunivå: easy, medium, hard eller evil");
            Game checkLevel = new Game();
            checkLevel.SudokuLevel = Console.ReadLine().ToLower();

            // instans av Game med sudokunivån som parameter
            Game game = new Game(checkLevel.SudokuLevel);

            // Färg på texten
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("---Startläge---");
            game.PrintBoardAsText();

            game.Solve();

            Console.WriteLine("---Slutläge---");
            game.PrintBoardAsText();


            Console.ReadLine();
        }
    }
}
