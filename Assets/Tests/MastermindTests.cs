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
        public void SameCombination_AskedForFeedback_IsFourBlackPegs()
        {
            var docColors = 4.CodeColors();
            var sut = new Combination(docColors);

            sut.AttemptMatchWith(new Combination(docColors))
                .Should()
                .BeEquivalentTo(AllBlacks());
        }

        [Test]
        public void DisjuntCombination_AskedForFeedback_IsFourEmptyPegs()
        {
            var sut = Combination().AllOf(Red).Build();
            var disjuntCombination = Combination().AllOf(Blue).Build();

            sut.AttemptMatchWith(disjuntCombination)
                .Should()
                .BeEquivalentTo(AllEmpty());
        }

        [Test]
        public void OnlySameColor_ReturnsBlackPegs()
        {
            Combination().AllOf(Red).Build()
                .AttemptMatchWith
                (
                    Combination().With(Red).Then(3, Blue).Build()
                ).Should()
                .BeEquivalentTo(Feedback().WithBlack().ThenNones(3).Build());

            Combination().AllOf(Green).Build()
                .AttemptMatchWith
                (
                    Combination().With(Yellow).Then(3, Green).Build()
                ).Should()
                .BeEquivalentTo(Feedback().WithNone().ThenBlacks(3).Build());
        }
    }
}