using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndexSample
{
    public class NameListSample
    {
        public static string GetIndex(NameList list)
        {
            return list[^1];
        }

        public static List<string> GetRange(NameList list)
        {
            return list[^1..];
        }
    }
}
