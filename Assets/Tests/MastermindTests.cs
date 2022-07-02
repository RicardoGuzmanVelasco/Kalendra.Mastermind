using FluentAssertions;
using NUnit.Framework;
using static Tests.GuessFeedbackBuilder;

namespace Tests
{
    public class MastermindTests
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
    }
}