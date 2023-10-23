namespace NotaktoAI;

internal static class Board
{
    public static bool[][] Generate(int hashNum)
    {
        var board = new bool[hashNum][];
        for (int i = 0; i < hashNum; i++)
            board[i] = new bool[Hash.HASH_LENGTH];

        return board;
    }

    public static bool IsValid(bool[][] board)
    {
        foreach (var hash in board)
        {
            if (Hash.IsValid(hash))
                return true;
        }

        return false;
    }

    public static bool[][] Clone(bool[][] board)
    {
        var newBoard = new bool[board.Length][];

        for (int i = 0; i < board.Length; i++)
            newBoard[i] = (bool[])board[i].Clone();

        return newBoard;
    }
}