namespace NotaktoAI;

public record struct Move(int Board, int Space)
{
    public override readonly string ToString()
        => $"{Board} {Space}";
}

