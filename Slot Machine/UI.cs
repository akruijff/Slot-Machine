namespace Slot_Machine
{
    internal class UI
    {
        internal static void WriteHeader()
        {
            Console.Clear();
            Console.WriteLine("Welkom to Slot Machine");
            Console.WriteLine("");
        }

        internal static void WriteSlotMachine(int[,] arr, bool showContent = false)
        {
            Console.WriteLine("╔═══════╗");
            for (int y = 0; y < arr.GetLength(1); ++y)
            {
                Console.Write("║ ");
                for (int x = 0; x < arr.GetLength(0); ++x)
                    if (showContent)
                        Console.Write($"{arr[y, x]} ");
                    else
                        Console.Write("? ");
                Console.WriteLine("║");
            }
            Console.WriteLine("╚═══════╝");
        }

        internal static double PlaceBet(double balance)
        {
            Console.WriteLine();
            Console.WriteLine($"You're balance is: {balance:F2}");
            Console.Write("Place a bet: ");

            string? s = Console.ReadLine();
            if (s == null || !int.TryParse(s, out _))
                return -1;

            int bet = int.Parse(s);
            if (bet < 0)
                return -1;
            return bet;
        }

        internal static Mode PickMode()
        {
            Console.WriteLine();
            Console.WriteLine("What do you want to play?");
            Console.WriteLine("1. The center line");
            Console.WriteLine("2. All three Horizontal line");
            Console.WriteLine("3. All veritical lines");
            Console.WriteLine("4. Diagonals");

            Mode mode = Mode.Unkown;
            while (mode == Mode.Unkown)
                mode = Console.ReadKey().Key switch
                {
                    ConsoleKey.D1 or ConsoleKey.NumPad1 => Mode.CenterLine,
                    ConsoleKey.D2 or ConsoleKey.NumPad2 => Mode.HorizontalLines,
                    ConsoleKey.D3 or ConsoleKey.NumPad3 => Mode.VerticalLines,
                    ConsoleKey.D4 or ConsoleKey.NumPad4 => Mode.Diagonals,
                    _ => Mode.Unkown,
                };
            return mode;
        }

        internal static void WriteResult(Won won)
        {
            switch (won)
            {
                case Won.CenterLine:
                    WriteResult(true, "center line");
                    break;
                case Won.HorizontalTop:
                    WriteResult(true, "   top horizontal line");
                    break;
                case Won.HorizontalCenter:
                    WriteResult(true, "center horizontal line");
                    break;
                case Won.HorizontalBottom:
                    WriteResult(true, "bottom horizontal line");
                    break;
                case Won.VerticalLinesLeft:
                    WriteResult(true, "  left vertical line");
                    break;
                case Won.VerticalLinesCenter:
                    WriteResult(true, "center vertical line");
                    break;
                case Won.VerticalLinesRight:
                    WriteResult(true, " right vertical line");
                    break;
                case Won.DiagonalsDiagonal:
                    WriteResult(true, "main diagonal line");
                    break;
                case Won.DiagonalsAnti:
                    WriteResult(true, "anti diagonal line");
                    break;
                case Won.DidntWin:
                    WriteResult(false, "");
                    break;
            }
        }

        internal static void WriteResult(bool won, string line)
        {
            string s = won ? "won" : "lost";
            Console.WriteLine($"You {s} on the {line}.");
        }

        internal static bool UserWantsToExit()

        {
            Console.WriteLine();
            Console.WriteLine("Press escape to exit");
            Console.WriteLine("Press any other key to continue");

            ConsoleKey key = Console.ReadKey().Key;
            return key == ConsoleKey.Escape;
        }
    }
}
