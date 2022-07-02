using System;
using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Runtime.Domain;

namespace Tests
{
    public class MastermindTests
    {
        #region Fixture
        static ICollection<CodeColor> RandomCodeColors(int howMany)
        {
            var colors = new CodeColor[howMany];

            for(var i = 0; i < colors.Length; i++)
                colors[i] = CodeColor.Black;

            return colors;
        }
        
        static ICollection<KeyColor> RandomKeyColors(int howMany)
        {
            var colors = new KeyColor[howMany];

            for(var i = 0; i < colors.Length; i++)
                colors[i] = KeyColor.Black;

            return colors;
        }
        #endregion
        
        [TestCase(0), TestCase(1), TestCase(2), TestCase(3), TestCase(5)]
        public void Invariant_FourPegsPerCombination(int n)
        {
            Action act = () => new Combination(RandomCodeColors(n));
            act.Should().Throw<ArgumentException>();
        }

        [TestCase(0), TestCase(1), TestCase(2), TestCase(3), TestCase(5)]
        public void Invariant_FourPegsPerGuessFeedback(int n)
        {
            Action act = () => new GuessFeedback(RandomKeyColors(n));
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void SameCombination_AskedForFeedback_IsFourBlackPegs()
        {
            var docColors = RandomCodeColors(4);
            var sut = new Combination(docColors);

            sut.AttemptMatchWith(new Combination(docColors))
                .Should()
                .HaveCount(4)
                .And
                .OnlyContain(c => c == KeyColor.Black);
        }
    }
}