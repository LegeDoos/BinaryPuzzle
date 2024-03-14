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
            puzzle.SetValue(1, 10, true);
            puzzle.PrintPuzzle();
        }
    }
}
