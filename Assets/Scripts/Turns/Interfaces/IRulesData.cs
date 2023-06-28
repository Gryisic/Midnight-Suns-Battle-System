namespace Turns.Interfaces
{
    public interface IRulesData
    {
        int CardPlays { get; }
        int Redraws { get; }
        int Moves { get; }
        int Heroism { get; }
    }
}