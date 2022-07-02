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

        #region Object mothers
        public static GuessFeedback AllBlacks()
        {
            return Feedback().AllOf(KeyColor.Black).Build();
        }

        public static GuessFeedback AllWhites()
        {
            return Feedback().AllOf(KeyColor.White).Build();
        }

        public static GuessFeedback AllEmpty()
        {
            return Feedback().AllOf(KeyColor.None).Build();
        }
        #endregion

        public GuessFeedbackBuilder WithNone() => With(1, KeyColor.None);
        public GuessFeedbackBuilder WithBlack() => With(1, KeyColor.Black);
        public GuessFeedbackBuilder WithWhite() => With(1, KeyColor.White);

        public GuessFeedbackBuilder ThenBlacks(int count) => With(count, KeyColor.Black);
        public GuessFeedbackBuilder ThenWhites(int count) => With(count, KeyColor.White);
        public GuessFeedbackBuilder ThenNones(int count) => With(count, KeyColor.None);

        public GuessFeedbackBuilder ThenNone() => WithNone();
        public GuessFeedbackBuilder ThenWhite() => WithWhite();
        public GuessFeedbackBuilder ThenBlack() => WithBlack();

        #region Support methods
        GuessFeedbackBuilder AllOf(KeyColor color)
        {
            Require(colors.Any()).False();

            return With(Combination.PegsCount, color);
        }

        GuessFeedbackBuilder With(int count, KeyColor color)
        {
            Require(colors.Count + count).Not.GreaterThan(Combination.PegsCount);

            colors.AddRange(Enumerable.Repeat(color, count));
            return this;
        }
        #endregion

        public GuessFeedback Build()
        {
            Require(colors.Count == Combination.PegsCount).True();

            return new GuessFeedback(colors);
        }
    }
}