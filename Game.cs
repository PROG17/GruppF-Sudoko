using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Gruppinlämning2GruppF
{
    public class Game
    {
        static string checkVault;
        public static string[] quadArr = new string[9];
        public static string[] rowArr = new string[9];
        public static string[] colArr = new string[9];
        //public static StringBuilder sb = new StringBuilder(Program.sudokuBoard);




        public void Solve()
        {
            while (Program.sudokuBoard.Contains("0"))
            {
                // Skapa ny rad, kolumn och kvadrat
                SplitToRow();
                SplitToCol();
                SplitToQuad();
                int quad = 0;


                for (int i = 0; i < Program.sudokuBoard.Length; i++)
                {

                    // Startvärden i början av varje ruta(char) på sudokubrädet
                    checkVault = "123456789";
                    int row = i / 9;
                    int col = i % 9;


                    if (row < 3 && col < 3) quad = 0;
                    else if (row < 3 && col < 6) quad = 1;
                    else if (row < 3 && col < 9) quad = 2;
                    else if (row < 6 && col < 3) quad = 3;
                    else if (row < 6 && col < 6) quad = 4;
                    else if (row < 6 && col < 9) quad = 5;
                    else if (row < 9 && col < 3) quad = 6;
                    else if (row < 9 && col < 6) quad = 7;
                    else if (row < 9 && col < 9) quad = 8;

                    // Kolla om rutan i sudokubrädet är 0, dvs inget värde


                    if (Program.sudokuBoard[i] == '0')
                    {
                        CheckAgainstRow(row);
                        CheckAgainstCol(col);
                        CheckAgainstQuad(quad);
                        string checking = ControlVault(checkVault);

                        if (checking.Length == 1) AddNumberToSudokuBoard(i, checking[0]);



                    }

                    // Skriv ut varje rad och kolumn tillhörande varje ruta
                    //Console.WriteLine($"Ruta {i} ger col = {col} och row = {row}");
                }


            }
        }


        public static void CheckAgainstRow(int row)
        {

            string checkingRow = rowArr[row];

            for (int i = 0; i < checkVault.Length; i++)
            {
                for (int j = 0; j < checkingRow.Length; j++)
                {
                    if (checkVault[i] == checkingRow[j])
                    {
                        checkVault = checkVault.Replace(checkVault[i], '0');
                    }

                }
            }


        }

        public static void CheckAgainstCol(int col)
        {
            string checkingCol = colArr[col];

            for (int i = 0; i < checkVault.Length; i++)
            {
                for (int j = 0; j < checkingCol.Length; j++)
                {
                    if (checkVault[i] == checkingCol[j])
                    {
                        checkVault = checkVault.Replace(checkVault[i], '0');
                    }

                }
            }

        }

        public static void CheckAgainstQuad(int quad)
        {
            string checkingQuad = quadArr[quad];

            for (int i = 0; i < checkVault.Length; i++)
            {
                for (int j = 0; j < checkingQuad.Length; j++)
                {
                    if (checkVault[i] == checkingQuad[j])
                    {
                        checkVault = checkVault.Replace(checkVault[i], '0');
                    }

                }
            }

        }


        public static void AddNumberToSudokuBoard(int oldChar, char newChar)
        {
            var sb = new StringBuilder(Program.sudokuBoard);
            sb[oldChar] = newChar;
            Program.sudokuBoard = sb.ToString();
        }

        public static string ControlVault(string check)
        {
            string text = "";

            foreach (var item in check)
            {
                if (item != '0') text += item;
            }

            return text;
        }

        public void PrintBoardAsText()
        {
            // Deklarerar variabler
            int counterSudokuBoard = Program.sudokuBoard.Length;
            int countRow = 0;
            int row = 0;

            // While loop som snurrar så länge vi har en plats kvar på brädet att skriva ut
            while (counterSudokuBoard > 0)
            {
                string border = "---------------------";

                Console.WriteLine("");
                if (countRow % 3 == 0) Console.WriteLine(border);
                ;

                //int countCursor = 0;

                for (int i = 1; i < 10; i++)
                {
                    // Skriv ut i mitten av konsolen
                    //countCursor += i;
                    //Console.SetCursorPosition((Console.WindowWidth - border.Length + i) / 2, Console.CursorTop);

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
                    Console.WriteLine(border);
                    Console.WriteLine("");

                    //Program.WriteCenteredText(border);
                }

            }

        }

        //Delar upp stringen i 9 delar med 9 siffror var, och 
        //tilldelar varje rowArr[index] 9 siffror i följd
        static void SplitToRow()
        {
            int counter = 0;

            for (int i = 0; i < rowArr.Length; i++)
            {
                rowArr[i] = Program.sudokuBoard.Substring(counter, 9);
                counter += 9;
            }

            //Console.WriteLine("Row");

            //foreach (var item in rowArr)
            //{
            //    Console.WriteLine(item);
            //}
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
                    colArr[i] = colArr[i] + Program.sudokuBoard.Substring(counter, 1);
                    counter += 9;
                }

            }

            //Console.WriteLine("Col");

            //foreach (var item in colArr)
            //{
            //    Console.WriteLine(item);
            //}
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
                    quadArr[i] = quadArr[i] + Program.sudokuBoard.Substring(counter, 3);
                    counter += 9;
                }

            }


            //Console.WriteLine("Quad");

            //foreach (var item in quadArr)
            //{
            //    Console.WriteLine(item);
            //}
        }

    }
}


