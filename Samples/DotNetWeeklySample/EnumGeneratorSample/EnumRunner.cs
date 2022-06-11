
using BenchmarkDotNet.Attributes;

namespace EnumGeneratorSample
{

    [MemoryDiagnoser]
    public class EnumRunner
    {
        [Benchmark]
        public string ColorToString()
        {
            return Color.Black.ToString();
        }

        [Benchmark]
        public string ColorToStringFast()
        {
            return Color.Black.ToStringFast();
        }

        [Benchmark]
        public bool ColorIsDefined()
        {
            return Enum.IsDefined(typeof(Color), (Color)10);
        }

        [Benchmark]
        public bool ColorIsDefinedFast()
        {
            return ColorExtensions.IsDefined((Color)5);
        }

        [Benchmark]
        public (bool, Color) ColorTryParse()
        {
            if (Enum.TryParse<Color>("Green", false, out var color))
            {
                return (true, color);
            }

            return (false, Color.Blue);
        }

        [Benchmark]
        public (bool, Color) ColorTryParseFase()
        {
            if (ColorExtensions.TryParse("Green", out var color))
            {
                return (true, color);
            }

            return (false, Color.White);
        }
    }
}
