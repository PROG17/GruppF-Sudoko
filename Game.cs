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
        public static string sudokuBoard;
        public static string sudokuBoardIfStuck;
        private string sudokuLevel;
        string checking;
        string guessNumbers;

        // Porperty för sudokunivå
        public string SudokuLevel
        {
            get { return sudokuLevel; }
            set
            {
                if (value == "easy" || value == "medium" || value == "hard" || value == "evil")
                {
                    switch (value)
                    {
                        case "easy":
                            //sudokuLevel = "619030040270061008000047621486302079000014580031009060005720806320106057160400030";
                            sudokuLevel = "003020600900305001001806400008102900700000008006708200002609500800203009005010300";
                            break;
                        case "medium":
                            sudokuLevel = "070005009008009100020070405800900530000020000034001002905080060003400700700500040";
                            break;
                        case "hard":
                            sudokuLevel = "030002000000930001010780290050004600090000080002500010024059060500028000000100020";
                            break;
                        case "evil":
                            sudokuLevel = "045000800000080000306900070030006920000204000064800010020001605000070000009000130";
                            break;
                        default:
                            sudokuLevel = value;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Sudokunviån existerar inte, vänligen välj easy, medium, hard eller evil");
                    SudokuLevel = Console.ReadLine();
                }
            }
        }

        // Construcor
        public Game()
        {
            
        }

        // Construcor
        public Game(string sudoku)
        {
            sudokuBoard = sudoku;
        }

        public void Solve()
        {
            int countStops = 0;
          
            while (sudokuBoard.Contains("0"))
            {
                // Startare för varje varv på brädet
                //Console.WriteLine("Tryck enter för att gå ett varv till på brädet");
                //Console.ReadLine();
                
                 string checkingBoard = sudokuBoard;

                // Skapa ny rad, kolumn och kvadrat
                SplitToRow();
                SplitToCol();
                SplitToQuad();
                
                int quad = 0;
                int zeroPosition = 0;

                for (int i = 0; i < sudokuBoard.Length; i++)
                {
                    // Startvärden i början av varje ruta(char) på sudokubrädet
                    checkVault = "123456789";
                    int row = i / 9;
                    int col = i % 9;

                    // Räknare för att veta vilken quad vi är i
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
                    if (sudokuBoard[i] == '0')
                    {
                        zeroPosition = i;
                        CheckAgainst(row, rowArr);
                        CheckAgainst(col, colArr);
                        CheckAgainst(quad, quadArr);

                        // Kolla vilka siffror som finns kvar att välja på
                        checking = ControlVault(checkVault);

                        // Om det enbart finns en siffra, lägg till den på brädet
                        if (checking.Length == 1) AddNumberToSudokuBoard(i, checking[0]);
                    }
                }

                if (sudokuBoard == checkingBoard)
                {
                    CantSolve();
                    PrintBoardAsText();
                    countStops++;
                    IfStuck(checking, zeroPosition);
                    //break;
                }

                // Vid första stoppet, lagra sudokuBoard
                if (countStops == 1) sudokuBoardIfStuck = sudokuBoard;
            }
        }

        public static void CheckAgainst(int position, string[] arr)
        {
            // Lagra rad, kolumn, eller grupp i en egen variabel
            string checking = arr[position];

            for (int i = 0; i < checkVault.Length; i++)
            {
                for (int j = 0; j < checking.Length; j++)
                {
                    if (checkVault[i] == checking[j])
                    {
                        checkVault = checkVault.Replace(checkVault[i], '0');
                    }
                }
            }
        }

        public static void AddNumberToSudokuBoard(int oldChar, char newChar)
        {
            var sb = new StringBuilder(sudokuBoard);
            sb[oldChar] = newChar;
            sudokuBoard = sb.ToString();
        }

        public static string ControlVault(string check)
        {
            string text = "";

            // Koll om strängen innehåller en siffra
            foreach (var item in check)
            {
                if (item != '0') text += item;
            }

            return text;
        }

        public void PrintBoardAsText()
        {
            // Deklarerar variabler
            int counterSudokuBoard = sudokuBoard.Length;
            int countRow = 0;
            int row = 0;

            // While loop som snurrar så länge vi har en plats kvar på brädet att skriva ut
            while (counterSudokuBoard > 0)
            {
                string border = "---------------------";

                Console.WriteLine("");
                if (countRow % 3 == 0) Console.WriteLine(border);
                
                for (int i = 1; i < 10; i++)
                {
                    // Beräkna vad i ska vara, dvs måste veta vilken rad
                    if (countRow > 0) row = i - 1 + 9 * (countRow);
                    if (countRow == 0) row = i - 1;

                    // om "0" => skriv ut blankt, om ej noll => skriv ut siffran
                    if (sudokuBoard[row] == '0') Console.Write(" " + " ");
                    if (sudokuBoard[row] != '0') Console.Write(sudokuBoard[row] + " ");

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
                rowArr[i] = sudokuBoard.Substring(counter, 9);
                counter += 9;
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
        }

        static void SplitToQuad()
        {

            for (int i = 0; i < quadArr.Length; i++)
            {
                int counter = 0;

                if (i > 0) counter += i * 3; // Om i > 0 => börja från rad 1. 
                if (i > 2) counter += 18; // Om i > 2 => börja från rad 4. 
                if (i > 5) counter += 18; // Om i > 4 => börja från rad 7 

                for (int j = 0; j < 3; j++)
                {
                    quadArr[i] = quadArr[i] + sudokuBoard.Substring(counter, 3);
                    counter += 9;
                }
            }
        }

        static void CantSolve()
        {            
                Console.WriteLine("Kan tyvärr inte lösa sudoku med nuvarande metoder");
        }

        static void IfStuck(string numbersLeft, int arrayPosition)
        {
 

                AddNumberToSudokuBoard(arrayPosition, numbersLeft[0]);
            Console.WriteLine("gissar");
            

        }
    }
}


