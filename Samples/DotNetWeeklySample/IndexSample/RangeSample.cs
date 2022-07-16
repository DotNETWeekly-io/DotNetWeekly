using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexSample
{
    public class RangeSample
    {

        public static string[] GetAll(string[] arr)
        {
            return arr[..];
        }

        public static string[] GetFirstTwoElements(string[] arr)
        {
            var start = new Index(0);
            var end = new Index(2);
            var range = new Range(start, end);

            return arr[range];
        }

        public static string[] GetFirstThreeElements(string[] arr)
        {
            return arr[..3];
        }
        public static string[] GetLastThreeElements(string[] arr)
        {
            return arr[^3..];
        }
        public static string[] GetThreeElementsFromMiddle(string[] arr)
        {
            return arr[3..6];
        }


    }
}
