using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IndexSample
{
    public class NameList : IEnumerable<string>
    {
        private List<string> _names;
        public NameList()
        {
            _names = new List<string>();
        }

        public IEnumerator<string> GetEnumerator()
        {
            return _names.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_names).GetEnumerator();
        }

        public void Add(string name) => _names.Add(name);

        public int Count => _names.Count;   


        public string this[int index] => _names[index];

        public List<string> Slice(int start, int length)
                => _names
         .Skip(start)
         .Take(length)
         .ToList();
    }
}
