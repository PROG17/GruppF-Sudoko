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
            string sudokuLevel;
            string sudokuLevelStartBoard = "";

            sudokuLevel = Console.ReadLine().ToLower();

            while (true)
            {
                if (sudokuLevel == "easy" || sudokuLevel == "medium" || sudokuLevel == "hard" || sudokuLevel == "evil")
                {
                    switch (sudokuLevel)
                    {
                        case "easy":
                            //sudokuLevel = "619030040270061008000047621486302079000014580031009060005720806320106057160400030";
                            sudokuLevelStartBoard = "003020600900305001001806400008102900700000008006708200002609500800203009005010300";
                            break;
                        case "medium":
                            sudokuLevelStartBoard = "070005009008009100020070405800900530000020000034001002905080060003400700700500040";
                            break;
                        case "hard":
                            sudokuLevelStartBoard = "030002000000930001010780290050004600090000080002500010024059060500028000000100020";
                            break;
                        case "evil":
                            sudokuLevelStartBoard = "045000800000080000306900070030006920000204000064800010020001605000070000009000130";
                            break;
                        default:
                            sudokuLevelStartBoard = sudokuLevel;
                            break;
                    }
                    // Gå ur while loop
                    break;
                }
                else
                {
                    Console.WriteLine("Sudokunviån existerar inte, vänligen välj easy, medium, hard eller evil");
                    sudokuLevel = Console.ReadLine().ToLower();
                    continue;
                }
            }


            // instans av Game med sudokubrädet som värde
            Game game = new Game(sudokuLevelStartBoard);

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
