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
        public static int[] dx = { 0, -1, 1, 0 };// on "x" coordinate for moving coins to neighbor
        public static int[] dy = { -1, 0, 0, 1 };// on "y" coordinate for moving coins to neighbor
        static void Main(string[] args)
        {   
            Console.WriteLine("Input the number of countries \"n\" (1<=n<=20): ");            
            while(!int.TryParse(Console.ReadLine(), out n)) //the number of countries
            {
                Console.WriteLine("the \"n\" should be the number");
            }
            
            if (n == 1) { day = 0; } else { day = 1; } // if a country includes only one city day = 0
            int count = 1; // count of a case

            //while an user don't input n = 0 
            while (n!=0)
            {
                int x1, x2,y1, y2;// city's coordinates
                Country[] list = new Country[n];
                City[,] grid = new City[11,11]; //11x11 - is maximum of cities

                for (int i = 0; i < 11; i++)
                    for (int j = 0; j < 11; j++)
                        grid[i, j] = null;

                for (int i = 0; i < n; i++)
                {
                    //parsing the values s, x1,y1,x2,y2
                    string[] str = Console.ReadLine().ToString().Split(' ');
                    string s = str[0];
                    int.TryParse(str[1], out x1);
                    int.TryParse(str[2], out y1);
                    int.TryParse(str[3], out x2);
                    int.TryParse(str[4], out y2);

                    list[i] = new Country(s, x1, y1, x2, y2);//the array of countries
                    Init(grid, list[i], i, n);
                }

                // Do this while the countries isn't completed
                while (!Done(list))
                {
                    grid = TransferCoins(grid);
                    UpdateStatus(grid, list);
                    day++;
                }

                // need to fix a sorting of array
                Array.Sort(list);
                Console.WriteLine("Case Number " + count);
                for (int i = 0; i < n; i++)
                    Console.WriteLine(list[i].ToString());

                count++;
                int.TryParse(Console.ReadLine(), out n); // repeat the input
            }
        }

        public static void UpdateStatus(City[,] grid, Country[] list)
        {
            // Update data of a city, then check if country are complete or not
            for (int i = 0; i < n; i++)
            {
                list[i].Update(grid, day);
                list[i].isCountryCompleted();
            }
        }

        public static City[,] TransferCoins(City[,] grid)
        {
            // Save a new copy for the next day
            City[,] next = new City[11,11];
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (grid[i,j] == null) next[i,j] = null;
                    else next[i,j] = new City(n, i, j);
                }
            }

            // Go on the whole city grid
            for (int i = 0; i < 11; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (grid[i,j] != null)
                    {
                        // Calculate how many coins shall go to neighbors
                        int[] move = new int[n];
                        for (int k = 0; k < n; k++)
                            move[k] = grid[i,j].coins[k] / 1000;

                        // Move coins to neighbors
                        int numNeighbors = 0;
                        for (int k = 0; k < dx.Length; k++)
                        {
                            int myx = i + dx[k];//the x coord of the neighbor
                            int myy = j + dy[k];//the y coord of the neighbor
                            if (InBounds(myx, myy) && grid[myx,myy] != null)
                            {
                                numNeighbors++;
                                for (int z = 0; z < n; z++)
                                    next[myx,myy].coins[z] += move[z];//sum = what we have in current + what we have from other neighbor
                            }
                        }

                        // Update coins for neighbor
                        for (int k = 0; k < n; k++)
                            next[i,j].coins[k] += (grid[i,j].coins[k] - numNeighbors * move[k]);// - how many coins we sent to others
                    }
                }
            }
            return next;
        }
        //return true if a neighbor (city) not out of gri
        public static bool InBounds(int x, int y)
        {
            return 0 <= x && x < 11 && 0 <= y && y < 11;
        }

        // In this method we put the country object on the grid
        public static void Init(City[,] grid, Country country, int index, int numCountries)
        {
            for (int i = country.x1; i <= country.x2; i++)
                for (int j = country.y1; j <= country.y2; j++)
                    grid[i,j] = new City(numCountries, i, j, index);
        }

        // Returns true if all countries are done
        public static bool Done(Country[] list)
        {
            for (int i = 0; i < n; i++)
                if (!list[i].isComplete())
                    return false;
            return true;
        }
    }
}
