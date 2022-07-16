using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexSample
{
    public class IndexClass
    {
        public static string GetFrist(string[] names)
        {
            var index = new Index(0);
            return names[index];
        }

        public static string GetLastMethod1(string[] names)
        {
            var index = new Index(1, true);
            return names[index];
        }

        public static string GetLastMethod2(string[] names)
        {
            return names[^1];
        }

        public static string GetSecondLast(string[] names)
        {
            return names[^2];
        }
    }
}
