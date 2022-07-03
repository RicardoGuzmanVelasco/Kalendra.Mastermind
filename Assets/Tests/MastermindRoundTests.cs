using FluentAssertions;
using NUnit.Framework;
using static Tests.GuessFeedbackBuilder;
using static Tests.BoardBuilder;
using static Tests.CombinationBuilder;

namespace Tests
{
    public class MastermindRoundTests
    {
        [Test]
        public void Feedback_Knows_IfEndsTheRound()
        {
            Feedback().AllBlacks().Build()
                .IsEndOfRound
                .Should().BeTrue();

            Feedback().AllEmpty().Build()
                .IsEndOfRound
                .Should().BeFalse();
            Feedback().AllWhites().Build()
                .IsEndOfRound
                .Should().BeFalse();

            Feedback().WithBlacks(3).WithEmpty(1).Build()
                .IsEndOfRound
                .Should().BeFalse();
            Feedback().WithBlacks(3).WithWhites(1).Build()
                .IsEndOfRound
                .Should().BeFalse();
        }

        [Test]
        public void Board_Starts_WaitingForGuess()
        {
            Board().Build()
                .IsWaitingForGuess
                .Should().BeTrue();
        }

        [Test]
        public void JustAfterReceiveGuess_BoardWaitsNotForGuess()
        {
            var sut = Board().Build();

            sut.AttemptGuess(Combination().AllRandom().Build());

            sut.IsWaitingForGuess.Should().BeFalse();
        }

        [Test]
        public void JustAfterReceiveFeedbackOfGuess_BoardAwaitsAgainForGuess()
        {
            var sut = Board().Build();
            sut.AttemptGuess(Combination().AllRandom().Build());

            sut.ResponseFeedback(Feedback().AllEmpty().Build());

            sut.IsWaitingForGuess.Should().BeTrue();
        }
    }
}