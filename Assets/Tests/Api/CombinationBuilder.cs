using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Domain;
using static RGV.DesignByContract.Runtime.Precondition;
using static Runtime.Domain.CodeColorExtensions;

namespace Tests
{
    internal class CombinationBuilder
    {
        readonly List<CodeColor> colors = new List<CodeColor>();

        public static CombinationBuilder Combination()
        {
            return new CombinationBuilder();
        }

        public CombinationBuilder AllRandom()
        {
            while(colors.Count < Runtime.Domain.Combination.PegsCount)
                colors.Add(RandomCodeColor(new Random()));
            return this;
        }

        public CombinationBuilder AllOf(CodeColor color)
        {
            Require(colors.Any()).False();

            return With(Runtime.Domain.Combination.PegsCount, color);
        }

        public CombinationBuilder WithRandom()
        {
            return With(RandomCodeColor(new Random()));
        }

        public CombinationBuilder With(int count, CodeColor color)
        {
            Require(colors.Count + count).Not.GreaterThan(Runtime.Domain.Combination.PegsCount);

            colors.AddRange(Enumerable.Repeat(color, count));
            return this;
        }

        public CombinationBuilder With(params CodeColor[] someColors)
        {
            Require(colors.Count).LesserThan(Runtime.Domain.Combination.PegsCount);

            colors.AddRange(someColors);
            return this;
        }

        public CombinationBuilder Then(int count, CodeColor color)
        {
            return With(count, color);
        }

        public Combination Build()
        {
            Require(colors.Count == Runtime.Domain.Combination.PegsCount).True();

            return new Combination(colors);
        }
    }
}