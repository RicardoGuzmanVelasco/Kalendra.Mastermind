using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Precondition;

namespace Runtime.Domain
{
    public class Board
    {
        public const int DefaultRowsCount = 10;

        readonly Combination secretCode;
        readonly List<Row> rows;

        #region Ctors
        public Board(Combination secretCode) : this(secretCode, DefaultRowsCount) { }

        public Board(Combination secretCode, int rows)
        {
            this.secretCode = secretCode;

            this.rows = new List<Row>();
            for(var i = 0; i < rows; i++)
                this.rows.Add(new Row());
        }
        #endregion

        [CanBeNull] Row RowOfLastRound => rows.LastOrDefault(r => r.IsCompleted);
        [CanBeNull] Row RowOfCurrentRound => rows.FirstOrDefault(r => !r.IsCompleted);

        public bool IsFull => RowOfCurrentRound == null;
        public bool IsSolved => RowOfLastRound?.CodeIsBroken ?? false;
        public bool IsGuessTurn => !IsFull && !RowOfCurrentRound!.HasCombination;
        public bool IsFeedbackTurn => !IsFull && RowOfCurrentRound!.HasCombination;

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