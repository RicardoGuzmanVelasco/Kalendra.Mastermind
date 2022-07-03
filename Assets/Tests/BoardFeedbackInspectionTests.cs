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
            sut.PinGuessPegs(Combination().AllOf(CodeColor.Red).Build());
            sut.PinFeedbackPegs(Feedback().WithBlacks(3).WithEmpty(1).Build());

            sut.InspectWrongFeedbackGiven().Should().NotBe(FeedbackInspection.NoWrong);
        }

        [Test]
        public void MustResultIn_NoWrongFeedback_IfFeedbackWasAllCorrect()
        {
            var sut = Board().WithSecretCode(Combination().AllOf(CodeColor.Red)).Build();
            sut.PinGuessPegs(Combination().AllOf(CodeColor.Red).Build());
            sut.PinFeedbackPegs(Feedback().AllBlacks().Build());

            sut.InspectWrongFeedbackGiven().Should().Be(FeedbackInspection.NoWrong);
        }

        [Test]
        public void WrongFeedback_IncludesInformationAboutTheMiss()
        {
            var code = Combination().AllOf(CodeColor.Red);
            var sut = Board().WithSecretCode(code).Build();

            sut.PinGuessPegs(code.Build());
            sut.PinFeedbackPegs(Feedback().WithEmpty(4).Build());

            sut.InspectWrongFeedbackGiven().CorrectFeedback
                .Should()
                .Be(Feedback().AllBlacks().Build());
        }

        [Test]
        public void WrongFeedback_IncludesRowOfTheMiss_IndexBased1()
        {
            var code = Combination().AllOf(CodeColor.Red);
            var sut = Board().WithSecretCode(code).Build();

            sut.PinGuessPegs(code.Build());
            sut.PinFeedbackPegs(Feedback().WithEmpty(4).Build());

            sut.InspectWrongFeedbackGiven().Row.Should().Be(1);
        }
    }
}