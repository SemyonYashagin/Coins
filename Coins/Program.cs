using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Coins
{
    public class Program
    {
        public static int day;
        public static int n; //the count of countries
        public static int[] dx = { 0, -1, 1, 0 };
        public static int[] dy = { -1, 0, 0, -1 };
        static void Main(string[] args)
        {

            n = int.Parse(Console.ReadLine()); //the number of countries
            int count = 1; // count of a case

            while (n!=0)
            {
                int x1, x2,y1, y2;
                Country[] list = new Country[n];
                City[,] grid = new City[11,11];

                for (int i = 0; i < 11; i++)
                    for (int j = 0; j < 11; j++)
                        grid[i, j] = null;

                for (int i = 0; i < n; i++)
                {
                    string s = Console.Read().ToString();
                    //int.TryParse(Console.Read().ToString(), out x1);
                    //int.TryParse(Console.Read().ToString(), out y1);
                    //int.TryParse(Console.Read().ToString(), out x2);
                    //int.TryParse(Console.Read().ToString(), out y2);

                    list[i] = new Country(s, x1, y1, x2, y2);
                    init(grid, list[i], i, n);
                }

                // Annoying special case.
                if (n == 1) 
                    day = 0;
                else 
                    day = 1;

                // Simulate.
                while (!done(list))
                {
                    grid = updateCoins(grid);
                    updateStatus(grid, list);
                    day++;
                }

                Array.Sort(list);
                Console.WriteLine("Case Number " + count);
                for (int i = 0; i < n; i++)
                    Console.WriteLine(list[i]);
            }
        }

        public static void updateStatus(City[,] grid, Country[] list)
        {

            // First update completion dates per city, then check if countries are complete.
            for (int i = 0; i < n; i++)
            {
                list[i].Update(grid, day);
                list[i].isCountryCompleted();
            }
        }

        public static City[,] updateCoins(City[,] grid)
        {

            // Store a new copy for the next day.
            City[,] next = new City[11,11];
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (grid[i,j] == null) next[i,j] = null;
                    else next[i,j] = new City(n, i, j);
                }
            }

            // Go through the whole grid.
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (grid[i,j] != null)
                    {

                        // Calculate coins that go to neighbors.
                        int[] move = new int[n];
                        for (int k = 0; k < n; k++)
                            move[k] = grid[i,j].coins[k] / 1000;

                        // Move coins to neighbors.
                        int numNeighbors = 0;
                        for (int k = 0; k < dx.Length; k++)
                        {
                            int myx = i + dx[k];
                            int myy = j + dy[k];
                            if (inbounds(myx, myy) && grid[myx,myy] != null)
                            {
                                numNeighbors++;
                                for (int z = 0; z < n; z++)
                                    next[myx,myy].coins[z] += move[z];
                            }
                        }

                        // Update coins left for you.
                        for (int k = 0; k < n; k++)
                            next[i,j].coins[k] += (grid[i,j].coins[k] - numNeighbors * move[k]);
                    }
                }
            }

            return next;
        }

        public static bool inbounds(int x, int y)
        {
            return 0 <= x && x < 11 && 0 <= y && y < 11;
        }

        // Initialize the city grid based on this country which has the given index.
        public static void init(City[,] grid, Country usa, int index, int numCountries)
        {
            for (int i = usa.x1; i <= usa.x2; i++)
                for (int j = usa.y1; j <= usa.y2; j++)
                    grid[i,j] = new City(numCountries, i, j, index);
        }

        // Returns true iff all countries are done.
        public static bool done(Country[] list)
        {
            for (int i = 0; i < n; i++)
                if (!list[i].isComplete())
                    return false;
            return true;
        }

    }
}
