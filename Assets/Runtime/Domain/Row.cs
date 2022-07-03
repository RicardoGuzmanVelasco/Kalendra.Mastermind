using System;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Precondition;

namespace Runtime.Domain
{
    internal class Row
    {
        Combination holesForGuess;
        GuessFeedback holesForFeedback;

        public bool HasCombination => holesForGuess != null;
        public bool IsCompleted => HasCombination && holesForFeedback != null;

        public bool CodeIsBroken => holesForFeedback.IsEndOfRound;

        public void PinGuessPegs(Combination with)
        {
            Require<InvalidOperationException>(HasCombination).False();
            holesForGuess = with;
        }

        public void PinFeedbackPegs(GuessFeedback with)
        {
            Require<InvalidOperationException>(HasCombination).True();
            Require<InvalidOperationException>(IsCompleted).False();

            holesForFeedback = with;
        }

        [NotNull]
        public Memento CollateWith(Combination code)
        {
            Require(IsCompleted).True();

            var correctFeedback = code.MatchWith(holesForGuess);

            return new Memento
            {
                GuessAttempted = holesForGuess,
                FeedbackProvided = holesForFeedback,
                FeedbackExpected = correctFeedback
            };
        }

        #region Formatting
        public override string ToString()
        {
            string FormatFeedback()
            {
                return holesForFeedback?.ToString() ?? "----";
            }

            string FormatCombination()
            {
                return holesForGuess?.ToString() ?? "xxxx";
            }

            return $"{FormatCombination()} | {FormatFeedback()}";
        }
        #endregion

        #region Nested
        internal record Memento
        (
            Combination GuessAttempted,
            GuessFeedback FeedbackProvided,
            GuessFeedback FeedbackExpected
        )
        {
            public Memento() : this(null, null, null) { }

            public bool FeedbackWasWrong => !FeedbackProvided.Equals(FeedbackExpected);
        }
        #endregion
    }
}