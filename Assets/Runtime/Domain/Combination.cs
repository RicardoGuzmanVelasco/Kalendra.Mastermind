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