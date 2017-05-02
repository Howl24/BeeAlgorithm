using System;
using System.Collections.Generic;

namespace TestAlgoritmo
{
    class Program
    {
        public static void Main(string [] args)
        {
            if (args.Length == 1){
                string filename = args[0];
                Test t1 = new Test();
                t1.LeerDatos(filename);

                Random rand = new Random();

                Abeja.ConfigurarDatos(t1,10);
                Abeja.rand = rand;
                AlgoritmoAbejas.rand = rand;

                AlgoritmoAbejas ba = new AlgoritmoAbejas(40, 100, 5, 50);
                ba.Asignacion();
                return ;
            }

          double prom = 0;
          int iter = 20;
          for (int i=0;i<iter;i++){
            Console.WriteLine("Test: {0}", i);
            Test t1 = new Test();
            t1.LeerDatos("CR100T10P.txt");

            Random rand = new Random();

            Abeja.ConfigurarDatos(t1,5);
            Abeja.rand = rand;
            AlgoritmoAbejas.rand = rand;

            AlgoritmoAbejas ba = new AlgoritmoAbejas(50, 100, 5, 20);
            prom += ba.Asignacion().fitness;
          }
          prom /= iter;
          Console.WriteLine("Promedio de Soluciones: {0}", prom);

        }
    }
}
