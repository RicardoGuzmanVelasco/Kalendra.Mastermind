using JetBrains.Annotations;
using Runtime.Domain;
using static RGV.DesignByContract.Runtime.Precondition;
using static Runtime.Domain.Board;
using static Tests.CombinationBuilder;

namespace Tests
{
    internal class BoardBuilder
    {
        CombinationBuilder secretCodeBuilder = Combination().AllRandom();
        int boardRows = DefaultRowsCount;

        public static BoardBuilder Board()
        {
            return new BoardBuilder();
        }

        public BoardBuilder WithoutSecretCode()
        {
            secretCodeBuilder = null;
            return this;
        }

        public BoardBuilder WithRows(int rows)
        {
            Require(rows).Positive();

            boardRows = rows;
            return this;
        }

        public BoardBuilder WithSecretCode([NotNull] CombinationBuilder secretCode)
        {
            secretCodeBuilder = secretCode;
            return this;
        }

        public Board Build()
        {
            return new Board(boardRows, secretCodeBuilder?.Build());
        }
    }
}