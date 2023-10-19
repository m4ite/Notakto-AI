using System;
using System.IO;
using System.Linq;
using System.Threading;
using NotaktoAI;

internal class Program
{
    const int DEPTH = 4;

    private static void Main(string[] args)
    {
        if (args.Length != 2)
            throw new Exception("Invalid arguments");


        string fileName = args[0];
        string movesFiles = $"{fileName}.txt";
        string lastMoveFile = $"{fileName} last.txt";

        int boardsNum = int.Parse(args[1]);
        int depth = args.GetValue(3) is null ? DEPTH : int.Parse(args[3]);

        Tree tree = new(boardsNum, depth);

        if (fileName == "M1")
            Play();

        while (true)
        {
            Thread.Sleep(1000);

            if (!File.Exists(lastMoveFile))
                continue;

            var lastMove = GetLastMove(lastMoveFile);

            tree.Update(lastMove);

            Play();
        }

        void Play()
        {
            var move = tree.Minimax();

            using (StreamWriter sw = File.CreateText(movesFiles))
                sw.WriteLine(move.ToString());

            tree.Update(move);
        }
    }

    private static Move GetLastMove(string file)
    {
        var line = File
            .ReadAllText(file)
            .Split(' ')
            .Select(int.Parse)
            .ToArray();

        File.Delete(file);

        var board = line[0];
        var space = line[1];

        return new Move(board, space);
    }
}