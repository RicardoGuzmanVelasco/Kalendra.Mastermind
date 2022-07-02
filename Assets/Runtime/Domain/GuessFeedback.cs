using System.Collections.Generic;
using System.Linq;
using static RGV.DesignByContract.Runtime.Precondition;

namespace Runtime.Domain
{
    public class GuessFeedback
    {
        readonly IDictionary<KeyColor, int> keypegs;

        public GuessFeedback(ICollection<KeyColor> keypegs)
        {
            Require(keypegs.Count == 4).True();

            this.keypegs = keypegs.Distinct().ToDictionary
            (
                color => color,
                color => keypegs.Count(c => c == color)
            );
        }

        public bool IsEndOfRound =>
            keypegs.TryGetValue(KeyColor.Black, out var blacks)
            && blacks == Combination.PegsCount;


        #region Equality
        public override bool Equals(object obj)
        {
            return obj is GuessFeedback other &&
                   keypegs.Keys.All
                   (
                       key => other.keypegs.ContainsKey(key) &&
                              keypegs[key] == other.keypegs[key]
                   );
        }

        public override int GetHashCode()
        {
            return keypegs != null ? keypegs.GetHashCode() : 0;
        }
        #endregion

        #region Formatting
        public override string ToString()
        {
            return string.Join
            (
                " ",
                keypegs.Select(pair => $"{pair.Value}x{pair.Key.ToString()}")
            );
        }
        #endregion
    }
}