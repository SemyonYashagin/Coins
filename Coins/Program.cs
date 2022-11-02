using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


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
            try
            {
                string outputData = "";
                List<InputData> inputData = ParseDataFromInputFile();

                if (IsFileExist() && inputData.Count != 0)
                {
                    Country[] list;
                    City[,] grid = new City[11, 11]; //11x11 - is maximum of cities
                    int count = 1; // count of a case
                    foreach (InputData line in inputData)
                    {
                        int k = 0;
                        n = line.n;

                        if (n == 1) { day = 0; } else { day = 1; } // if a country includes only one city day = 0
                        list = new Country[n];                        

                        for (int i = 0; i < 11; i++)
                            for (int j = 0; j < 11; j++)
                                grid[i, j] = null;

                        foreach (CountryData country in line.Countries)
                        {
                            list[k] = new Country(country.Name, country.x1, country.y1, country.x2, country.y2);//the array of countries
                            Init(grid, list[k], k, n);
                            k++;
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
                        outputData += "Case Number " + count + "\n";
                        for (int i = 0; i < n; i++)
                            outputData += list[i].ToString() + "\n";

                        count++;
                    }

                    WriteDataIntoOutputFile(outputData);
                    Console.WriteLine("All right!!!\nThe data insert into output.txt file");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("input.txt is invalid. Check it.");
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("The format of input.txt file isn't correct\n"+ex.Message);
                Console.ReadLine();
            }
        }

        public static void UpdateStatus(City[,] grid, Country[] list)
        {
            // Update data of a city, then check if country are complete or not
            for (int i = 0; i < n; i++)
            {
                list[i].Update(grid, day);//update city status (finishArray)
                list[i].UpdateCompleted();
            }
        }

        public static City[,] TransferCoins(City[,] grid)
        {
            int[] move = new int[n];
            City[,] next = new City[11,11];

            // Save a new copy for the next day
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
                        for (int k = 0; k < n; k++)
                            move[k] = grid[i,j].coins[k] / 1000; // 1/1000 part have to send to a neighbors

                        // Move coins to neighbors
                        int numNeighbors = 0;
                        for (int k = 0; k < dx.Length; k++)
                        {
                            int myx = i + dx[k];//the x coord of the neighbor
                            int myy = j + dy[k];//the y coord of the neighbor
                            if (InGrid(myx, myy) && grid[myx,myy] != null)
                            {
                                numNeighbors++;
                                for (int z = 0; z < n; z++)
                                    next[myx,myy].coins[z] += move[z];//sum = what we have in current + what we have from other neighbor
                            }
                        }

                        // Update coins for neighbor
                        for (int k = 0; k < n; k++)
                            next[i,j].coins[k] += (grid[i,j].coins[k] - numNeighbors * move[k]);// - how many coins we sent to others, grid - the last grid, next - the current grid
                    }
                }
            }
            return next;
        }

        //return true if a neighbor (city) not out of grid
        public static bool InGrid(int x, int y)
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

        public static void WriteDataIntoOutputFile(string data)
        {
            string currentDirectory = Directory.GetParent(Environment.CurrentDirectory.ToString()).Parent.FullName;
            string outputFilePath = Path.Combine(currentDirectory, "data\\output.txt");
            
            if (File.Exists(outputFilePath))
                File.Delete(outputFilePath);

            using (FileStream fs = File.Create(outputFilePath))
            {
                Byte[] output = new UTF8Encoding(true).GetBytes(data);
                fs.Write(output, 0, output.Length);
            }
        }

        public static List<InputData> ParseDataFromInputFile()
        {
            List<InputData> list = new List<InputData>();
            if(IsFileExist())
            {
                try
                {
                    string currentDirectory = Directory.GetParent(Environment.CurrentDirectory.ToString()).Parent.FullName;
                    string inputFilePath = Path.Combine(currentDirectory, "data\\input.txt");

                    using (StreamReader reader = new StreamReader(inputFilePath))
                    {
                        string line = reader.ReadLine();
                        while (line != "0" && line!= null)
                        {
                            InputData data = new InputData
                            {
                                n = Convert.ToInt32(line),
                                Countries = new List<CountryData> { }
                            };
                            for (int i = 0; i < data.n; i++)
                            {
                                string[] str = reader.ReadLine().Split(' ');
                                CountryData countryData = new CountryData
                                {
                                    Name = str[0],
                                    x1 = Convert.ToInt32(str[1]),
                                    y1 = Convert.ToInt32(str[2]),
                                    x2 = Convert.ToInt32(str[3]),
                                    y2 = Convert.ToInt32(str[4])
                                };
                                data.Countries.Add(countryData);
                            }
                            list.Add(data);
                            line = reader.ReadLine();
                        }
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Invalid input.txt file. Check it.\n" + Ex.Message);
                    Console.ReadLine();
                    list.Clear();
                }
            }           
            return list;
        }

        public static bool IsFileExist()
        {
            string currentDirectory = Directory.GetParent(Environment.CurrentDirectory.ToString()).Parent.FullName;
            string inputFilePath = Path.Combine(currentDirectory, "data\\input.txt");           
            return File.Exists(inputFilePath);
        }
    }
}