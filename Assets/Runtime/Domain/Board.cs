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

        public bool IsFull => RowOfCurrentRound == null;
        public bool IsSolved => RowOfLastRound?.CodeIsBroken ?? false;
        public bool IsGuessTurn => !IsFull && !RowOfCurrentRound!.HasCombination;
        public bool IsFeedbackTurn => !IsFull && RowOfCurrentRound!.HasCombination;

        public void AttemptGuess(Combination guess)
        {
            Require<InvalidOperationException>(IsSolved).False();

            RowOfCurrentRound!.PinGuessPegs(guess);
        }

        public void ResponseFeedback(GuessFeedback feedback)
        {
            Require<InvalidOperationException>(IsSolved).False();

            RowOfCurrentRound!.PinFeedbackPegs(feedback);
        }

        public FeedbackInspection ShowWrongFeedbackGiven()
        {
            return WrongFeedbackGiven()
                ? new FeedbackInspection()
                : FeedbackInspection.NoWrong;

            bool WrongFeedbackGiven()
            {
                foreach(var completedRow in CompletedRows)
                    if(completedRow.CollateWith(secretCode).FeedbackWasWrong)
                        return true;
                return false;
            }
        }

        #region Support methods
        [CanBeNull] Row RowOfLastRound => rows.LastOrDefault(r => r.IsCompleted);
        [CanBeNull] Row RowOfCurrentRound => rows.FirstOrDefault(r => !r.IsCompleted);
        IEnumerable<Row> CompletedRows => rows.Where(r => r.IsCompleted);
        #endregion
    }
}