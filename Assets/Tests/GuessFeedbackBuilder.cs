using System.Collections.Generic;
using System.Linq;
using Runtime.Domain;
using static RGV.DesignByContract.Runtime.Precondition;

namespace Tests
{
    internal class GuessFeedbackBuilder
    {
        readonly List<KeyColor> colors = new List<KeyColor>();

        public static GuessFeedbackBuilder Feedback()
        {
            return new GuessFeedbackBuilder();
        }

        public GuessFeedbackBuilder AllBlacks() => WithBlacks(4);
        public GuessFeedbackBuilder AllWhites() => WithWhites(4);
        public GuessFeedbackBuilder AllEmpty() => WithEmpty(4);

        public GuessFeedbackBuilder WithBlacks(int count) => With(count, KeyColor.Black);
        public GuessFeedbackBuilder WithWhites(int count) => With(count, KeyColor.White);
        public GuessFeedbackBuilder WithEmpty(int count) => With(count, KeyColor.Empty);

        GuessFeedbackBuilder With(int count, KeyColor color)
        {
            Require(colors.Count + count).Not.GreaterThan(Combination.PegsCount);

            colors.AddRange(Enumerable.Repeat(color, count));
            return this;
        }

        public GuessFeedback Build()
        {
            Require(colors.Count == Combination.PegsCount).True();

            return new GuessFeedback(colors);
        }
    }
}