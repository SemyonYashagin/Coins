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
                if (value < 1 || value > 20)
                    throw new ArgumentOutOfRangeException("\"n\" (country count) should be in the interval [1;20]");
                else n = value;
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
                if (string.IsNullOrEmpty(value))
                    throw new NullReferenceException("\"Name\" (country's name) is null or empty");
                else Name = value;
            }
        }
        private int x1;
        public int X1
        {
            get { return x1; }
            set
            {
                if (value < 1 || value > 10)
                    throw new ArgumentOutOfRangeException("\"x1\" should be in the interval [1;10]");
                else x1 = value;
            }
        }

        private int y1;
        public int Y1
        {
            get { return y1; }
            set
            {
                if (value < 1 || value > 10)
                    throw new ArgumentOutOfRangeException("\"y1\" should be in the interval [1;10]");
                else y1 = value;
            }
        }
        private int x2;
        public int X2
        {
            get { return x2; }
            set
            {
                if (value < 1 || value > 10)
                    throw new ArgumentOutOfRangeException("\"x2\" should be in the interval [1;10]");
                else x2 = value;
            }
        }
        private int y2;
        public int Y2
        {
            get { return y2; }
            set
            {
                if (value < 1 || value > 10)
                    throw new ArgumentOutOfRangeException("\"y2\" should be in the interval [1;10]");
                else y2 = value;
            }
        }
    }
}
