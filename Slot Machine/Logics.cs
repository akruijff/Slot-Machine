namespace Slot_Machine
{
    internal class Logics
    {
        private const int WIN_FACTOR = 3;

        internal static double BetMultiplier(double bet, Won won) => won switch
        {
            Won.CenterLine => BetMultiplier(bet),
            Won.HorizontalTop => BetMultiplier(bet / 3),
            Won.HorizontalCenter => BetMultiplier(bet / 3),
            Won.HorizontalBottom => BetMultiplier(bet / 3),
            Won.VerticalLinesLeft => BetMultiplier(bet / 3),
            Won.VerticalLinesCenter => BetMultiplier(bet / 3),
            Won.VerticalLinesRight => BetMultiplier(bet / 3),
            Won.DiagonalsDiagonal => BetMultiplier(bet / 2),
            Won.DiagonalsAnti => BetMultiplier(bet / 2),
            _ => 0,
        };

        internal static double BetMultiplier(double bet) => bet * WIN_FACTOR;

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
