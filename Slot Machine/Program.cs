namespace Slot_Machine
{
    internal class Program
    {
        static void Main(string[] args)
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

            int balance = 500;
            int[,] arr = new int[3, 3];

            Console.WriteLine("Welkom to Slot Machine");
        }
    }
}
