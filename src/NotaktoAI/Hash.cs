using System;

namespace NotaktoAI;

internal static class Hash
{
    private const int MATRIX_ORDER = 3;
    public const int HASH_LENGTH = 9;

    public static bool IsValid(bool[] hash)
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

        foreach (var point in points)
        {
            if (point > 2)
                return false;
        }

        return true;
    }
}