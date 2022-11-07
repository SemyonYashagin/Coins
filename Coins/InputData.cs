using System.Collections.Generic;
using System;

namespace Coins
{
    
    public class InputData
    {
        public int lineNumber { get; set; }
        private Variable n;
        public Variable N
        {
            get { return n; }
            set
            {
                if (!value.isValid)
                    throw new ArgumentException($"Country count \"{value.val}\" should be in \"int\" format and in the interval [1;20]" + $"\nError line number is {lineNumber}");
                if (Convert.ToInt32(value.val) < 1 || Convert.ToInt32(value.val) > 20)
                    throw new ArgumentOutOfRangeException($"Country count \"{value.val}\" should be in the interval [1;20]" + $"\nError line number is {lineNumber}");
                else n = value;
            }
        }

        public List<CountryData> Countries { get; set; }
    }

    public class CountryData
    {
        public int lineNumber { get; set; }
        private string Name;
        
        public string name
        {
            get { return Name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new NullReferenceException("Country name is null or empty. " + $"\nError line number is {lineNumber}");
                else Name = value;
            }
        }
        private Variable x1;
        public Variable X1
        {
            get { return x1; }
            set
            {
                if (!value.isValid)
                    throw new ArgumentException($"Coordinate \"{value.val}\" should be in \"int\" format in the interval [1;10]" + $"\nError line number is {lineNumber}");
                if (Convert.ToInt32(value.val) < 1 || Convert.ToInt32(value.val) > 10)
                    throw new ArgumentOutOfRangeException($"Coordinate \"{value.val}\" should be in the interval [1;10]" + $"\nError linenumber is {lineNumber}");
                else x1 = value;
            }
        }

        private Variable y1;
        public Variable Y1
        {
            get { return y1; }
            set
            {
                if (!value.isValid)
                    throw new ArgumentException($"Coordinate \"{value.val}\" should be in \"int\" format in the interval [1;10]" + $"\nError line number is {lineNumber}");
                if (Convert.ToInt32(value.val) < 1 || Convert.ToInt32(value.val) > 10)
                    throw new ArgumentOutOfRangeException($"Coordinate \"{value.val}\" should be in the interval [1;10]" + $"\nError line number is {lineNumber}");
                else y1 = value;
            }
        }
        private Variable x2;
        public Variable X2
        {
            get { return x2; }
            set
            {
                if (!value.isValid)
                    throw new ArgumentException($"Coordinate \"{value.val}\" should be in \"int\" format in the interval [1;10]" + $"\nError line number is {lineNumber}");
                if (Convert.ToInt32(value.val) < 1 || Convert.ToInt32(value.val) > 10)
                    throw new ArgumentOutOfRangeException($"Coordinate \"{value.val}\" should be in the interval [1;10]" + $"\nError line number is {lineNumber}");
                else x2 = value;
            }
        }
        private Variable y2;
        public Variable Y2
        {
            get { return y2; }
            set
            {
                if (!value.isValid)
                    throw new ArgumentException($"Coordinate \"{value.val}\" should be in \"int\" format in the interval [1;10]" + $"\nError line number is {lineNumber}");
                if (Convert.ToInt32(value.val) < 1 || Convert.ToInt32(value.val) > 10)
                    throw new ArgumentOutOfRangeException($"Coordinate \"{value.val}\" should be in the interval [1;10]" + $"\nError line number is {lineNumber}");
                else y2 = value;
            }
        }
    }

    public class Variable
    {
        public object val;
        public bool isValid;
    }
}
