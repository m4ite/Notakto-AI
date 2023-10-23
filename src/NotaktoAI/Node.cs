using System.Collections.Generic;
using System.Linq;

namespace NotaktoAI;

public class Node
{
    public readonly bool[][] GameBoard;
    public readonly Move PrevMove;
    public readonly List<Node> Children = new();
    public float Value { get; private set; } = 0;

    public Node(bool[][] board)
        => GameBoard = board;

    public Node(bool[][] board, Move move)
    {
        board[move.Board][move.Space] = true;
        GameBoard = board;
        PrevMove = move;
    }

    public List<Node> GetChildren()
    {
        if (Children.Count == 0)
            return new List<Node>() { this };

        return Children.SelectMany(c => c.GetChildren()).ToList();
    }

    public void GenChildren()
    {
        if (!Board.IsValid(GameBoard))
            return;

        for (int i = 0; i < GameBoard.Length; i++)
        {
            if (!Hash.IsValid(GameBoard[i]))
                continue;

            var hash = GameBoard[i];
            for (int j = 0; j < hash.Length; j++)
            {
                var space = hash[j];
                if (!space)
                {
                    var node = new Node(Board.Clone(GameBoard), new(i, j));
                    Children.Add(node);
                }
            }
        }
    }

    private float Heuristic(bool ImPlaying)
    {
        if (!Board.IsValid(GameBoard))
            return ImPlaying ? float.NegativeInfinity : float.PositiveInfinity;


        if (PrevMove.Space == 4)
            return ImPlaying ? -1 : 1;

        return 0f;
    }

    public (Move Move, float Value) Minimax(bool maximize, int depth)
    {
        if (Children.Count == 0 || depth == 0)
            return (PrevMove, Heuristic(maximize));

        var minimax = Children.Select(c => c.Minimax(!maximize, depth - 1));

        (Move Move, float Value) res;

        if (maximize)
            res = minimax.MaxBy(r => r.Value);
        else
            res = minimax.MinBy(r => r.Value);

        Value = res.Value;

        return res;
    }
}