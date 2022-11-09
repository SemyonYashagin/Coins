using System;

namespace Coins
{
    public class Country: IComparable<Country>
    {
        public string Name;
        public int x1, y1, x2, y2; //coordinates of cites: left buttom and top right
        public static int maxValue = int.MaxValue;//the maximum of operations (no more days then MaxValue)
        public int numberOfDays;
        public int[,] finishArray;

        public Country(string name, int xl, int yl, int xh, int yh) // init the country
        {
            Name = name;
            x1 = xl;
            y1 = yl;
            x2 = xh;
            y2 = yh;
            numberOfDays = maxValue;
            finishArray = new int[x2 - x1 + 1,y2 - y1 + 1]; // all elements are null (0)
            for (int i = 0; i < finishArray.GetLength(0); i++) //circling of all cities in the country
            {
                for (int j = 0; j < finishArray.GetLength(1); j++)
                {
                    finishArray[i, j] = -1;//do it for n=1
                }
            }
        }
        public void UpdateCompleted()//if the finishArray don't contains -1 values this array is completed (country completed)
        {
            int max = 0;
            for (int i = 0; i < finishArray.GetLength(0); i++) //circling of all cities in the country
            {
                for (int j = 0; j < finishArray.GetLength(1); j++)
                {

                    if (finishArray[i,j] == -1) //if only one city isn't completed
                        return;//exit
                    else 
                        max = Math.Max(max, finishArray[i,j]);
                }
            }
            numberOfDays = max;
        }
        public void Update(City[,] city, int day)//update finishArray
        {
            for (int i = 0; i < finishArray.GetLength(0); i++)
                for (int j = 0; j < finishArray.GetLength(1); j++)
                    if (finishArray[i,j] == -1 && city[x1 + i,y1 + j].isDone())//if in the current city there are all coins[i] exist 
                        finishArray[i,j] = day;//then this city is completed
        }
        public bool isComplete()//the county is comlete or not
        {
            return numberOfDays < maxValue;
        }
        public int CompareTo(Country other)// should do this for sorting countries by alphabet (IComparable<Country)
        {
            if (this.numberOfDays != other.numberOfDays)
                return this.numberOfDays - other.numberOfDays;

            return this.Name.ToLower().CompareTo(other.Name.ToLower());
        }
        public override string ToString()
        {
            return " " + Name + " " + numberOfDays;
        }
    }
}
