using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotsABoxes
{
    public static class BaseMechanics
    {
        public static string[,] GenerateArray(int Rows, int Columns, bool empty)
        {
            int rows = Rows * 2 + 1;
            int columns = Columns * 2 + 1;
            string[,] array = new string[rows, columns];
            string[,] arraycolor = new string[rows, columns];
            for (int y= 0; y< rows; y++)
            {
                if (y% 2 == 0)
                {
                    for (int x = 0; x < columns; x++)
                    {
                        array[y, x] = (x % 2) > 0 ? "--" : "o";
                    }
                }
                else
                {
                    for (int x = 0; x < columns; x++)
                    {
                        array[y, x] = (x % 2) > 0 ? "  " : "|";  
                    }
                }
            }
            return empty ? arraycolor : array;
        }
        public static void ReGenerate(string[,] array2D, string[,] arraycolor)
        {
            var sirka = array2D.GetLength(0); 
            var delka = array2D.GetLength(1); 
            for (int y= 0; y< sirka; y++)
            {
                for (int x = 0; x < delka; x++)
                {
                    if (arraycolor[y, x] == "1")
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write(array2D[y, x]);
                    }
                    else
                    {
                        Console.Write(array2D[y, x]);
                    }
                    Console.ResetColor();
                }
                
                Console.WriteLine();
            }
        }
        public static void Color(string[,] arraycolor, int Y, int X)
        {
            int y = Y;
            int x= X;
            arraycolor[y, x]  = "1";
        }
    }
}
