using System;
using FluentAssertions;
using NUnit.Framework;
using static Tests.GuessFeedbackBuilder;
using static Tests.BoardBuilder;
using static Tests.CombinationBuilder;

namespace Tests
{
    public class BoardRoundTests
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
        public void Board_WithoutSecretCode_CannotOperate()
        {
            var sut = Board().WithoutSecretCode().Build();

            Action act = () => sut.PinGuessPegs(Combination().AllRandom().Build());
            act.Should().Throw<InvalidOperationException>();

            sut.PinSecretCodePegs(Combination().AllRandom().Build());
            act.Should().NotThrow();
        }

        [Test]
        public void Cannot_PlaceSecretCode_Twice()
        {
            var sut = Board().WithoutSecretCode().Build();

            Action act = () => sut.PinSecretCodePegs(Combination().AllRandom().Build());
            act.Invoke();

            act.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void IsNotGuessTurn_UntilSecretCodeIsPlaced()
        {
            var sut = Board().WithoutSecretCode().Build();

            sut.IsGuessTurn.Should().BeFalse();
            sut.PinSecretCodePegs(Combination().AllRandom().Build());
            sut.IsGuessTurn.Should().BeTrue();
        }

        [Test]
        public void BoardWithSecretCode_Starts_WaitingForGuess()
        {
            Board().Build()
                .IsGuessTurn
                .Should().BeTrue();
            Board().Build()
                .IsFeedbackTurn
                .Should().BeFalse();
        }

        [Test]
        public void JustAfterReceiveGuess_BoardWaitsNotForGuess()
        {
            var sut = Board().Build();

            sut.PinGuessPegs(Combination().AllRandom().Build());

            sut.IsGuessTurn.Should().BeFalse();
            sut.IsFeedbackTurn.Should().BeTrue();
        }

        [Test]
        public void JustAfterReceiveFeedbackOfGuess_BoardAwaitsAgainForGuess()
        {
            var sut = Board().Build();
            sut.PinGuessPegs(Combination().AllRandom().Build());

            sut.PinFeedbackPegs(Feedback().AllEmpty().Build());

            sut.IsGuessTurn.Should().BeTrue();
            sut.IsFeedbackTurn.Should().BeFalse();
        }

        [Test]
        public void Cannot_AttemptGuess_TwiceInARow()
        {
            var sut = Board().Build();
            sut.PinGuessPegs(Combination().AllRandom().Build());

            Action act = () => sut.PinGuessPegs(Combination().AllRandom().Build());

            act.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Cannot_ResponseFeedback_TwiceInARow()
        {
            var sut = Board().Build();
            sut.PinGuessPegs(Combination().AllRandom().Build());
            sut.PinFeedbackPegs(Feedback().AllEmpty().Build());

            Action act = () => sut.PinFeedbackPegs(Feedback().AllEmpty().Build());

            act.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void IsSolved_WhenLastFeedbackEndsTheRound()
        {
            var sut = Board().Build();
            sut.PinGuessPegs(Combination().AllRandom().Build());
            sut.PinFeedbackPegs(Feedback().AllBlacks().Build());

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
            sut.PinGuessPegs(Combination().AllRandom().Build());
            sut.PinFeedbackPegs(Feedback().WithBlacks(3).WithEmpty(1).Build());

            sut.IsSolved.Should().BeFalse();
        }

        [Test]
        public void Cannot_ContinueGame_IfBoardIsSolved()
        {
            var sut = Board().Build();
            sut.PinGuessPegs(Combination().AllRandom().Build());
            sut.PinFeedbackPegs(Feedback().AllBlacks().Build());

            Action act = () => sut.PinGuessPegs(Combination().AllRandom().Build());

            act.Should().Throw<InvalidOperationException>();
        }

        [Test]
        public void Board_Full_AfterAllRowsAreCompleted()
        {
            var sut = Board().WithRows(1).Build();
            sut.PinGuessPegs(Combination().AllRandom().Build());
            sut.PinFeedbackPegs(Feedback().WithBlacks(2).WithWhites(2).Build());

            sut.IsFull.Should().BeTrue();
        }

        [Test]
        public void Board_IsNotFull_ByDefault()
        {
            Board().Build().IsFull.Should().BeFalse();
        }

        [Test]
        public void Board_IsNotFull_AfterGuess()
        {
            var sut = Board().Build();
            sut.PinGuessPegs(Combination().AllRandom().Build());

            sut.IsFull.Should().BeFalse();
        }

        [Test]
        public void AfterClearBoard_aNewSecretCode_MustBePinnedAgain()
        {
            var sut = Board().Build();
            sut.PinGuessPegs(Combination().AllRandom().Build());
            sut.PinFeedbackPegs(Feedback().AllBlacks().Build());

            sut.Clear();

            sut.IsGuessTurn.Should().BeFalse();
            sut.PinSecretCodePegs(Combination().AllRandom().Build());
            sut.IsGuessTurn.Should().BeTrue();
        }

        [Test]
        public void Board_CanBeCleared_IfIsSolved()
        {
            var sut = Board().Build();
            sut.PinGuessPegs(Combination().AllRandom().Build());
            sut.PinFeedbackPegs(Feedback().AllBlacks().Build());

            sut.IsSolved.Should().BeTrue();
            sut.Clear();
            sut.IsSolved.Should().BeFalse();
        }

        [Test]
        public void Board_CanBeCleared_IfIsFull()
        {
            var sut = Board().WithRows(1).Build();
            sut.PinGuessPegs(Combination().AllRandom().Build());
            sut.PinFeedbackPegs(Feedback().WithBlacks(2).WithWhites(2).Build());

            sut.IsFull.Should().BeTrue();
            sut.Clear();
            sut.IsFull.Should().BeFalse();
        }

        [Test]
        public void Board_CannotBeCleared_IfIsStillInPlay()
        {
            var sut = Board().Build();

            Action act = () => sut.Clear();
            act.Should().Throw<InvalidOperationException>();
        }
    }
}