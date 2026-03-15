namespace Slot_Machine
{
    internal class Program
    {
        /*
         * Design a game where the user can play a make-believe slot machine. The user will be asked to make a
         * wager to play various lines in a 3 x 3 grid. They can play center line, all three horizontal lines,
         * all vertical lines and diagonals.
         * 
         * For instance the user can enter $3 dollars and play all three horizontal lines. If the top line hits 
         * a winning combination, they earn $1 dollar for that line.
         * 
         * Tips: The mention of a grid here should be a dead giveaway that you need a 2D array. You will  also 
         * need functionality that can check a horizontal line, a vertical line and a diagonal. Depending on 
         * the number of lines they play, you may need to execute all three of these statements one or multiple 
         * times to look for winning lines. If they are playing three lines, you would call your horizontal line 
         * check function three times... one for the top row, one for the center row and one for the bottom row. 
         * Each of these row checking algorithms will then need to look for winning combinations. The result is 
         * then dumped into the player’s money total. As for the mechanism to determine what the wheels produce 
         * per spin, use a random number generating function.
         */
        enum Mode { CenterLine, HorizontalLines, VerticalLines, Diagonals, Unkown };
        enum Won { 
            CenterLine,
            HorizontalTop, HorizontalCenter, HorizontalBottom,
            VerticalLinesLeft, VerticalLinesCenter, VerticalLinesRight,
            DiagonalsDiagonal, DiagonalsAnti,
            DidntWin,
        };

        private const int HORIONTAL_TOP = 0;
        private const int HORIONTAL_CENTER = 1;
        private const int HORIONTAL_BOTTOM = 2;
        private const int VERTICAL_LEFT = 0;
        private const int VERTICAL_CENTER = 1;
        private const int VERTICAL_RIGHT = 2;

        private const int MIN = 1;
        private const int MAX = 9;

        static void Main(string[] args)
        {
            double balance = 500;
            int[,] arr = new int[3, 3];
            Random random = new Random();

            while (balance > 0)
            {
                Console.Clear();
                WriteHeader();
                WriteSlotMachine(arr);
                Spin(arr, random);

                double bet = PlaceBet(balance);
                if (bet < 0)
                    continue;
                balance -= bet;

                Mode mode = PickMode();
                if (mode == Mode.Unkown)
                    continue;

                // ---------------------------------------------

                Console.Clear();
                WriteHeader();
                WriteSlotMachine(arr, true);

                balance = CheckResult(arr, mode, balance, bet);

                if (UserWantsToExit())
                    break;
            }
        }

        private static void WriteHeader()
        {
            Console.WriteLine("Welkom to Slot Machine");
            Console.WriteLine("");
        }

        private static void WriteSlotMachine(int[,] arr, bool showContent = false)
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

        private static void Spin(int[,] arr, Random random)
        {
            for (int x = 0; x < arr.GetLength(1); ++x)
                for (int y = 0; y < arr.GetLength(0); ++y)
                    arr[x, y] = random.Next(MIN, MAX);
        }

        private static double PlaceBet(double balance)
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

        private static Mode PickMode()
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

        private static double CheckResult(int[,] arr, Mode mode, double balance, double bet)
        {
            Console.WriteLine();
            Won won = Won.DidntWin;
            switch (mode)
            {
                case Mode.CenterLine:
                    if (Logics.CheckRow(arr, HORIONTAL_CENTER))
                        won = Won.CenterLine;
                    break;
                case Mode.HorizontalLines:
                    if (Logics.CheckRow(arr, HORIONTAL_TOP))
                        won = Won.HorizontalTop;
                    else if (Logics.CheckRow(arr, HORIONTAL_CENTER))
                        won = Won.HorizontalCenter;
                    else if (Logics.CheckRow(arr, HORIONTAL_BOTTOM))
                        won = Won.HorizontalBottom;
                    break;
                case Mode.VerticalLines:
                    if (Logics.CheckColumn(arr, VERTICAL_LEFT))
                        won = Won.VerticalLinesLeft;
                    else if (Logics.CheckColumn(arr, VERTICAL_CENTER))
                        won = Won.VerticalLinesCenter;
                    else if (Logics.CheckColumn(arr, VERTICAL_RIGHT))
                        won = Won.VerticalLinesRight;
                    break;
                case Mode.Diagonals:
                    if (Logics.CheckMainDiagonal(arr))
                        won = Won.DiagonalsDiagonal;
                    else if (Logics.CheckAntiDiagonal(arr))
                        won = Won.DiagonalsAnti;
                    break;
            }
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
            switch (won)
            {
                case Won.DidntWin:
                    balance = 0;
                    break;
                default:
                    balance = BetMultiplier(bet, won);
                    break;
            }
            return balance;
        }

        private static double BetMultiplier(double bet, Won won) => won switch
        {
            Won.CenterLine => Logics.BetMultiplier(bet),
            Won.HorizontalTop => Logics.BetMultiplier(bet / 3),
            Won.HorizontalCenter => Logics.BetMultiplier(bet / 3),
            Won.HorizontalBottom => Logics.BetMultiplier(bet / 3),
            Won.VerticalLinesLeft => Logics.BetMultiplier(bet / 3),
            Won.VerticalLinesCenter => Logics.BetMultiplier(bet / 3),
            Won.VerticalLinesRight => Logics.BetMultiplier(bet / 3),
            Won.DiagonalsDiagonal => Logics.BetMultiplier(bet / 2),
            Won.DiagonalsAnti => Logics.BetMultiplier(bet / 2),
            _ => 0,
        };

        private static void WriteResult(bool won, string line)
        {
            string s = won ? "won" : "lost";
            Console.WriteLine($"You {s} on the {line}.");
        }

        private static bool UserWantsToExit()

        {
            Console.WriteLine();
            Console.WriteLine("Press escape to exit");
            Console.WriteLine("Press any other key to continue");

            ConsoleKey key = Console.ReadKey().Key;
            return key == ConsoleKey.Escape;
        }
    }
}
