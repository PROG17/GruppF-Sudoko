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
        public static string sudokuBoard =
            "619030040270061008000047621486302079000014580031009060005720806320106057160400030";

        public static string[] quadArr = new string[9];
        public static string[] rowArr = new string[9];
        public static string[] colArr = new string[9];

        static void Main(string[] args)
        {

            SplitToRow();
            SplitToCol();
            SplitToQuad();

            Game game = new Game();

            game.PrintBoardAsText();
            game.AddNumberToBoard();
            game.Solve();


            Console.ReadLine();
        }
        //Delar upp stringen i 9 delar med 9 siffror var, och 
        //tilldelar varje rowArr[index] 9 siffror i följd
        static void SplitToRow() 
        {
            int counter = 0;

            for (int i = 0; i < rowArr.Length; i++)
            {
                rowArr[i] = sudokuBoard.Substring(counter, 9);
                counter += 9;
            }

            Console.WriteLine("Row");

            foreach (var item in rowArr)
            {
                Console.WriteLine(item);
            }
        }

        //Delar upp stringen i 9 delar med 9 siffror var, och 
        //tilldelar varje colArr[index] 9 siffror i följd
        static void SplitToCol()
        {

            for (int i = 0; i < colArr.Length; i++)
            {
                int counter = 0;
                counter += i;

                for (int j = 0; j < colArr.Length; j++)
                {
                    colArr[i] = colArr[i] + sudokuBoard.Substring(counter, 1);
                    counter += 9;
                }

            }

            Console.WriteLine("Col");

            foreach (var item in colArr)
            {
                Console.WriteLine(item);
            }
        }

        static void SplitToQuad()
        {

            for (int i = 0; i < quadArr.Length; i++)
            {
                int counter = 0;
             
                if (i > 0) counter += i * 3; // Om i > 0 => börja från rad 1. 
                if (i > 2) counter += 18; // Om i > 2 => börja från rad 4. 
                if (i > 4) counter += 18; // Om i > 4 => börja från rad 7 

                for (int j = 0; j < 3; j++)
                {
                    quadArr[i] = quadArr[i] + sudokuBoard.Substring(counter, 3);
                    counter += 9;
                }

            }

            Console.WriteLine("Quad");

            foreach (var item in quadArr)
            {
                Console.WriteLine(item);
            }
        }


    }
}
