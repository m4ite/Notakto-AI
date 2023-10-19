using System;
using System.Threading;
using NotaktoAI;

internal class Program
{
    const int DEPTH = 10;

    private static void Main(string[] args)
    {
        if (args.Length != 2)
            throw new Exception("Invalid arguments");

        string fileName = args[0];
        int hashNum = int.Parse(args[1]);

        Game game = new(fileName, hashNum, DEPTH);

        while (game.IsRunning())
        {
            Thread.Sleep(1000);
            game.Round();
        }

        Console.WriteLine("Game ended");
    }
}