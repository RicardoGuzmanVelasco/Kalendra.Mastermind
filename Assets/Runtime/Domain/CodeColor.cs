using System;

namespace Runtime.Domain
{
    public enum CodeColor
    {
        Red,
        Blue,
        Yellow,
        Green,
        White,
        Black,
    }

    public static class CodeColorExtensions
    {
        public static CodeColor RandomCodeColor(Random rand)
        {
            var values = Enum.GetValues(typeof(CodeColor));
            var color = (CodeColor)values.GetValue(rand.Next(values.Length));
            return color;
        }
    }
}