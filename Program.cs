using System;

namespace TestAlgoritmo
{
    class Program
    {
        public static void Main()
        {
            Test t1 = new Test();
            t1.LeerDatos("test1.txt");
            Abeja.ConfigurarDatos(t1);


            AlgoritmoAbejas ba = new AlgoritmoAbejas(1, 5, 5, 1,3);
            ba.Asignacion();
            Console.Read();
        }
    }
}
