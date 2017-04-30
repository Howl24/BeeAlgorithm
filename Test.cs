using System.Collections.Generic;
using System.IO;
using System;

namespace TestAlgoritmo
{
    class Test
    {
        public int num_empleados;
        public int num_puestos;

        public double[, ] roturas;
        public double[, ] tareas;
        public int[] ordenesxpuesto;

        public Test()
        {
            this.num_empleados = 0;
            this.num_puestos = 0;
        }

        public int[] LeerArreglo(StreamReader sr, int length)
        {
            var values = sr.ReadLine().Split();
            var array = new int[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = int.Parse(values[i]);
            }
            return array;
        }

        public double[,] LeerMatriz(StreamReader sr, int rows, int columns)
        {
            double[,] matrix = new double[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                var values = sr.ReadLine().Split();
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = double.Parse(values[j]);
                }
            }
            return matrix;
        }

        public void LeerDatos(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            var line = sr.ReadLine();
            var spl = line.Split();

            num_empleados = int.Parse(spl[0]);
            num_puestos = int.Parse(spl[1]);

            roturas = LeerMatriz(sr, num_empleados, num_puestos);
            tareas = LeerMatriz(sr, num_empleados, num_puestos);
            ordenesxpuesto = LeerArreglo(sr, num_puestos);

        }

    }
}
