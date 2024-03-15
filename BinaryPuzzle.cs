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
            // puzzlesize should be even
            if (puzzleSize % 2 != 0)
            {
                throw new Exception("Puzzle size should be even");
            }
            // puzzlesize should be at least 4
            if (puzzleSize < 4)
            {
                throw new Exception("Puzzle size should be at least 4");
            }

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
            //Console.Clear();

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

        /// <summary>
        /// Clone the puzzle
        /// </summary>
        /// <returns></returns>
        private BinaryPuzzle Clone()
        {
            BinaryPuzzle clone = new(Size);
            for (int i = 0; i <= Size; i++)
            {
                for (int j = 0; j <= Size; j++)
                {
                    clone.Values[i, j, 0] = Values[i, j, 0];
                    clone.Values[i, j, 1] = Values[i, j, 1];
                }
            }
            return clone;
        }

        public void Solve()
        {
            bool foundFreeSpot = false;
            int i = 0;
            int j = 0;
            while (i < Size && !foundFreeSpot)
            {
                i++;
                j = 0;
                while (j < Size && !foundFreeSpot)
                {
                    j++;
                    if (!Values[i, j, 1])
                    {
                        foundFreeSpot = true;
                    }
                }
            }    
            if (!foundFreeSpot)
            {
                // puzzle is solved
                return;
            }
            else
            {
                if (i == 6 && j == 9)
                {
                    PrintPuzzle();
                    Console.ReadLine();
                }
                try
                {
                    BinaryPuzzle clone = Clone();
                    clone.SetValue(i, j, true);
                    clone.Solve();
                    Values = clone.Values;
                }
                catch (Exception)
                {
                    // if true leads to exception, try false
                    try
                    {
                        BinaryPuzzle clone = Clone();
                        clone.SetValue(i, j, false);
                        clone.Solve();
                        Values = clone.Values;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
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
                result = ".";
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
            SetFinished(row, RowOrCol.Row);
            Validate(row, RowOrCol.Row);
            SetFinished(col, RowOrCol.Col);
            Validate(col, RowOrCol.Col);
            ValidatePuzzle();
        }

        private void ValidatePuzzle()
        {
            // check for double rows and double columns
            if (FindDoubleRowsOrColumns(null, 1, RowOrCol.Row))
            {
                throw new Exception("Double rows found");
            }
            if (FindDoubleRowsOrColumns(null, 1, RowOrCol.Col))
            {
                throw new Exception("Double columns found");
            }
        }

        private bool FindDoubleRowsOrColumns(List<int>? same, int pos, RowOrCol rowOrCol)
        {
            List<int> trues = new List<int>();
            List<int> falses = new List<int>();

            if (pos > Size)
            {
                return true;
            }

            if (same == null)
            {
                // first time
                pos = 1;

                // dooloop alle items indien de eerste keer
                for (int i = 1; i <= Size; i++)
                {
                    // als betreffende rij klaar is
                    if (SetFinished(i, rowOrCol))
                    {                        
                        if (Values[rowOrCol == RowOrCol.Row ? i : pos, rowOrCol == RowOrCol.Row ? pos : i, 0])
                        {
                            trues.Add(i);
                        }
                        else
                        {
                            falses.Add(i);
                        }
                    }
                }
            }
            else
            {
                // indien niet de eerste keer, dan doorloop de items uit de meegegeven verzameling
                foreach (var item in same)
                {
                    if (Values[rowOrCol == RowOrCol.Row ? item : pos, rowOrCol == RowOrCol.Col ? item : pos, 0])
                    {
                        trues.Add(item);
                    }
                    else
                    {
                        falses.Add(item);
                    }
                }
            }

            bool result = false;
            if (trues.Count > 1)
            {
                result = result || FindDoubleRowsOrColumns(trues, pos + 1, rowOrCol);
            }
            if (falses.Count > 1)
            {
                result = result || FindDoubleRowsOrColumns(falses, pos + 1, rowOrCol);
            }
            return result;
        }

        /// <summary>
        /// Validate the row or column
        /// </summary>
        /// <param name="idToValidate">The id to validate</param>
        /// <param name="rowOrCol">Enum determining row or column</param>
        /// <exception cref="Exception">when not valid</exception>
        private void Validate(int idToValidate, RowOrCol rowOrCol)
        {
            int row = rowOrCol == RowOrCol.Row ? idToValidate : 0;
            int col = rowOrCol == RowOrCol.Col ? idToValidate : 0;

            // check the values
            int trueCount = 0;
            int falseCount = 0;

            for (int i = 1; i <= Size; i++)
            {
                // check number of trues and falses
                if (Values[rowOrCol == RowOrCol.Row ? row : i, rowOrCol == RowOrCol.Col ? col: i, 1])
                {
                    if (Values[rowOrCol == RowOrCol.Row ? row : i, rowOrCol == RowOrCol.Col ? col : i, 0])
                        trueCount++;
                    else
                        falseCount++;
                }
                if (trueCount > Size / 2 || falseCount > Size / 2)
                {
                    throw new Exception($"Too many same values in {rowOrCol}");
                }

                if (rowOrCol == RowOrCol.Row)
                {
                    if (i >= 3 && Values[row, i - 2, 1] && Values[row, i - 1, 1] && Values[row, i, 1])
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
                else
                {
                    if (i >= 3 && Values[i - 2, col, 1] && Values[i - 1, col, 1] && Values[i, col, 1])
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
        }

        /// <summary>
        /// Check if the row or column is finished and set finished flag
        /// </summary>
        /// <param name="idToFinish">The id of the row or column</param>
        /// <param name="rowOrCol">Enum determining row or column</param>
        /// <returns>True when finished</returns>
        private bool SetFinished(int idToFinish, RowOrCol rowOrCol)
        {
            int row = rowOrCol == RowOrCol.Row ? idToFinish : 0;
            int col = rowOrCol == RowOrCol.Col ? idToFinish : 0;

            if (Values[row, col, 0])
            {
                // return true if finish flag is set
                return true;
            }
            // else determine if row is finished
            for (int i = 1; i <= Size; i++)
            {
                if (!Values[rowOrCol == RowOrCol.Row ? row : i, rowOrCol == RowOrCol.Col ? col : i, 1])
                {
                    // row not finished
                    return false;
                }
            }
            Values[row, col, 0] = true;
            return true;
        }
    }
}
