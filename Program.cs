using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

/*Din Sudoku-lösare ska ta en sträng så här som sin indata:
619030040270061008000047621486302079000014580031009060005720806320106057160400030
Varje på varandra följande 9 siffror representerar en rad av Sudoku-brädet och ’0’ representerar en
tom cell. Det skulle fungera så här:
 static void Main(string[] args)
{
 Sudoku game = new Sudoku("003020600900305001001806400"+
 "008102900700000008006708200"+
 "002609500800203009005010300");
 game.Solve();
 Console.WriteLine(game.BoardAsText);
}
Detta skulle skriva ut något som ser ut så här:
*/

namespace Gruppinlämning2GruppF
{
    class Program
    {
        //Easy Sudoku
        //public static string sudokuBoard =
        //    "619030040270061008000047621486302079000014580031009060005720806320106057160400030";
        //Evil Sudoku
        public static string sudokuBoard =
            "045000800000080000306900070030006920000204000064800010020001605000070000009000130";
        //Hard Sudoku
        //public static string sudokuBoard =
        //      "030002000000930001010780290050004600090000080002500010024059060500028000000100020";

        ////Medium Sudoku
        //public static string sudokuBoard =
        //      "070005009008009100020070405800900530000020000034001002905080060003400700700500040";


        static void Main(string[] args)
        {
            

            Game game = new Game();

            // Färg på texten
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("---Startläge---");
            //WriteCenteredText("---Startläge---");
            game.PrintBoardAsText();

            //while (sudokuBoard.Contains("0"))
            //{ 
            game.Solve();
            //}

            Console.WriteLine("---Slutläge---");
            //WriteCenteredText("---Slutläge---");
            game.PrintBoardAsText();


            Console.ReadLine();
        }



        public static void WriteCenteredText(string text)
        {

            Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
            Console.WriteLine(text);

        }


    }
}
