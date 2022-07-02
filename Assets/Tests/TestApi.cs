using System.Collections.Generic;
using Runtime.Domain;
using static RGV.DesignByContract.Runtime.Precondition;

namespace Tests
{
    public static class TestApi
    {
        public static IList<CodeColor> CodeColors(this int howMany)
        {
            Require(howMany).Not.Negative();

            var colors = new CodeColor[howMany];

            for(var i = 0; i < colors.Length; i++)
                colors[i] = CodeColor.Black;

            return colors;
        }

        public static IList<KeyColor> KeyColors(this int howMany)
        {
            Require(howMany).Not.Negative();

            var colors = new KeyColor[howMany];

            for(var i = 0; i < colors.Length; i++)
                colors[i] = KeyColor.Black;

            return colors;
        }
    }
}