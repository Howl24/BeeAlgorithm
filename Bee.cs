using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAlgoritmo
{
  class Abeja
  {

    public static int num_puestos;
    public static int num_empleados;
    public static int[] ordenes;
    public static int[, ] tareas;
    public static int[, ] costo_asignacion;

    public List<List<int>> empleados_asignados;

    public Abeja(){
      Console.WriteLine("Abeja created");
    }

    public static int[, ] CalcularCostos(int[, ] tareas, int[, ] roturas){
      int[, ] costos = new int[num_empleados, num_puestos];
      for (int i=0;i<num_empleados;i++){
        for (int j=0;j<num_puestos;j++){
          costos[i, j] = (1+roturas[i, j])/(1+tareas[i, j]);
        }
      }
      return costos;
    }

    public static void ConfigurarDatos(Test test){
      Abeja.num_puestos = test.num_puestos;
      Abeja.num_empleados = test.num_empleados;
      Abeja.ordenes = test.ordenesxpuesto;
      Abeja.tareas = test.tareas; // Tareas realizadas por un empleado en un puesto [empleado, puesto]
      Abeja.costo_asignacion = CalcularCostos(test.tareas, test.roturas);
    }

    public double RelacionTareaOrden(int empleado, int puesto){

      double relacion = (double)tareas[empleado, puesto]/(double)ordenes[puesto];
      return relacion;
    }

    public Dictionary<int, double> CalcularProbabilidadesAsignacion(int empleado, List<int> posibles_puestos){
      /*
       * La probabilidad se calcula mediante la formula
       * pij = (bij/ai)/Sum(bij/ai)
       * donde bij/ai es una relacion tarea-orden
       * esto significa que porcentaje de la orden se puede
       * realizar si se asigna al empleado i al puesto j
       *
       * La idea del GRAH es darle una mayor probabilidad
       * de asignacion al empleado que tenga un mayor porcentaje.
       * Esto debido a que indica un mayor acercamiento a cumplir
       * la orden pedida.
       *
       */
      
      Dictionary<int, double> prob_asignacion = new Dictionary<int, double>();

      double total = 0;
      for (int i=0;i<posibles_puestos.Count;i++){
        int puesto = posibles_puestos[i];
        total += RelacionTareaOrden(empleado, puesto);
      }

      for (int i=0;i<posibles_puestos.Count;i++){
        int puesto = posibles_puestos[i];
        prob_asignacion[i] = RelacionTareaOrden(empleado, puesto)/total;
      }

      return prob_asignacion;
    }



    public void AsignacionRandom(){
      empleados_asignados = new List<List<int>>();
      List<List<int>> posibles_puestos = new List<List<int>>();

      for (int i=0;i<num_puestos;i++){
        empleados_asignados.Add(new List<int>());
      }

      //Initialize possible agents per task
      //All agents can be chosen at the beginning
      for (int empleado=0;empleado<num_empleados;empleado++){
        posibles_puestos.Add(new List<int>());

        for (int puesto=0;puesto<num_puestos;puesto++){
          posibles_puestos[empleado].Add(puesto);
        }
      }


      //GRAH
      Random rand = new Random();
      for (int empleado=0;;){
        Dictionary<int, double> prob_asignacion = CalcularProbabilidadesAsignacion(empleado, posibles_puestos[empleado]);

        double rnd = rand.NextDouble();
        double cur_sum = 0;
        int puesto_escogido = 0;
        for (int j=0;j<posibles_puestos[empleado].Count;j++){
          int puesto = posibles_puestos[empleado][j];
          cur_sum += prob_asignacion[puesto];
          if (cur_sum>rnd){
            puesto_escogido = puesto;
            break;

          }
        }

        empleados_asignados[puesto_escogido].Add(empleado);

        empleado++;
        if (empleado<num_empleados){
          int sum_tareas = 0;
          for (int j=0;j<empleados_asignados[puesto_escogido].Count;j++){
            int emp = empleados_asignados[puesto_escogido][j];
            sum_tareas += tareas[emp, puesto_escogido];
          }
          if (sum_tareas > ordenes[puesto_escogido]){
            for (int j=0;j<posibles_puestos.Count;j++){
              posibles_puestos[j].Remove(puesto_escogido);
            }
          }
        }else{
          break;
        }
      }
    }

    public void ImprimirSolucion(){
      for (int i=0;i<empleados_asignados.Count;i++){
        Console.WriteLine("Puesto: {0}", i);
        Console.Write(" Empleados: ");
        for (int j=0;j<empleados_asignados[i].Count;j++){
          Console.Write("{0} ", empleados_asignados[i][j]);
        }
        Console.WriteLine();
      }
    }

  }
}
