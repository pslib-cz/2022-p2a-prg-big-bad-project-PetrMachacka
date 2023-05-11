using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotsABoxes
{
    public static class BaseMechanics
    {
        public static string[,] GenerateArray(int rows, int columns)
        {
            string[,] array = new string[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    array[i, j] = "o";
                }
            }
            return array;
        }
        public static void ReGenerate(string[,] array2D)
        {
            var sirka = array2D.GetLength(0); 
            var delka = array2D.GetLength(1); 
            for (int i = 0; i < sirka; i++)
            {
                for (int j = 0; j < delka; j++)
                {
                    Console.Write(array2D[i, j] + "  ");
                }
                Console.WriteLine();
            }
        }
    }
}
