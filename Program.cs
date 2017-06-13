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

                Abeja.ConfigurarDatos(t1,7);
                Abeja.rand = rand;
                AlgoritmoAbejas.rand = rand;

                AlgoritmoAbejas ba = new AlgoritmoAbejas(20, 100, 5, 50);
                ba.Asignacion();
                return ;
            }
        }
    }
}
