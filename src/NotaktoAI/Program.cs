using System;
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
        int hashNum = int.Parse(args[1]);

        var game = new Game(fileName, hashNum, DEPTH);

        while (game.IsRunning())
        {
            Thread.Sleep(1000);
            game.Round();
        }

        Console.WriteLine("Game ended");
    }
}