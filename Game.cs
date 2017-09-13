using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Gruppinlämning2GruppF
{
    public class Game
    {
        public void Solve()
        {
            for (int i = 0; i < Program.sudokuBoard.Length; i++)
            { 
                string[] checkVault = "123456789";
                int row = i / 9;
                int col = i % 9;
                
                if (Program.sudokuBoard[i] == '0')
                {
                    
                }

                // Skriv ut varje rad och kolumn tillhörande varje ruta
                Console.WriteLine($"Ruta {i+1} ger col = {col+1} och row = {row+1}");
            }
            
        }

        public static void CheckAgainstRow(int row, int col)
        {


        }

        public void PrintBoardAsText()
        {
            // Deklarerar variabler
            int counterSudokuBoard = Program.sudokuBoard.Length;
            int countRow = 0;
            int row = 0;

            // Färg på texten
            Console.ForegroundColor = ConsoleColor.Cyan;

            // While loop som snurrar så länge vi har en plats kvar på brädet att skriva ut
            while (counterSudokuBoard > 0)
            {
                Console.WriteLine("");
                if (countRow % 3 == 0) Console.WriteLine("---------------------");


                for (int i = 1; i < 10; i++)
                {
                    // Beräkna vad i ska vara, dvs måste veta vilken rad
                    if (countRow > 0) row = i - 1 + 9 * (countRow);
                    if (countRow == 0) row = i - 1;

                    // om "0" => skriv ut blankt, om ej noll => skriv ut siffran
                    if (Program.sudokuBoard[row] == '0') Console.Write(" " + " "); 
                    if (Program.sudokuBoard[row] != '0') Console.Write(Program.sudokuBoard[row] + " "); 

                    // Skriv ut en breakpoint mellan var tredje kolumn
                    if (i % 3 == 0 && i != 9 && i != 0) Console.Write("| ");

                    // Räkna hur många tecken på brädet vi har kvar
                    counterSudokuBoard--;
                }

                // Öka rad räkningen
                countRow++;

                // Skriv ut sista raden med en linje

                if (countRow == 9)
                {
                    Console.WriteLine("");
                    Console.WriteLine("---------------------");
                }

            }

        }


    }
}
