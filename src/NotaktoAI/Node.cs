using System.Collections.Generic;
using System.Linq;

namespace NotaktoAI;

public class Node
{
    public readonly bool[][] Board;
    public readonly Move PrevMove;
    public readonly List<Node> Children = new();
    public float Value { get; private set; } = 0;

    public Node(bool[][] board)
        => Board = board;

    public Node(bool[][] board, Move move)
    {
        board[move.Board][move.Space] = true;
        Board = board;
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
        if (Hash.GameEnded(Board))
            return;

        for (int i = 0; i < Board.Length; i++)
        {
            if (!Hash.Check(Board[i]))
                continue;

            var hash = Board[i];
            for (int j = 0; j < hash.Length; j++)
            {
                var space = hash[j];
                if (!space)
                {
                    var node = new Node(Hash.CloneBoard(Board), new(i, j));
                    Children.Add(node);
                }
            }
        }
    }

    private float Heuristic(bool ImPlaying)
    {
        if (Hash.GameEnded(Board))
            return ImPlaying ? float.NegativeInfinity : float.PositiveInfinity;


        if (PrevMove.Space == 4)
            return 0.5f * (ImPlaying ? 1 : -1);

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