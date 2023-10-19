using System.Collections.Generic;
using System.Linq;

namespace NotaktoAI;

public class Node
{
    public readonly bool[][] Board;
    public Move PrevMove { get; private set; }
    public float Value { get; private set; } = 0;
    public List<Node> Children { get; private set; } = new();
    public Node? Father { get; private set; }

    public Node(bool[][] board)
        => Board = board;

    public Node(bool[][] board, Move move, Node father)
    {
        board[move.Board][move.Space] = true;
        Board = board;
        PrevMove = move;
        Father = father;
    }

    public List<Node> GetChildren()
    {
        if (Children.Count == 0)
            return Children;

        return Children.SelectMany(c => c.GetChildren()).ToList();
    }

    public void GenChildren()
    {
        if (Board.All(hash => !Hash.Check(hash)))
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
                    var node = new Node(CloneBoard(Board), new(i, j), this);
                    Children.Add(node);
                }
            }
        }
    }

    private float Heuristic(bool ImPlaying)
    {
        if (Board.All(hash => !Hash.Check(hash)))
            return ImPlaying ? float.NegativeInfinity : float.PositiveInfinity;

        return 0f;
    }

    public (Move Move, float Value) Minimax(bool maximize, int depth)
    {
        if (Children.Count == 0 || depth == 0)
            return (PrevMove, Heuristic(maximize));

        Move move = PrevMove;

        if (maximize)
        {
            Value = float.NegativeInfinity;

            foreach (var child in Children)
            {
                var res = child.Minimax(false, depth - 1);
                if (Value < res.Value)
                {
                    Value = res.Value;
                    move = res.Move;
                }
            }
        }
        else
        {
            Value = float.PositiveInfinity;

            foreach (var child in Children)
            {
                var res = child.Minimax(true, depth - 1);
                if (Value > res.Value)
                {
                    Value = res.Value;
                    move = res.Move;
                }
            }
        }

        return (move, Value);
    }

    private static bool[][] CloneBoard(bool[][] board)
    {
        var newBoard = new bool[board.Length][];

        for (int i = 0; i < board.Length; i++)
            newBoard[i] = (bool[])board[i].Clone();

        return newBoard;
    }
}