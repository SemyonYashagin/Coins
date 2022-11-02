using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins
{
    public class InputData
    {
        public int n { get; set; }
        public List<CountryData> Countries { get; set; }
    }

    public class CountryData
    {
        public string Name { get; set; }
        public int x1 { get; set; }
        public int y1 { get; set; }
        public int y2 { get; set; }
        public int x2 { get; set; }
    }
}
