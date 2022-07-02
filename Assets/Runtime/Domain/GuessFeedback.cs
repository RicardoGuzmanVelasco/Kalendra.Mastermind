using System.Collections;
using System.Collections.Generic;
using RGV.DesignByContract.Runtime;
using static RGV.DesignByContract.Runtime.Precondition;

namespace Runtime.Domain
{
    public class GuessFeedback : IReadOnlyCollection<KeyColor>
    {
        readonly ICollection<KeyColor> keypegs;

        public GuessFeedback(ICollection<KeyColor> keypegs)
        {
            Require(keypegs.Count == 4).True();
            
            this.keypegs = keypegs;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyColor> GetEnumerator()
        {
            return keypegs.GetEnumerator();
        }

        public int Count => keypegs.Count;
    }
}