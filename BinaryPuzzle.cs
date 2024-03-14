using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BinaryPuzzle
{
    public class BinaryPuzzle
    {
        /// <summary>
        /// 1st index is the row, 2nd index is the column, 3rd index is the status (0 = unset, 2 = set
        /// 1st row is status of the columns (0 = wip, 1 = completed)
        /// 1st column is status of the rows (0 = wip, 1 = completed)
        /// </summary>
        public bool[,,] Values { get; set; }

        /// <summary>
        /// Size of the puzzle
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Constructor for the BinaryPuzzle
        /// </summary>
        /// <param name="puzzleSize">The size of the puzzle</param>
        public BinaryPuzzle(int puzzleSize)
        {
            Size = puzzleSize;
            Values = new bool[puzzleSize + 1, puzzleSize + 1, 2];
            // set the status of first row and column
            for (int i = 0; i <= puzzleSize; i++)
            {
                Values[0, i, 1] = true;
                Values[i, 0, 1] = true;
            }
        }

        public void PrintPuzzle()
        {
            Console.Clear();

            for (int i = 0; i <= Size; i++)
            {

                for (int j = 0; j <= Size; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        Console.Write("".PadLeft(5));
                    }
                    else if (i == 0)
                    {
                        Console.Write($"{j}:{PosToString(i, j, 0)}".PadLeft(5));
                    }
                    else if (j == 0)
                    {
                        Console.Write($"{i}:{PosToString(i, j, 0)}".PadLeft(5));
                    }
                    else
                    {
                        Console.Write(PosToString(i, j, 5));
                    }
                }
                if (i == 0)
                {
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }

        private string PosToString(int row, int col, int padding)
        {
            string result;
            if (Values[row, col, 1])
            {
                result = Values[row, col, 0] ? "1" : "0";
            }
            else
            { 
                result = "X";
            }
            return padding > 0 ? result.PadLeft(padding) : result;
        }

        public void SetValue(int row, int col, bool value)
        {
            if (Values[row, 0, 0] || Values[0, col, 0])
            {
                throw new Exception("Row or column already finished");
            }
            if (Values[row, col, 1])
            {
                throw new Exception("Value already set");
            }
            Values[row, col, 1] = true;
            Values[row, col, 0] = value;
            ValidateRow(row);
            ValidateCol(col);
            ValidatePuzzle();
        }

        private void ValidatePuzzle()
        {
            // check for double rows and double columns
            
            // if finished row count > 1
            // todo finish this
        }

        private void ValidateRow(int row)
        {
            if (RowFinished(row))
            {
                Values[row, 0, 0] = true;
            }
            // check the values
            for (int i = 3; i <= Size; i++)
            {
                if (Values[row, i - 2, 1] && Values[row, i - 1, 1] && Values[row, i, 1])
                {
                    // last three are set
                    if ((Values[row, i - 2, 0] && Values[row, i - 1, 0] && Values[row, i, 0])
                        || (!Values[row, i - 2, 0] && !Values[row, i - 1, 0] && !Values[row, i, 0]))
                    {
                        // three values the same
                        throw new Exception("Invalid row");
                    }
                }
            }
        }

        private void ValidateCol(int col)
        {
            if (ColFinished(col))
            {
                Values[0, col, 0] = true;
            }
            // check the values
            for (int i = 3; i <= Size; i++)
            {
                if (Values[i - 2, col, 1] && Values[i - 1, col, 1] && Values[i, col, 1])
                {
                    // last three are set
                    if ((Values[i - 2, col, 0] && Values[i - 1, col, 0] && Values[i, col, 0])
                        || (!Values[i - 2, col, 0] && !Values[i - 1, col, 0] && !Values[i, col, 0]))
                    {
                        // three values the same
                        throw new Exception("Invalid col");
                    }
                }
            }
        }
        private bool RowFinished(int row)
        {
            for (int i = 1; i <= Size; i++)
            {
                if (!Values[row, i, 1])
                {
                    // row not finished
                    return false;
                }
            }
            return true;
        }

        private bool ColFinished(int col)
        {
            for (int i = 1; i <= Size; i++)
            {
                if (!Values[i, col, 1])
                {
                    // col not finished
                    return false;
                }
            }
            return true;
        }
    }
}
