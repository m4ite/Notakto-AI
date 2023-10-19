using System;
using System.Linq;

namespace NotaktoAI;

public static class Hash
{
    private const int BOARD_LENGTH = 9;
    private const int MATRIX_ORDER = 3;

    public static bool Check(bool[] board)
    {
        if (board.Length != BOARD_LENGTH)
            throw new Exception("Invalid board for a hash game");

        var points = new int[8];

        for (int i = 0; i < BOARD_LENGTH; i++)
        {
            int row = i / MATRIX_ORDER;
            int col = i % MATRIX_ORDER;

            if (!board[i])
                continue;

            if (row == col)
                points[0]++;

            if (row + col == MATRIX_ORDER - 1)
                points[1]++;

            points[col + 2]++;
            points[row + 5]++;
        }

        return !points.Any(p => p == 3);
    }
}