using System;
using FluentAssertions;
using NUnit.Framework;
using Runtime.Domain;
using static System.Linq.Enumerable;

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
                .Should().OnlyContain(c => c == KeyColor.Black);
        }

        [Test]
        public void DisjuntCombination_AskedForFeedback_IsFourWhitePegs()
        {
            var sut = new Combination(Repeat(CodeColor.Red, 4).ToArray());
            var disjuntCombination = new Combination(Repeat(CodeColor.Blue, 4).ToArray());

            sut.AttemptMatchWith(disjuntCombination)
                .Should().OnlyContain(c => c == KeyColor.White);
        }
    }
}