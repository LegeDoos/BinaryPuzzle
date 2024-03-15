namespace BinaryPuzzle
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello, binary puzzle!");
            BinaryPuzzle puzzle = new(10);
            puzzle.SetValue(1, 1, true);
            puzzle.SetValue(1, 2, true);
            puzzle.SetValue(1, 3, false);
            puzzle.SetValue(1, 4, false);
            puzzle.SetValue(1, 5, true);
            puzzle.SetValue(1, 6, true);
            puzzle.SetValue(1, 7, false);
            puzzle.SetValue(1, 8, false);
            puzzle.SetValue(1, 9, true);
            puzzle.SetValue(1, 10, false);

            puzzle.SetValue(3, 1, false);
            puzzle.SetValue(3, 2, true);
            puzzle.SetValue(3, 3, false);
            puzzle.SetValue(3, 4, false);
            puzzle.SetValue(3, 5, true);
            puzzle.SetValue(3, 6, true);
            puzzle.SetValue(3, 7, false);
            puzzle.SetValue(3, 8, false);
            puzzle.SetValue(3, 9, true);
            puzzle.SetValue(3, 10, true);

            puzzle.SetValue(2, 9, false);
            puzzle.SetValue(4, 9, false);
            puzzle.SetValue(5, 9, true);
            puzzle.SetValue(6, 9, true);
            puzzle.SetValue(7, 9, false);
            puzzle.SetValue(8, 9, false);
            puzzle.SetValue(9, 9, true);
            puzzle.SetValue(10, 9, true);

            puzzle.SetValue(2, 6, false);
            puzzle.SetValue(4, 6, false);
            puzzle.SetValue(5, 6, true);
            puzzle.SetValue(6, 6, true);
            puzzle.SetValue(7, 6, false);
            puzzle.SetValue(8, 6, false);
            puzzle.SetValue(9, 6, true);
            puzzle.SetValue(10, 6, true);
            puzzle.PrintPuzzle();       
        }
    }
}
