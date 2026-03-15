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

        private const int HORIONTAL_TOP = 0;
        private const int HORIONTAL_CENTER = 1;
        private const int HORIONTAL_BOTTOM = 2;
        private const int VERTICAL_LEFT = 0;
        private const int VERTICAL_CENTER = 1;
        private const int VERTICAL_RIGHT = 2;

        private const int MIN = 1;
        private const int MAX = 9;
        private const int WIN_FACTOR = 3;

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
            switch (mode)
            {
                case Mode.CenterLine:
                    balance += WriteResult(arr, bet, Logics.CheckRow(arr, HORIONTAL_CENTER), "center line");
                    return balance;
                case Mode.HorizontalLines:
                    balance += WriteResult(arr, bet / 3, Logics.CheckRow(arr, HORIONTAL_TOP),    "   top horizontal line");
                    balance += WriteResult(arr, bet / 3, Logics.CheckRow(arr, HORIONTAL_CENTER), "center horizontal line");
                    balance += WriteResult(arr, bet / 3, Logics.CheckRow(arr, HORIONTAL_BOTTOM), "bottom horizontal line");
                    return balance;
                case Mode.VerticalLines:
                    balance += WriteResult(arr, bet / 3, Logics.CheckColumn(arr, VERTICAL_LEFT),   "  left vertical line");
                    balance += WriteResult(arr, bet / 3, Logics.CheckColumn(arr, VERTICAL_CENTER), "center vertical line");
                    balance += WriteResult(arr, bet / 3, Logics.CheckColumn(arr, VERTICAL_RIGHT),  " right vertical line");
                    return balance;
                case Mode.Diagonals:
                    balance += WriteResult(arr, bet / 2, Logics.CheckMainDiagonal(arr), "main diagonal line");
                    balance += WriteResult(arr, bet / 2, Logics.CheckAntiDiagonal(arr), "anti diagonal line");
                    return balance;
            }
            return balance;
        }

        private static double WriteResult(int[,] arr, double bet, bool won, string line)
        {
            double result = BetMultiplier(bet, won);
            WriteResult(won, line);
            return result;
        }

        private static void WriteResult(bool won, string line)
        {
            string s = won ? "won" : "lost";
            Console.WriteLine($"You {s} on the {line}.");
        }

        private static double BetMultiplier(double bet, bool won)
        {
            return won ? bet * WIN_FACTOR : 0;
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
