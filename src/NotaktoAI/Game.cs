using System.IO;
using System.Linq;

namespace NotaktoAI;

public class Game
{
    private readonly string file;
    private readonly string enemyFile;
    private readonly Tree tree;
    private bool myTurn;

    public Game(string player, int hashNum, int depth)
    {
        myTurn = player == "M1";
        file = player + ".txt";
        enemyFile = player + " last.txt";
        tree = new(hashNum, depth);
    }

    public void Round()
    {
        if (myTurn)
            Play();
        else
            EnemyPlay();
    }

    public bool IsRunning()
        => Hash.GameEnded(tree.Root.Board);

    public void Play()
    {
        var move = tree.Minimax();

        using (StreamWriter sw = File.CreateText(file))
            sw.WriteLine(move.ToString());

        tree.Update(move);

        myTurn = false;
    }

    public void EnemyPlay()
    {
        if (!File.Exists(enemyFile))
            return;

        var move = GetLastMove(enemyFile);

        tree.Update(move);

        myTurn = true;
    }

    private static Move GetLastMove(string file)
    {
        var line = File
            .ReadAllText(file)
            .Split(' ');

        File.Delete(file);

        var board = int.Parse(line[0]);
        var space = int.Parse(line[1]);

        return new Move(board, space);
    }
}