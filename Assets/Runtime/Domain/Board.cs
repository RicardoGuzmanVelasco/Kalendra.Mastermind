using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Precondition;

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

        [CanBeNull] Row RowOfLastRound => rows.LastOrDefault(r => r.IsCompleted);
        [CanBeNull] Row RowOfCurrentRound => rows.FirstOrDefault(r => !r.IsCompleted);

        public bool IsSolved => RowOfLastRound?.CodeIsBroken ?? false;
        public bool IsGuessTurn => !RowOfCurrentRound?.HasCombination ?? false;
        public bool IsFeedbackTurn => RowOfCurrentRound?.HasCombination ?? false;

        public void AttemptGuess(Combination guess)
        {
            Require<InvalidOperationException>(IsSolved).False();

            RowOfCurrentRound!.PinCombinationPegs(guess);
        }

        public void ResponseFeedback(GuessFeedback feedback)
        {
            Require<InvalidOperationException>(IsSolved).False();

            RowOfCurrentRound!.PinFeedbackPegs(feedback);
        }
    }
}