using System;
using System.IO;
using System.Linq;
using NotaktoAI;

internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length != 2)
            throw new Exception();

        string fileName = args[0] + ".txt";
        string[] lines = File.ReadAllLines(fileName);
        // ADD NEW LINE 
        // lines = lines.Append("tabuleiro posição").ToArray();





        int boardsNum = int.Parse(args[1]);

        var gameBoards = Enumerable
            .Range(0, boardsNum)
            .Select(_ => new bool[9])
            .ToArray();

        gameBoards[0][0] = true;
        gameBoards[0][1] = true;
        gameBoards[0][2] = true;

        // foreach (var board in gameBoards)
        //     Console.WriteLine($"Is valid board {Hash.Check(board)}");
    }
}