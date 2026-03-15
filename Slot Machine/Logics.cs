namespace Slot_Machine
{
    internal class Logics
    {
        private const int HORIONTAL_TOP = 0;
        private const int HORIONTAL_CENTER = 1;
        private const int HORIONTAL_BOTTOM = 2;
        private const int VERTICAL_LEFT = 0;
        private const int VERTICAL_CENTER = 1;
        private const int VERTICAL_RIGHT = 2;

        private const int SPIN_VALUE_MIN = 1;
        private const int SPIN_VALUE_MAX = 9;

        private const int WIN_FACTOR = 3;

        internal static void Spin(int[,] arr, Random random)
        {
            for (int x = 0; x < arr.GetLength(1); ++x)
                for (int y = 0; y < arr.GetLength(0); ++y)
                    arr[x, y] = random.Next(SPIN_VALUE_MIN, SPIN_VALUE_MAX);
        }

        internal static Won DetermineWinState(int[,] arr, Mode mode)
        {
            switch (mode)
            {
                case Mode.CenterLine:
                    if (Logics.CheckRow(arr, HORIONTAL_CENTER))
                        return Won.CenterLine;
                    else
                        return Won.DidntWin;
                case Mode.HorizontalLines:
                    if (Logics.CheckRow(arr, HORIONTAL_TOP))
                        return Won.HorizontalTop;
                    else if (Logics.CheckRow(arr, HORIONTAL_CENTER))
                        return Won.HorizontalCenter;
                    else if (Logics.CheckRow(arr, HORIONTAL_BOTTOM))
                        return Won.HorizontalBottom;
                    else
                        return Won.DidntWin;
                case Mode.VerticalLines:
                    if (Logics.CheckColumn(arr, VERTICAL_LEFT))
                        return Won.VerticalLinesLeft;
                    else if (Logics.CheckColumn(arr, VERTICAL_CENTER))
                        return Won.VerticalLinesCenter;
                    else if (Logics.CheckColumn(arr, VERTICAL_RIGHT))
                        return Won.VerticalLinesRight;
                    else
                        return Won.DidntWin;
                case Mode.Diagonals:
                    if (Logics.CheckMainDiagonal(arr))
                        return Won.DiagonalsDiagonal;
                    else if (Logics.CheckAntiDiagonal(arr))
                        return Won.DiagonalsAnti;
                    else
                        return Won.DidntWin;
                default:
                    return Won.DidntWin;
            }
        }

        internal static double BetMultiplier(double bet, Won won) => won switch
        {
            Won.CenterLine => bet * WIN_FACTOR,
            Won.HorizontalTop => bet * WIN_FACTOR / 3,
            Won.HorizontalCenter => bet * WIN_FACTOR / 3,
            Won.HorizontalBottom => bet * WIN_FACTOR / 3,
            Won.VerticalLinesLeft => bet * WIN_FACTOR / 3,
            Won.VerticalLinesCenter => bet * WIN_FACTOR / 3,
            Won.VerticalLinesRight => bet * WIN_FACTOR / 3,
            Won.DiagonalsDiagonal => bet * WIN_FACTOR / 2,
            Won.DiagonalsAnti => bet * WIN_FACTOR / 2,
            _ => 0,
        };

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
