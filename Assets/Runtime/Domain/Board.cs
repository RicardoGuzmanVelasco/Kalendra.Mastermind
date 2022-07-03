using System.Collections.Generic;
using System.Linq;

namespace Runtime.Domain
{
    public class Board
    {
        const int RowsCount = 10;

        readonly Combination secretCode;
        readonly List<Row> rows;

        public Board(Combination secretCode)
        {
            this.secretCode = secretCode;

            rows = new List<Row>();
            for(var i = 0; i < RowsCount; i++)
                rows.Add(new Row());
        }

        Row RowOfCurrentRound => rows.FirstOrDefault(r => !r.IsCompleted) ?? rows.Last();

        public bool IsWaitingForGuess => !RowOfCurrentRound.HasCombination;

        public void AttemptGuess(Combination guess)
        {
            RowOfCurrentRound.PinCombinationPegs(guess);
        }

        public void ResponseFeedback(GuessFeedback feedback)
        {
            RowOfCurrentRound.PinFeedbackPegs(feedback);
        }
    }
}