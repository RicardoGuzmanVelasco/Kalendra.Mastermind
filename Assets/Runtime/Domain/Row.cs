using System;
using static RGV.DesignByContract.Runtime.Precondition;

namespace Runtime.Domain
{
    internal class Row
    {
        Combination holesForCombination;
        GuessFeedback holesForFeedback;

        public bool HasCombination => holesForCombination != null;
        public bool IsCompleted => HasCombination && holesForFeedback != null;

        public bool CodeIsBroken => holesForFeedback.IsEndOfRound;

        public void PinCombinationPegs(Combination with)
        {
            Require<InvalidOperationException>(HasCombination).False();
            holesForCombination = with;
        }

        public void PinFeedbackPegs(GuessFeedback with)
        {
            Require<InvalidOperationException>(HasCombination).True();
            Require<InvalidOperationException>(IsCompleted).False();

            holesForFeedback = with;
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
                return holesForCombination?.ToString() ?? "xxxx";
            }

            return $"{FormatCombination()} | {FormatFeedback()}";
        }
        #endregion
    }
}