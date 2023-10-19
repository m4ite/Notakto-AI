using System;
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

        Tree tree = new(int.Parse(args[1]), 4);

        if (fileName == "M1")
            Play();

        while (true)
        {
            if (!File.Exists(lastMoveFile))
                continue;

            var lastMove = GetLastMove(lastMoveFile);

            tree.Update(lastMove);

            Play();

            Thread.Sleep(1000);
        }

        void Play()
        {
            var move = tree.Minimax();

            using (StreamWriter sw = File.AppendText(movesFiles))
                sw.WriteLine(move.ToString());
            
            tree.Update(move);
        }
    }
    
    private static Move GetLastMove(string file)
    {
        var line = File.ReadAllLines(file)[0].Split(' ');

        var board = int.Parse(line[0]);
        var space = int.Parse(line[1]);

        File.Delete(file);

        return new Move(board, space);
    }
}