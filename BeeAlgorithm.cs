using System;
using System.Collections.Generic;

namespace TestAlgoritmo{

  class AlgoritmoAbejas{

      public int num_abejas_ocupadas;
      public int num_abejas_espera;
      public int num_abejas_explo;
      public int max_iteraciones;
      public int coeficiente_penalidad;
      public int tam_cadena_ejeccion;

      public int num_puestos;
      public int num_empleados;

      public int[] ordenes; //ordenes por puesto
      public int[, ] tareas; // tareas realizadas por un empleado en un puesto [empleado, puesto]
      public int[, ] costo_asignacion; //[task, agent]

      public AlgoritmoAbejas(int num_abejas_ocupadas,
                             int num_abejas_espera,
                             int max_iteraciones,
                             int coeficiente_penalidad,
                             int tam_cadena_ejeccion){

          this.num_abejas_ocupadas = num_abejas_ocupadas;
          this.num_abejas_espera = num_abejas_espera;
          this.max_iteraciones = max_iteraciones;
          this.coeficiente_penalidad = coeficiente_penalidad;
          this.tam_cadena_ejeccion = tam_cadena_ejeccion;
      }

      public List<Abeja> InicializarAbejasOcupadas(){
        List<Abeja> abejas_ocupadas = new List<Abeja>();

        for (int i=0;i<num_abejas_ocupadas;i++){
          Abeja abeja = new Abeja();
          abeja.AsignacionRandom();
          abeja.ImprimirSolucion();
          abejas_ocupadas.Add(abeja);
        }
        return abejas_ocupadas;
      }

      public Abeja Asignacion(){
        List<Abeja> abejas_ocupadas = InicializarAbejasOcupadas();

        Console.WriteLine("Assign done");
        return abejas_ocupadas[0]; // Best abeja returned, 0 just for now
      }
  }
}
