using System;
using System.Collections.Generic;

namespace TestAlgoritmo
{
    class Program
    {
        public static void Main()
        {
            //Random rand = new Random();
            //List<int> rnds = new List<int>();
            //for (int i=0;i<100;i++){
            //  int rnd = rand.Next(0, 100);
            //  rnds.Add(rnd);
            //}

            //for (int i=0;i<100;i++){
            //  Console.WriteLine(rnds[i]);
            //}




            Test t1 = new Test();
            t1.LeerDatos("CR400T10P.txt");
            Abeja.ConfigurarDatos(t1);

            AlgoritmoAbejas ba = new AlgoritmoAbejas(30, 100, 5, 70, 1);
            ba.Asignacion();
        }
    }
}
