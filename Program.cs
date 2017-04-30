using System;

namespace TestAlgoritmo
{
    class Program
    {
        public static void Main()
        {
            Test t1 = new Test();
            t1.LeerDatos("test2.txt");
            Abeja.ConfigurarDatos(t1);

            AlgoritmoAbejas ba = new AlgoritmoAbejas(25, 250,5, 250, 1);
            ba.Asignacion();
        }
    }
}
