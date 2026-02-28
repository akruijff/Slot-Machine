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
        enum Mode { CenterLine, HorizonalLines, VerticalLines, Diagnoals, Unkown };

        private const int MIN = 1;
        private const int MAX = 9;
        private const int WIN_FACTOR = 3;

        static void Main(string[] args)
        {
            int balance = 500;
            int[,] arr = new int[3, 3];
            Random random = new Random();

            while (balance > 0)
            {
                Console.Clear();
                WriteHeader();
                WriteSlotMachine(arr);
                Spin(arr, random);

                int bet = PlaceBet(balance);
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

        private static int PlaceBet(int balance)
        {
            Console.WriteLine();
            Console.WriteLine($"You're balance is: {balance}");
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
            Console.WriteLine("2. All three horizonal line");
            Console.WriteLine("3. All veritical lines");
            Console.WriteLine("4. Diagnoals");

            Mode mode = Mode.Unkown;
            while (mode == Mode.Unkown)
                mode = Console.ReadKey().Key switch
                {                    
                    ConsoleKey.D1 or ConsoleKey.NumPad1 => Mode.CenterLine,
                    ConsoleKey.D2 or ConsoleKey.NumPad2 => Mode.HorizonalLines,
                    ConsoleKey.D3 or ConsoleKey.NumPad3 => Mode.VerticalLines,
                    ConsoleKey.D4 or ConsoleKey.NumPad4 => Mode.Diagnoals,
                    _ => Mode.Unkown,
                };
            return mode;
        }

        private static int CheckResult(int[,] arr, Mode mode, int balance, int bet)
        {
            Console.WriteLine();
            switch (mode)
            {
                case Mode.CenterLine:
                    if (CheckRow(arr, 1))
                    {
                        int won = bet * WIN_FACTOR;
                        Console.WriteLine("You won {0} on the center line!", won);
                        balance += won;
                    }
                    else
                        Console.WriteLine("You lost on the center line.");
                    return balance;
                case Mode.HorizonalLines:
                case Mode.VerticalLines:
                case Mode.Diagnoals:
                default:
                    return balance;
            }
        }

        private static bool CheckRow(int[,] arr, int row)
        {
            return arr[row, 0] == arr[row, 1] && arr[row, 0] == arr[row, 2];
        }

        private static bool UserWantsToExit()
        {
            Console.WriteLine("Press escape to exit");
            Console.WriteLine("Press any other key to continue");

            ConsoleKey key = Console.ReadKey().Key;
            return key == ConsoleKey.Escape;
        }
    }
}
