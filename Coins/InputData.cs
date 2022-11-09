using System.Collections.Generic;
using System;

namespace Coins
{
    
    public class InputData
    {
        private int n;
        public int N
        {
            get { return n; }
            set
            {
               n = value;
            }
        }

        public List<CountryData> Countries { get; set; }
    }

    public class CountryData
    {
        private string Name;
        
        public string name
        {
            get { return Name; }
            set
            {
                Name = value;
            }
        }
        private int x1;
        public int X1
        {
            get { return x1; }
            set
            {
               x1 = value;
            }
        }

        private int y1;
        public int Y1
        {
            get { return y1; }
            set
            {
                y1 = value;
            }
        }
        private int x2;
        public int X2
        {
            get { return x2; }
            set
            {
                x2 = value;
            }
        }
        private int y2;
        public int Y2
        {
            get { return y2; }
            set
            {
                y2 = value;
            }
        }
    }
}
