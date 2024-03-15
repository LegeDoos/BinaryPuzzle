namespace BinaryPuzzle
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello, binary puzzle!");

            /*
            BinaryPuzzle puzzle = new(6);
            puzzle.SetValue(1, 1, true);
            puzzle.SetValue(1, 3, true);            
            puzzle.SetValue(1, 6, true);            
            puzzle.SetValue(2, 5, false);
            puzzle.SetValue(3, 2, false);
            puzzle.SetValue(4, 5, false);
            puzzle.SetValue(5, 1, true);
            puzzle.SetValue(5, 4, true);
            puzzle.SetValue(6, 3, false);
            */

            BinaryPuzzle puzzle = new(10);
            puzzle.SetValue(1, 4, true);
            puzzle.SetValue(1, 6, true);
            puzzle.SetValue(1, 7, false);
            puzzle.SetValue(2, 6, true);
            puzzle.SetValue(2, 10, true);
            puzzle.SetValue(3, 2, true);
            puzzle.SetValue(3, 3, true);
            puzzle.SetValue(3, 10, true);
            puzzle.SetValue(4, 2, true);
            puzzle.SetValue(4, 4, false);
            puzzle.SetValue(4, 5, false);
            puzzle.SetValue(5, 8, true);
            puzzle.SetValue(5, 10, true);
            puzzle.SetValue(7, 1, false);
            puzzle.SetValue(7, 9, false);
            puzzle.SetValue(7, 10, false);
            puzzle.SetValue(8, 10, false);
            puzzle.SetValue(9, 7, false);
            puzzle.SetValue(10, 6, true);

            puzzle.PrintPuzzle();

            Console.WriteLine("Sart solving...");
            int tickCountStart = Environment.TickCount;
            puzzle.Solve();
            int tickCountEnd = Environment.TickCount;
            puzzle.PrintPuzzle();
            Console.WriteLine($"Time: {tickCountEnd - tickCountStart} ms");

        }
    }
}
