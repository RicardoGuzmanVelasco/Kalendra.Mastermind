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
        static ICollection<CodeColor> RandomColors(int howMany)
        {
            var colors = new CodeColor[howMany];

            for(var i = 0; i < colors.Length; i++)
                colors[i] = CodeColor.Black;

            return colors;
        }
        #endregion
        
        [TestCase(0), TestCase(1), TestCase(2), TestCase(3), TestCase(5)]
        public void Invariant_FourPegsPerCombination(int n)
        {
            Action act = () => new Combination(RandomColors(n));
            act.Should().Throw<ArgumentException>();
        }
    }
}