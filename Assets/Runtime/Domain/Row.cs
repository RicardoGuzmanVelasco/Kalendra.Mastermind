using static RGV.DesignByContract.Runtime.Precondition;

namespace Runtime.Domain
{
    internal class Row
    {
        Combination holesForCombination;
        GuessFeedback holesForFeedback;

        public bool HasCombination => holesForCombination != null;
        public bool IsCompleted => HasCombination && holesForFeedback != null;

        public void PinCombinationPegs(Combination with)
        {
            Require(HasCombination).False();
            holesForCombination = with;
        }

        public void PinFeedbackPegs(GuessFeedback with)
        {
            Require(HasCombination).True();
            Require(IsCompleted).False();

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