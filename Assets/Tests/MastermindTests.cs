using System;
using FluentAssertions;
using NUnit.Framework;
using Runtime.Domain;
using static Runtime.Domain.CodeColor;
using static Tests.CombinationBuilder;
using static Tests.GuessFeedbackBuilder;

namespace Tests
{
    public class MastermindTests
    {
        [TestCase(0), TestCase(1), TestCase(2), TestCase(3), TestCase(5)]
        public void Invariant_FourPegsPerCombination(int some)
        {
            Action act = () => new Combination(some.CodeColors());
            act.Should().Throw<ArgumentException>();
        }

        [TestCase(0), TestCase(1), TestCase(2), TestCase(3), TestCase(5)]
        public void Invariant_FourPegsPerGuessFeedback(int some)
        {
            Action act = () => new GuessFeedback(some.KeyColors());
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void GuessFeedback_Equality()
        {
            Feedback().AllBlacks().Build()
                .Should().Be(Feedback().AllBlacks().Build());
            Feedback().AllWhites().Build()
                .Should().NotBe(Feedback().AllBlacks().Build());

            Feedback().WithBlacks(2).WithWhites(1).WithEmpty(1).Build()
                .Should().Be(Feedback().WithBlacks(2).WithWhites(1).WithEmpty(1).Build());

            Feedback().WithEmpty(2).WithBlacks(1).WithWhites(1).Build()
                .Should().Be(Feedback().WithBlacks(1).WithWhites(1).WithEmpty(2).Build());
        }

        [Test]
        public void SameCombination_AskedForFeedback_IsFourBlackPegs()
        {
            var sut = Combination().AllRandom().Build();

            sut.AttemptMatchWith(sut)
                .Should()
                .Be(Feedback().AllBlacks().Build());
        }

        [Test]
        public void DisjuntCombination_AskedForFeedback_IsFourEmptyPegs()
        {
            var sut = Combination().AllOf(Red).Build();
            var disjuntCombination = Combination().AllOf(Blue).Build();

            sut.AttemptMatchWith(disjuntCombination)
                .Should()
                .Be(Feedback().AllEmpty().Build());
        }

        [Test]
        public void OnlySameColor_ReturnsBlackPegs()
        {
            Combination().AllOf(Red).Build()
                .AttemptMatchWith
                (
                    Combination().With(Red).Then(3, Blue).Build()
                )
                .Should()
                .Be(Feedback().WithBlacks(1).WithEmpty(3).Build());

            Combination().AllOf(Green).Build()
                .AttemptMatchWith
                (
                    Combination().With(Yellow).Then(3, Green).Build()
                )
                .Should()
                .Be(Feedback().WithEmpty(1).WithBlacks(3).Build());

            Combination().With(Red, Green, Red, Green).Build()
                .AttemptMatchWith
                (
                    Combination().AllOf(Green).Build()
                )
                .Should()
                .Be(Feedback().WithEmpty(2).WithBlacks(2).Build());
        }

        [Test]
        public void SameFourColors_NotRepeated_AndNotTheSamePositions_AllWhites()
        {
            Combination().With(Blue, Yellow, Red, Green).Build()
                .AttemptMatchWith
                (
                    Combination().With(Green, Red, Yellow, Blue).Build()
                )
                .Should()
                .Be(Feedback().AllWhites().Build());
        }

        [Test]
        public void SameTwoColors_Repeated_ButNotTheSamePositions_AllWhites()
        {
            Combination().With(Blue, Yellow, Blue, Yellow).Build()
                .AttemptMatchWith
                (
                    Combination().With(Yellow, Blue, Yellow, Blue).Build()
                )
                .Should()
                .Be(Feedback().AllWhites().Build());
        }
    }
}