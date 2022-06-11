using NetEscapades.EnumGenerators;

namespace EnumGeneratorSample
{
    [EnumExtensions]
    public enum Color
    {
        /// <summary>No color was read</summary>
        None = 0,

        /// <summary>Black</summary>
        Black = 1,

        /// <summary>Blue</summary>
        Blue = 2,

        /// <summary>Green</summary>
        Green = 3,

        /// <summary>Yellow</summary>
        Yellow = 4,

        /// <summary>Red</summary>
        Red = 5,

        /// <summary>White</summary>
        White = 6,

        /// <summary>Brown</summary>
        Brown = 7
    }
}
