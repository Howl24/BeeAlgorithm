using System;

namespace AlgorithmTest
{
    class Program
    {
        public static void Main()
        {
            Test t1 = new Test();
            t1.ReadData("test1.txt");

            BeeAlgorithm ba = new BeeAlgorithm(10, 5, 5, 1,3, t1);
            ba.Assign(t1);
            Console.Read();
        }

    }
}
