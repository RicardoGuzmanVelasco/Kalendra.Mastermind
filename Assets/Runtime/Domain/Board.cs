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

        readonly List<Row> rows;
        Combination secretCode;

        #region Ctors
        public Board(int rows, [CanBeNull] Combination secretCode)
        {
            Require(rows > 0).True();

            this.secretCode = secretCode;

            this.rows = new List<Row>(rows);
            for(var i = 0; i < rows; i++)
                this.rows.Add(new Row());
        }
        #endregion

        public void PinSecretCode(Combination code)
        {
            Require<InvalidOperationException>(secretCode).Null();
            secretCode = code;
        }

        public bool IsFull => RowOfCurrentRound == null;
        public bool IsSolved => RowOfLastRound?.CodeIsBroken ?? false;

        public bool IsGuessTurn => secretCode is not null &&
                                   !IsFull &&
                                   !RowOfCurrentRound!.HasCombination;

        public bool IsFeedbackTurn => !IsFull &&
                                      RowOfCurrentRound!.HasCombination;

        public void PinGuessPegs(Combination guess)
        {
            Require<InvalidOperationException>(secretCode).Not.Null();
            Require<InvalidOperationException>(IsSolved).False();

            RowOfCurrentRound!.PinGuessPegs(guess);
        }

        public void PinFeedbackPegs(GuessFeedback feedback)
        {
            Require<InvalidOperationException>(secretCode).Not.Null();
            Require<InvalidOperationException>(IsSolved).False();

            RowOfCurrentRound!.PinFeedbackPegs(feedback);
        }

        public FeedbackInspection InspectWrongFeedbackGiven()
        {
            Require<InvalidOperationException>(secretCode).Not.Null();

            foreach(var row in CompletedRows)
            {
                var comparation = row.CollateWith(secretCode);
                if(comparation.FeedbackWasWrong)
                    return new FeedbackInspection
                    {
                        Row = rows.IndexOf(row) + 1,
                        CorrectFeedback = comparation.FeedbackExpected
                    };
            }

            return FeedbackInspection.NoWrong;
        }

        #region Support methods
        [CanBeNull] Row RowOfLastRound => rows.LastOrDefault(r => r.IsCompleted);
        [CanBeNull] Row RowOfCurrentRound => rows.FirstOrDefault(r => !r.IsCompleted);
        IEnumerable<Row> CompletedRows => rows.Where(r => r.IsCompleted);
        #endregion
    }
}