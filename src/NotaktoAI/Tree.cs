using System.Collections.Generic;
using System.Linq;

namespace NotaktoAI;

public class Tree
{
    public Node Root { get; private set; }
    private readonly int Depth;

    public Tree(int hashNum, int depth)
    {
        Root = new(Hash.GenBoard(hashNum));
        Depth = depth;

        IEnumerable<Node> children = new Node[] { Root };

        for (int i = 0; i < depth; i++)
        {
            foreach (var child in children)
                child.GenChildren();

            children = children.SelectMany(c => c.Children);
        }
    }

    public Move Minimax()
        => Root.Minimax(true, Depth).Move;

    public void Update(Move move)
    {
        Root = Root.Children.First(c => c.PrevMove == move);
        Root.GetChildren().ForEach(c => c.GenChildren());
    }
}