using System;
using System.Linq;

namespace NotaktoAI;

public static class Hash
{
    private const int HASH_LENGTH = 9;
    private const int MATRIX_ORDER = 3;

    public static bool[][] GenBoard(int hashNum)
    {
        var board = new bool[hashNum][];
        for (int i = 0; i < hashNum; i++)
            board[i] = new bool[HASH_LENGTH];

        return board;
    }

    public static bool Check(bool[] hash)
    {
        if (hash.Length != HASH_LENGTH)
            throw new Exception("Invalid length for a hash game");

        var points = new int[8];

        for (int i = 0; i < HASH_LENGTH; i++)
        {
            int row = i / MATRIX_ORDER;
            int col = i % MATRIX_ORDER;

            if (!hash[i])
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

    // TODO: Improving
    public static bool GameEnded(bool[][] board)
        => board.All(hash => !Check(hash));

    public static bool[][] CloneBoard(bool[][] board)
    {
        var newBoard = new bool[board.Length][];

        for (int i = 0; i < board.Length; i++)
            newBoard[i] = (bool[])board[i].Clone();

        return newBoard;
    }
}