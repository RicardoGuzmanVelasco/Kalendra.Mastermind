using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Precondition;

namespace Runtime.Domain
{
    public class Combination
    {
        public const int PegsCount = 4;
        [NotNull] readonly IList<CodeColor> codePegs;

        public Combination([NotNull] IList<CodeColor> codePegs)
        {
            Require(codePegs.Count == PegsCount).True();

            this.codePegs = codePegs;
        }

        [NotNull]
        public GuessFeedback AttemptMatchWith(Combination other)
        {
            if(codePegs.First() == other.codePegs.First() && codePegs.Last() != other.codePegs.Last())
                return new GuessFeedback(new[] { KeyColor.Black }.Concat(Enumerable.Repeat(KeyColor.None, 3))
                    .ToArray());
            if(!codePegs.Intersect(other.codePegs).Any())
                return new GuessFeedback(Enumerable.Repeat(KeyColor.White, PegsCount).ToList());
            if(codePegs.SequenceEqual(other.codePegs))
                return new GuessFeedback(Enumerable.Repeat(KeyColor.Black, PegsCount).ToList());

            var keyPegs = new List<KeyColor>();
            for(var i = 0; i < codePegs.Count; i++)
                if(codePegs[i] == other.codePegs[i])
                    keyPegs.Add(KeyColor.Black);
                else
                    keyPegs.Add(KeyColor.None);

            return new GuessFeedback(keyPegs);
        }

        #region Formatting
        public override string ToString()
        {
            return string.Join(" ", codePegs.Select(c => c.ToString()));
        }
        #endregion
    }
}