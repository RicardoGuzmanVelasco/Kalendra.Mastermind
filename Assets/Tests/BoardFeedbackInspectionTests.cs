using FluentAssertions;
using NUnit.Framework;
using Runtime.Domain;
using static Tests.BoardBuilder;
using static Tests.CombinationBuilder;
using static Tests.GuessFeedbackBuilder;

namespace Tests
{
    public class BoardFeedbackInspectionTests
    {
        [Test]
        public void CannotResultIn_NoWrongFeedback_IfWrongFeedbackWasGiven()
        {
            var sut = Board().WithSecretCode(Combination().AllOf(CodeColor.Red)).Build();
            sut.AttemptGuess(Combination().AllOf(CodeColor.Red).Build());
            sut.ResponseFeedback(Feedback().WithBlacks(3).WithEmpty(1).Build());

            sut.ShowWrongFeedbackGiven().Should().NotBe(FeedbackInspection.NoWrong);
        }

        [Test]
        public void MustResultIn_NoWrongFeedback_IfFeedbackWasAllCorrect()
        {
            var sut = Board().WithRows(1).WithSecretCode(Combination().AllOf(CodeColor.Red)).Build();
            sut.AttemptGuess(Combination().AllOf(CodeColor.Red).Build());
            sut.ResponseFeedback(Feedback().AllBlacks().Build());

            sut.ShowWrongFeedbackGiven().Should().Be(FeedbackInspection.NoWrong);
        }

        [Test]
        public void WrongFeedback_IncludesInformationAboutTheMiss() { }
    }
}