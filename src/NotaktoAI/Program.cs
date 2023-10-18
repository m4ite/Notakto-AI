using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using NotaktoAI;

internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length != 2)
            throw new Exception();

        string fileName = args[0];
        string movesFiles = $"{fileName}.txt";
        string lastMoveFile = $"{fileName} last.txt";

        var sw = new Stopwatch();

        sw.Start();

        Tree tree = new(int.Parse(args[1]), 4);

        sw.Stop();

        Console.WriteLine("Elapsed={0}", sw.Elapsed);

        while (true)
        {
            if (!File.Exists(lastMoveFile))
                continue;

            var lastMove = GetLastMove(lastMoveFile);

            tree.Update(lastMove);

            var move = tree.Minimax();

            Thread.Sleep(1000);
        }
    }

    static Move GetLastMove(string file)
    {
        var line = File.ReadAllLines(file)[0].Split(' ');
        
        var board = int.Parse(line[0]);
        var space = int.Parse(line[1]);

        File.Delete(file);

        return new Move(board, space);
    }
}