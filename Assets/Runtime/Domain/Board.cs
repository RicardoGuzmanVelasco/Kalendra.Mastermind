namespace Runtime.Domain
{
    public class Board
    {
        readonly Combination secretCode;

        public Board(Combination secretCode)
        {
            this.secretCode = secretCode;
        }

        public bool IsWaitingForGuess { get; } = true;
    }
}