using System.Collections.Generic;
using RGV.DesignByContract.Runtime;
using static RGV.DesignByContract.Runtime.Precondition;

namespace Runtime.Domain
{
    public class Combination
    {
        public Combination(ICollection<CodeColor> codepegs)
        {
            Require(codepegs.Count == 4).True();
        }
    }
}