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
                Console.WriteLine("Welkom to Slot Machine");
                Console.WriteLine("");

                WriteSlotMachine(arr);

                Console.WriteLine($"You're balance is: {balance}");
                Console.Write("Place a bet: ");

                string? s = Console.ReadLine();
                if (s == null || !int.TryParse(s, out _))
                    continue;

                int bet = int.Parse(s);
                balance -= bet;

                // ---------------------------------------------

                Spin(arr, random);

                Console.Clear();
                Console.WriteLine("Welkom to Slot Machine");
                Console.WriteLine("");

                WriteSlotMachine(arr, true);

                if (arr[1, 0] == arr[1, 1] && arr[1, 0] == arr[1, 2])
                {
                    int won = bet * WIN_FACTOR;
                    Console.WriteLine("You won {0}!", won);
                    balance += won;
                }
                else
                    Console.WriteLine("You lost.");

                Console.WriteLine("Press escape to exit");
                Console.WriteLine("Press any other key to continue");

                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Escape)
                    break;
            }
        }

        private static void WriteSlotMachine(int[,] arr, bool showContent = false)
        {
            Console.WriteLine("╔═══════╗");
            for (int x = 0; x < arr.GetLength(1); ++x)
            {
                Console.Write("║ ");
                for (int y = 0; y < arr.GetLength(0); ++y)
                    if (showContent)
                        Console.Write($"{arr[x, y]} ");
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
    }
}
