using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travelling_Salesmen
{
    class Program
    {
        static void Main(string[] args)
        {
            int elements;
            double distCurrent = 1000000;
            double distTemp;
            using (StreamWriter sw = new StreamWriter("City Ditances.txt"))
            {
                // Console.WriteLine("Please enter the number of cities ");
                // elements = Convert.ToInt32(Console.ReadLine());
               
                for (int k = 2; k < 10; k++)
                {
                    distTemp = 0;
                    distCurrent = (k * 1000 + 1);
                    Stopwatch time = new Stopwatch();
                    elements = k;
                    int[,] cities = new int[elements, 2];
                    int[] currentBest = new int[elements];
                    int[] numCities = new int[elements];
                    cities = genRandArr(elements);
                    for (int i = 0; i < numCities.Length; i++)
                    {
                        numCities[i] = i + 1;
                        currentBest[i] = i + 1;
                    }
                    sw.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    sw.WriteLine("Running Calculations on " + k + " number of cities");
                    sw.WriteLine("City Coordinates Are: ");
                    sw.Write("X Coord: ");
                    for (int i = 0; i < cities.Length / 2; i++)
                    {
                        sw.Write(cities[i, 0] + " ");
                    }
                    sw.WriteLine();
                    sw.Write("Y Coord: ");
                    for (int i = 0; i < cities.Length / 2; i++)
                    {
                        sw.Write(cities[i, 1] + " ");
                    }
                    time.Start();
                    for (long i = 0; i < factorial(elements); i++)
                    {
                        distTemp = checkDist(cities, numCities, elements);
                        if (distTemp < distCurrent)
                        {

                            distCurrent = distTemp;
                            for (int j = 0; j < numCities.Length; j++)
                            {
                                currentBest[j] = numCities[j];
                            }
                            sw.Write("New Current Best With Distance: " + distCurrent + ", and City Arrangement Of: ");
                            Console.Write("New Current Best With Distance: " + distCurrent + ", and City Arrangement Of: ");
                            for (int j = 0; j < numCities.Length; j++)
                            {
                                sw.Write(numCities[j] + " ");
                                Console.Write(numCities[j] + " ");
                            }
                            Console.WriteLine();
                            sw.WriteLine();
                            sw.WriteLine();

                        }
                        if( i % 1000000 == 0)
                        {
                            sw.WriteLine();
                            sw.WriteLine("Iteration " + i + " With City Line Up At: ");
                            Console.WriteLine("Iteration " + i + " With City Line Up At: ");
                            for (int j = 0; j < numCities.Length; j++)
                            {
                                sw.Write(numCities[j] + " ");
                                Console.Write(numCities[j] + " ");
                            }
                            Console.WriteLine();
                            sw.WriteLine();
                            sw.WriteLine("Time Taken Thus Far Is " + time.ElapsedMilliseconds + " Milliseconds");
                            Console.WriteLine("Time Taken Thus Far Is " + time.ElapsedMilliseconds + " Milliseconds");
                            sw.WriteLine();
                        }
                        numCities = sqArrange(numCities);
                        /*   for (int j = 0; j < numCities.Length; j++)
                           {
                               Console.Write(numCities[j] + " ");
                           }
                           Console.WriteLine();
                           */
                    }
                    time.Stop();
                    sw.WriteLine();
                    sw.WriteLine("#########################################################");
                    sw.WriteLine("Computations Finished");
                    sw.Write("Best Distance is: " + distCurrent + " With City Arrangements ");
                    for (int j = 0; j < currentBest.Length; j++)
                    {
                        sw.Write(currentBest[j] + " ");
                    }
                    sw.WriteLine();
                    sw.WriteLine("And City Locations: ");
                    for (int j = 0; j < (cities.Length / 2); j++)
                    {
                        sw.Write(cities[currentBest[j] - 1, 0] + " ");
                    }
                    sw.WriteLine();
                    for (int j = 0; j < (cities.Length / 2); j++)
                    {
                        sw.Write(cities[currentBest[j] - 1, 1] + " ");
                    }
                    sw.WriteLine();
                    sw.WriteLine("Total Time Taken Is " + time.ElapsedMilliseconds + " Milliseconds");
                    sw.WriteLine("#########################################################");
                    sw.WriteLine();

                }
                sw.Close();
            }
            Console.WriteLine("Done!");
            Console.ReadKey();
        }

        /************************************* Computes Factorial *******************************/

        public static int factorial(int num)
        {
            int factorial = 1;
            for (int i = num; i > 0; i--)
            {
                factorial *= i;
            }
            return factorial;
        }

        /*************************************  Changes permutations *******************************/

        public static int[] sqArrange(int[] square)
        {
            int i = square.Length - 1;
            int j = i - 1;
            int largest = square[(square.Length - 1)];
            while (j >= 0 && i > j)
            {

                if (square[j] < largest)
                {
                    while (square[i] < square[j])
                    {
                        i--;
                    }
                    int temp = square[i];
                    square[i] = square[j];
                    square[j] = temp;
                    j++;
                    i = square.Length - 1;
                    while (j >= 0 && i > j)
                    {
                        temp = square[i];
                        square[i] = square[j];
                        square[j] = temp;
                        j++;
                        i--;
                    }
                }
                else
                {
                    largest = square[j];
                    j--;
                }

            }
            return square;
        }

        /************************************* Create Random City Array ***************************/

        public static int[,] genRandArr(int n)
        {
            
            Random rnd = new Random();
            int[,] randArr = new int[n,2];
            int tempNum = 0;
            for(int i = 0; i < (randArr.Length/2); i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    tempNum = rnd.Next(0, 1000);
                    randArr[i,j] = tempNum;
                }
            }
            return randArr;

        }
        /************************************* Check distances in array ***************************/
        public static double checkDist(int[,] cities, int[] numof, int num)
        {
            double distance = 0;
            for (long i = 0; i < (cities.Length-2)/2; i++)
            {
                
                    distance += distanceForm(cities[numof[i]-1,0], cities[numof[i]-1,1], cities[numof[i+1]-1,0], cities[numof[i+1]-1,1]);
                
                
            }
            return distance;
        }

        /************************************* distance formula ***************************/
        public static double distanceForm(int x1, int y1, int x2, int y2)
        {
            double dist = Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
            return dist;
        }

        /************************************ Checks which distance is bigger ****************************/

        /*************************************  Prints line breaks *******************************/

        public static void printStuff()
        {

            Console.WriteLine();
            Console.WriteLine("=================================================================================================================================");
            Console.WriteLine();
        }
    }

}
