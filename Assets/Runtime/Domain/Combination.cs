using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using static RGV.DesignByContract.Runtime.Precondition;

namespace Runtime.Domain
{
    public class Combination
    {
        [NotNull] readonly ICollection<CodeColor> codepegs;

        public Combination([NotNull] ICollection<CodeColor> codepegs)
        {
            Require(codepegs.Count == 4).True();

            this.codepegs = codepegs;
        }

        [NotNull]
        public GuessFeedback AttemptMatchWith(Combination other)
        {
            if(codepegs.First() == other.codepegs.First() && codepegs.Last() != other.codepegs.Last())
                return new GuessFeedback(new[] { KeyColor.Black }.Concat(Enumerable.Repeat(KeyColor.None, 3))
                    .ToArray());
            if(!codepegs.Intersect(other.codepegs).Any())
                return new GuessFeedback(Enumerable.Repeat(KeyColor.White, 4).ToList());
            else
                return new GuessFeedback(Enumerable.Repeat(KeyColor.Black, 4).ToList());
        }

        #region Formatting
        #endregion
    }
}