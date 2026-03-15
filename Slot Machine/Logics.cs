namespace Slot_Machine
{
    internal class Logics
    {
        private const int WIN_FACTOR = 3;

        internal static double BetMultiplier(double bet, bool won)
        {
            return won ? bet * WIN_FACTOR : 0;
        }

        internal static bool CheckRow(int[,] arr, int row)
        {
            for (int y = 1; y < arr.GetLength(1); ++y)
                if (arr[row, 0] != arr[row, y])
                    return false;
            return true;
        }

        internal static bool CheckColumn(int[,] arr, int column)
        {
            for (int x = 1; x < arr.GetLength(1); ++x)
                if (arr[0, column] != arr[x, column])
                    return false;
            return true;
        }

        internal static bool CheckMainDiagonal(int[,] arr)
        {
            for (int i = 1; i < arr.GetLength(0); ++i)
                if (arr[0, 0] != arr[i, i])
                    return false;
            return true;
        }

        internal static bool CheckAntiDiagonal(int[,] arr)
        {
            for (int i = 1; i < arr.GetLength(0); ++i)
                if (arr[0, arr.GetLength(0) - 1] != arr[i, arr.GetLength(0) - i - 1])
                    return false;
            return true;
        }

    }
}
