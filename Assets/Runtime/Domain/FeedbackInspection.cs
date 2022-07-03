namespace Runtime.Domain
{
    /// <param name="Row">1-BASED INDEX !!!</param>
    public record FeedbackInspection(int Row, GuessFeedback CorrectFeedback)
    {
        public FeedbackInspection() : this(0, null) { }

        public static FeedbackInspection NoWrong { get; } = new NoWrongFeedback();

        sealed record NoWrongFeedback : FeedbackInspection;
    }
}