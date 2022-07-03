using System;
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
            Board().Build()
                .IsWaitingForFeedback
                .Should().BeFalse();
        }

        [Test]
        public void JustAfterReceiveGuess_BoardWaitsNotForGuess()
        {
            var sut = Board().Build();

            sut.AttemptGuess(Combination().AllRandom().Build());

            sut.IsWaitingForGuess.Should().BeFalse();
            sut.IsWaitingForFeedback.Should().BeTrue();
        }

        [Test]
        public void JustAfterReceiveFeedbackOfGuess_BoardAwaitsAgainForGuess()
        {
            var sut = Board().Build();
            sut.AttemptGuess(Combination().AllRandom().Build());

            sut.ResponseFeedback(Feedback().AllEmpty().Build());

            sut.IsWaitingForGuess.Should().BeTrue();
            sut.IsWaitingForFeedback.Should().BeFalse();
        }

        [Test]
        public void Cannot_AttemptGuess_TwiceInARow()
        {
            var sut = Board().Build();
            sut.AttemptGuess(Combination().AllRandom().Build());

            Action act = () => sut.AttemptGuess(Combination().AllRandom().Build());

            act.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Cannot_ResponseFeedback_TwiceInARow()
        {
            var sut = Board().Build();
            sut.AttemptGuess(Combination().AllRandom().Build());
            sut.ResponseFeedback(Feedback().AllEmpty().Build());

            Action act = () => sut.ResponseFeedback(Feedback().AllEmpty().Build());

            act.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void IsSolved_WhenLastFeedbackEndsTheRound()
        {
            var sut = Board().Build();
            sut.AttemptGuess(Combination().AllRandom().Build());
            sut.ResponseFeedback(Feedback().AllBlacks().Build());

            sut.IsSolved.Should().BeTrue();
        }

        [Test]
        public void NotSolved_ByDefault()
        {
            Board().Build().IsSolved.Should().BeFalse();
        }

        [Test]
        public void NotSolved_BeforeFeedbackOfBrokenCode()
        {
            var sut = Board().Build();
            sut.AttemptGuess(Combination().AllRandom().Build());
            sut.ResponseFeedback(Feedback().WithBlacks(3).WithEmpty(1).Build());

            sut.IsSolved.Should().BeFalse();
        }

        [Test]
        public void Cannot_ContinueGame_IfBoardIsSolved()
        {
            var sut = Board().Build();
            sut.AttemptGuess(Combination().AllRandom().Build());
            sut.ResponseFeedback(Feedback().AllBlacks().Build());

            Action act = () => sut.AttemptGuess(Combination().AllRandom().Build());

            act.Should().Throw<InvalidOperationException>();
        }
    }
}