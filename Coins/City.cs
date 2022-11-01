using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coins
{
    public class City
    {
        public int n;
        public int x;
        public int y;
        public int[] coins;

        public City(int numContries, int x, int y, int myCountry)//for the zero day
        {
            n = numContries;
            this.x = x;
            this.y = y;
            coins = new int[n];
            coins[myCountry] = 1000000;
        }

        public City(int numContries, int x, int y)//for days since the first one
        {
            n = numContries;
            this.x = x;
            this.y = y;
            coins = new int[n];
        }

        public bool isDone()// check! if all coins in not null returns true, else false
        {
            for (int i = 0; i < n; i++)
                if (coins[i] == 0)
                    return false;
            return true;
        }
    }
}
