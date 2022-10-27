using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Coins
{
    public class Country
    {
        public string Name;
        public int x1;
        public int y1;
        public int x2;
        public int y2;
        public static int NOTDONE = 1000000000;
        public int isComplete;
        public int[,] grid;

        public Country(string name, int xl, int yl, int xh, int yh) // init the country
        {
            Name = name;
            x1 = xl;
            y1 = yl;
            x2 = xh;
            y2 = yh;
            isComplete = NOTDONE;
            grid = new int[x2 - x1 + 1,y2 - y1 + 1]; // all elements are null (0)
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    grid[i, j] = -1;
                }
            }
        }

        public void isCountryCompleted()
        {
            int max = 1;
            for (int i = 0; i < grid.GetLength(0); i++) //circling of all cities in the country
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {

                    if (grid[i,j] == 0) 
                        return;
                    else 
                        max = Math.Max(max, grid[i,j]);
                }
            }
        }

        public void Update(City[,] city, int day)
        {
            for (int i = 0; i < x2 - x1 + 1; i++)
                for (int j = 0; j < y2 - y1 + 1; j++)
                    if (grid[i,j] == 0 && city[x1 + i,y1 + j].done())
                        grid[i,j] = day;
        }


    }
}
