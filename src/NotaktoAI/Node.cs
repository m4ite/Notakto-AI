using System.Collections.Generic;
using System.Linq;
using System;

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

    public void GenChildren()
    {
        for (int i = 0; i < Board.Length; i++)
        {
            if (!Hash.Check(Board[i]))
                continue;

            for (int j = 0; j < Board[i].Length; j++)
            {
                if (!Board[i][j])
                {
                    var node = new Node(CloneBoard(Board), new(i, j), this);
                    Children.Add(node);
                }
            }
        }
    }

    float Heuristic(bool ImPlaying)
    {
        if (Board.All(hash => !Hash.Check(hash)))
        {
            Console.WriteLine("entrou");
            return ImPlaying ? float.NegativeInfinity : float.PositiveInfinity;
        }

        return 0f;
    }

    public float Minimax(bool maximize, int depth)
    {
        if (Children.Count == 0 || depth == 0)
            return Heuristic(maximize);

        Value = maximize ? float.NegativeInfinity : float.PositiveInfinity;
        Func<float, float, float> minimax = maximize ? float.Max : float.Min;

        foreach (var child in Children)
            Value = minimax(Value, child.Minimax(!maximize, depth - 1));

        return Value;
    }

    static bool[][] CloneBoard(bool[][] board)
    {
        var newBoard = new bool[board.Length][];

        for (int i = 0; i < board.Length; i++)
            newBoard[i] = (bool[])board[i].Clone();

        return newBoard;
    }
}