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
        public GuessFeedback MatchWith(Combination other)
        {
            var keyPegs = new KeyColor[PegsCount]; //All empty by default.

            MatchBlacks(other, ref keyPegs);
            MatchWhites(other, ref keyPegs);

            return new GuessFeedback(keyPegs);
        }

        void MatchBlacks(Combination other, ref KeyColor[] keyPegs)
        {
            for(var i = 0; i < codePegs.Count; i++)
                if(codePegs[i] == other.codePegs[i])
                    keyPegs[i] = KeyColor.Black;
        }

        void MatchWhites(Combination other, ref KeyColor[] keyPegs)
        {
            for(var i = 0; i < codePegs.Count; i++)
            {
                if(keyPegs[i] == KeyColor.Black)
                    continue;
                if(codePegs.Count(c => c == other.codePegs[i]) > keyPegs.Count(c => c == KeyColor.Black))
                    keyPegs[i] = KeyColor.White;
            }
        }


        #region Formatting
        public override string ToString()
        {
            return string.Join(" ", codePegs.Select(c => c.ToString()));
        }
        #endregion
    }
}