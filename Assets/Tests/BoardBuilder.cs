using JetBrains.Annotations;
using Runtime.Domain;
using static Tests.CombinationBuilder;

namespace Tests
{
    internal class BoardBuilder
    {
        CombinationBuilder secretCodeBuilder = Combination().AllRandom();

        public static BoardBuilder Board()
        {
            return new BoardBuilder();
        }

        public BoardBuilder WithSecretCode([NotNull] CombinationBuilder secretCode)
        {
            secretCodeBuilder = secretCode;
            return this;
        }

        public Board Build()
        {
            return new Board(secretCodeBuilder.Build());
        }
    }
}