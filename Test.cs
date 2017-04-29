using System.Collections.Generic;
using System.IO;
using System;

namespace AlgorithmTest
{
    class Test
    {
        private int num_workers;
        private int num_workplaces;

        private int[, ] breaks;
        private int[, ] tasks;
        private int[] orders;

        public Test()
        {
            this.num_workers = 0;
            this.num_workplaces = 0;
        }

        public int[] ReadArray(StreamReader sr, int length)
        {
            var values = sr.ReadLine().Split();
            var array = new int[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = int.Parse(values[i]);
            }
            return array;
        }

        public int[,] ReadMatrix(StreamReader sr, int rows, int columns)
        {
            int[,] matrix = new int[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                var values = sr.ReadLine().Split();
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = int.Parse(values[j]);
                }
            }
            return matrix;
        }

        public void ReadData(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            var line = sr.ReadLine();
            var spl = line.Split();

            num_workers = int.Parse(spl[0]);
            num_workplaces = int.Parse(spl[1]);

            breaks = ReadMatrix(sr, num_workers, num_workplaces);
            tasks = ReadMatrix(sr, num_workers, num_workplaces);
            orders = ReadArray(sr, num_workplaces);

            /*
            for (int i = 0; i < num_workers; i++) {
                for (int j=0;j<num_workplaces; j++)
                {
                    Console.Write(breaks[i,j]);
                }
                Console.WriteLine();
            }
            */
        }

    }
}
