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

        public GuessFeedbackBuilder WithBlack()
        {
            return With(1, KeyColor.Black);
        }

        public GuessFeedbackBuilder WithWhite()
        {
            return With(1, KeyColor.White);
        }

        public GuessFeedbackBuilder ThenBlacks(int count)
        {
            return With(count, KeyColor.Black);
        }

        public GuessFeedbackBuilder ThenWhites(int count)
        {
            return With(count, KeyColor.White);
        }

        public GuessFeedbackBuilder ThenNones(int count)
        {
            return With(count, KeyColor.None);
        }

        public GuessFeedbackBuilder ThenBlack()
        {
            return WithBlack();
        }

        public GuessFeedbackBuilder ThenWhite()
        {
            return WithWhite();
        }

        public GuessFeedback Build()
        {
            Require(colors.Count == Combination.PegsCount).True();

            return new GuessFeedback(colors);
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
        #endregion
    }
}