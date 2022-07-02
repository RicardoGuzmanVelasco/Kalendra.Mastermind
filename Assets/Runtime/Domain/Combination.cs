using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using RGV.DesignByContract.Runtime;
using static RGV.DesignByContract.Runtime.Precondition;

namespace Runtime.Domain
{
    public class Combination
    {
        public Combination([NotNull] ICollection<CodeColor> codepegs)
        {
            Require(codepegs.Count == 4).True();
        }

        [NotNull] public GuessFeedback AttemptMatchWith(Combination other)
        {
            return new GuessFeedback(Enumerable.Repeat(KeyColor.Black, 4).ToList());
        }
    }
}