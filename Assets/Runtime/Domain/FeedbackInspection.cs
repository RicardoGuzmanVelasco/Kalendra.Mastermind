namespace Runtime.Domain
{
    public class FeedbackInspection
    {
        public static FeedbackInspection NoWrong { get; } = new NoWrongFeedback();

        sealed class NoWrongFeedback : FeedbackInspection { }
    }
}