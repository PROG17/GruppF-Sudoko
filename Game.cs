using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Gruppinlämning2GruppF
{
    public class Game
    {
        private string[] quadArr = new string[9];
        private string[] rowArr = new string[9];
        private string[] colArr = new string[9];

        private string sudokuBoard;
        private string sudokuBoardIfStuck;
        private string checkAvailableNumbers;

        private List<String> listOfGuessingSudokuBoards = new List<string>();
        //static Dictionary<string, bool> dictSudoku = new Dictionary<string, bool>();
        private Dictionary<int, string> dictGuessingNumbers = new Dictionary<int, string>();

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
                Console.WriteLine("Tryck enter för att gå ett varv till på brädet");
                Console.ReadLine();
                PrintBoardAsText();

                // checkinBoard är hur sudokuBoard ser ut innan den börjar gå igenom varje cell
                string checkingBoard = sudokuBoard;
                LoopAllCellsAndCheckForEmptyOnes();
                
                if (checkAvailableNumbers.Length < 3)
                {
                    StartGuessing(countStops, checkingBoard);
                }
            }
        }

        void LoopAllCellsAndCheckForEmptyOnes()
        {
            for (int i = 0; i < sudokuBoard.Length; i++)
            {
                // Skapa ny rad, kolumn och kvadrat
                SplitToRow();
                SplitToCol();
                SplitToQuad();

                // Startvärden i början av varje ruta(char) på sudokubrädet
                string checkVault = "123456789";
                int row = i / 9;
                int col = i % 9;
                int quad = 0;

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
                    int zeroPosition = i;
                    checkVault = CheckAgainst(row, rowArr, checkVault);
                    checkVault = CheckAgainst(col, colArr, checkVault);
                    checkVault = CheckAgainst(quad, quadArr, checkVault);

                    // Kolla vilka siffror som finns kvar att välja på
                    checkAvailableNumbers = ControlVault(checkVault);

                    // Om det enbart finns en siffra, lägg till den på brädet
                    if (checkAvailableNumbers.Length == 1)
                    {
                        AddNumberToSudokuBoard(i, checkAvailableNumbers[0]);
                    }
                    else if (checkAvailableNumbers.Length != 0)
                    {
                        // Lägg till de möjliga gissningarna i en lista
                        AddGuessingNumbersToList(zeroPosition);
                    }
                }
            }
        }

        void AddGuessingNumbersToList(int zeroPosition)
        {
            if (dictGuessingNumbers.ContainsKey(zeroPosition))
            {
                dictGuessingNumbers[zeroPosition] = checkAvailableNumbers;
            }
            else 
            {
                dictGuessingNumbers.Add(zeroPosition, checkAvailableNumbers);
            }
        }

        void StartGuessing(int countStops, string checkingBoard)
        {

            // Börja gissa om programmet inte hittat någon lösning med ovan metoder 
            // och sista rutan i brädet som har 0 har fler än en siffra tillgänglig att välja från
            if (sudokuBoard == checkingBoard && checkAvailableNumbers.Length >= 2)
            {
                CantSolve();
                countStops++;

                // Vid första stoppet, lagra sudokuBoard
                if (countStops == 1) sudokuBoardIfStuck = sudokuBoard;

                // Gissa --- lägg till gissning i en lista med sudokus
                foreach (var item in dictGuessingNumbers)
                {
                    //AddGuessedNumbersToSudokuBoard(item.Key, item.Value);
                    Console.WriteLine($"{item.Key} --> {item.Value}");
                }

                //foreach (var item in listOfGuessingSudokuBoards)
                //{
                //    Console.WriteLine(item);    
                //}
                //IfStuck(checking, zeroPosition, guessIndex);

            }
            // Börja om och gissa med ny siffra om det inte går vägen
            else if (sudokuBoard == checkingBoard && checkAvailableNumbers.Length == 0)
            {
                CantSolve();
                Console.WriteLine("Börja om med ny gissning");
                sudokuBoard = sudokuBoardIfStuck;
                Console.ReadLine();
            }
        }

        

        static string CheckAgainst(int position, string[] arr, string checkVault)
        {
            // Lagra rad, kolumn, eller grupp i en egen variabel
            string checkAvailableNumbers = arr[position];

            for (int i = 0; i < checkVault.Length; i++)
            {
                for (int j = 0; j < checkAvailableNumbers.Length; j++)
                {
                    if (checkVault[i] == checkAvailableNumbers[j])
                    {
                        checkVault = checkVault.Replace(checkVault[i], '0');
                    }
                }
            }
            return checkVault;
        }

        void AddNumberToSudokuBoard(int oldChar, char newChar)
        {
            var sb = new StringBuilder(sudokuBoard);
            sb[oldChar] = newChar;
            sudokuBoard = sb.ToString();     
            
            SplitToCol();
            SplitToQuad();
            SplitToRow();
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
        void SplitToRow()
        {
            int counter = 0;

            for (int i = 0; i < rowArr.Length; i++)
            {
                rowArr[i] = sudokuBoard.Substring(counter, 9);
                counter += 9;
                //Console.WriteLine($"row({i}) = {rowArr[i]}");
            }
        }

        //Delar upp stringen i 9 delar med 9 siffror var, och 
        //tilldelar varje colArr[index] 9 siffror i följd
        void SplitToCol()
        {
            // Gör så att colArr blir tom
            string[] empty = new string[9];
            colArr = empty;

            for (int i = 0; i < colArr.Length; i++)
            {
                int counter = 0;
                counter += i;

                for (int j = 0; j < colArr.Length; j++)
                {
                    colArr[i] = colArr[i] + sudokuBoard.Substring(counter, 1);
                    counter += 9;
                }
                //Console.WriteLine($"col({i}) = {colArr[i]}");
            }
        }

        void SplitToQuad()
        {
            // Gör så att colArr blir tom
            string[] empty = new string[9];
            quadArr = empty;

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
                //Console.WriteLine($"quad({i}) = {quadArr[i]}");
            }
        }

        static void CantSolve()
        {
            Console.WriteLine("Kan tyvärr inte lösa sudoku med nuvarande metoder");
        }

        static void IfStuck(string numbersLeft, int arrayPosition, int numbersLeftPosition)
        {            
            //AddNumberToSudokuBoard(arrayPosition, numbersLeft[numbersLeftPosition]);
            Console.WriteLine("gissar");

        }

        //void AddGuessedNumbersToSudokuBoard(int oldChar, string numbersToGuess)
        //{
        //    for (int i = 0; i < numbersToGuess.Length; i++)
        //    {
        //        var sb = new StringBuilder(sudokuBoard);
        //        sb[oldChar] = numbersToGuess[i];
        //        listOfGuessingSudokuBoards.Add(sb.ToString());
        //    }

        //    //sudokuBoard = listOfGuessingSudokuBoards[oldChar];
        //}
    }
}


