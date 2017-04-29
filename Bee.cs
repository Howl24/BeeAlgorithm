using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAlgoritmo
{
  class Abeja
  {
    public List<List<int>> solucion;

    public Abeja(){
      Console.WriteLine("Abeja created");
    }

    public void ImprimirSolucion(){
      for (int i=0;i<solucion.Count;i++){
        Console.WriteLine("Puesto: {0}", i);
        Console.Write(" Empleados: ");
        for (int j=0;j<solucion[i].Count;j++){
          Console.Write("{0} ", solucion[i][j]);
        }
        Console.WriteLine();
      }
    }

  }
}
