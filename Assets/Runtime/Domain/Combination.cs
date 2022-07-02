using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Precondition;

namespace Runtime.Domain
{
    public class Combination
    {
        public const int PegsCount = 4;
        [NotNull] readonly ICollection<CodeColor> codepegs;

        public Combination([NotNull] ICollection<CodeColor> codepegs)
        {
            Require(codepegs.Count == PegsCount).True();

            this.codepegs = codepegs;
        }

        [NotNull]
        public GuessFeedback AttemptMatchWith(Combination other)
        {
            if(codepegs.First() == other.codepegs.First() && codepegs.Last() != other.codepegs.Last())
                return new GuessFeedback(new[] { KeyColor.Black }.Concat(Enumerable.Repeat(KeyColor.None, 3))
                    .ToArray());
            if(!codepegs.Intersect(other.codepegs).Any())
                return new GuessFeedback(Enumerable.Repeat(KeyColor.White, PegsCount).ToList());
            else
                return new GuessFeedback(Enumerable.Repeat(KeyColor.Black, PegsCount).ToList());
        }

        #region Formatting
        #endregion
    }
}