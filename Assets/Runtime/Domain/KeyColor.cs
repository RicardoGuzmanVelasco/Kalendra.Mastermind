using System;

namespace Runtime.Domain
{
    public enum KeyColor
    {
        Empty = 0,
        White,
        Black,
    }

    public static class KeyColorExtensions
    {
        public static KeyColor RandomKeyColor(Random rand)
        {
            var values = Enum.GetValues(typeof(KeyColor));
            var color = (KeyColor)values.GetValue(rand.Next(values.Length));
            return color;
        }
    }
}