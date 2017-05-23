using System;
using System.Collections.Generic;

namespace TestAlgoritmo
{
    class Program
    {
        public static void Main(string [] args)
        {
            if (args.Length == 1){
                //Lectura de archivo
                string filename = args[0];
                Test t1 = new Test();
                t1.LeerDatos(filename);

                //Inicializar datos
                Random rand = new Random();
                Abeja.ConfigurarDatos(t1,5);
                Abeja.rand = rand;
                AlgoritmoAbejas.rand = rand;

                //Iniciar algoritmo
                AlgoritmoAbejas ba = new AlgoritmoAbejas(50, 10, 5, 30);
                ba.Asignacion();
            }else{
              Console.WriteLine("Debe ingresar un archivo");
            }
        }
    }
}
