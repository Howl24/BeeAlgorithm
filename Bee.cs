using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Falta divisiones entre 0

namespace TestAlgoritmo
{
  class Abeja
  {

    public static int num_puestos;
    public static int num_empleados;
    public static int[] ordenes;// [puestos]
    public static double[, ] tareas; //[empleados, puestos]
    public static double[, ] costo_asignacion; // [empleados, puestos]
    public static double coeficiente_penalidad = 1;
    public static double tam_cadena_reemplazos;

    public List<List<int>> empleados_asignados; // [puestos, empleados]
    public double fitness;
    public static Random rand;

    public Abeja(){
    }
    public Abeja(Abeja abeja){
      this.AsignarSolucion(abeja.empleados_asignados);
      this.CalcularFitness();
    }

    public static double[, ] CalcularCostos(double[, ] tareas, double[, ] roturas){
      double[, ] costos = new double[num_empleados, num_puestos];
      for (int i=0;i<num_empleados;i++){
        for (int j=0;j<num_puestos;j++){
          costos[i, j] = (1+roturas[i, j])/(1+tareas[i, j]);
        }
      }
      return costos;
    }

    public void AsignarSolucion(List<List<int>> sol){
      empleados_asignados = new List<List<int>>();
      for (int i=0;i<sol.Count;i++){
        empleados_asignados.Add(new List<int>());
        for (int j=0;j<sol[i].Count;j++){
          int empleado = sol[i][j];
          empleados_asignados[i].Add(empleado);
        }
      }
    }


    public static void ConfigurarDatos(Test test, int tam_cadena_reemplazos){
      Abeja.num_puestos = test.num_puestos;
      Abeja.num_empleados = test.num_empleados;
      Abeja.ordenes = test.ordenesxpuesto;
      Abeja.tareas = test.tareas; // Tareas realizadas por un empleado en un puesto [empleado, puesto]
      Abeja.costo_asignacion = CalcularCostos(test.tareas, test.roturas);
      Abeja.tam_cadena_reemplazos = tam_cadena_reemplazos;
    }

    //Suceptible a Pedido, revisar test
    public double RelacionTareaOrden(int empleado, int puesto){

      double relacion = (double)tareas[empleado, puesto]/(double)ordenes[puesto];
      //double relacion = 1/costo_asignacion[empleado, puesto];
      return relacion;
    }

    public Dictionary<int, double> CalcularProbabilidadesAsignacion(int empleado, List<int> posibles_puestos){

      //Console.WriteLine("Empleado {0}", empleado);
      Dictionary<int, double> prob_asignacion = new Dictionary<int, double>();

      double max = 0;
      double min = 100000;
      for (int i=0;i<posibles_puestos.Count;i++){
        int puesto = posibles_puestos[i];
        max = Math.Max(max, RelacionTareaOrden(empleado, puesto));
        min = Math.Min(min, RelacionTareaOrden(empleado, puesto));
      }

      double total = 0;
      for (int i=0;i<posibles_puestos.Count;i++){
        int puesto = posibles_puestos[i];
        total += (RelacionTareaOrden(empleado, puesto));
      }

      for (int i=0;i<posibles_puestos.Count;i++){
        int puesto = posibles_puestos[i];
        prob_asignacion[puesto] = RelacionTareaOrden(empleado, puesto)/total;
        //Console.WriteLine("{0} {1}", RelacionTareaOrden(empleado, puesto), prob_asignacion[puesto]);
      }
      //Console.WriteLine();

      return prob_asignacion;
    }

    public void AsignacionMegaRandom(){
      empleados_asignados = new List<List<int>>();
      for (int i=0;i<num_puestos;i++){
        empleados_asignados.Add(new List<int>());
      }

      List<int> no_visitados = new List<int>();
      for (int i=0;i<num_empleados;i++){
        no_visitados.Add(i);
      }

      for (int puesto=0;puesto<num_puestos;puesto++){
        if (puesto == 1) continue;
        double sum = 0;
        int cnt = 0;
        while(sum < ordenes[puesto]){
          double max = 0;
          int esc = 0;
          for (int i=0;i<no_visitados.Count;i++){
            int emp = no_visitados[i];
            if (max < tareas[emp, puesto]){
              max = tareas[emp, puesto];
              esc = emp;
            }
          }
          sum += max;
          cnt++;
          empleados_asignados[puesto].Add(esc);
          no_visitados.Remove(esc);
        }
        Console.WriteLine(cnt);
      }

      for (int i=0;i<no_visitados.Count;i++){
        int emp = no_visitados[i];
        empleados_asignados[1].Add(emp);
      }


      //int j = 0;
      //for (;j<38;j++){
      //  int min = 10000;
      //  int esc = 0;
      //  for (int i=0;i<no_visitados.Count;i++){
      //    int emp = no_visitados[i];
      //    if (min > costo_asignacion[emp, 0]){
      //      min = costo_asignacion[emp, 0];
      //      esc = emp;
      //    }
      //  }
      //  empleados_asignados[0].Add(esc);
      //  no_visitados.Remove(esc);
      //}

      //for (;j<77;j++){
      //  int min = 10000;
      //  int esc = 0;
      //  for (int i=0;i<no_visitados.Count;i++){
      //    int emp = no_visitados[i];
      //    if (min > costo_asignacion[emp, 2]){
      //      min = costo_asignacion[emp, 2];
      //      esc = emp;
      //    }
      //  }
      //  empleados_asignados[2].Add(esc);
      //  no_visitados.Remove(esc);
      //}

      //for (;j<117;j++){
      //  int min = 10000;
      //  int esc = 0;
      //  for (int i=0;i<no_visitados.Count;i++){
      //    int emp = no_visitados[i];
      //    if (min > costo_asignacion[emp, 3]){
      //      min = costo_asignacion[emp, 3];
      //      esc = emp;
      //    }
      //  }
      //  empleados_asignados[3].Add(esc);
      //  no_visitados.Remove(esc);
      //}

      //for (;j<135;j++){
      //  int min = 10000;
      //  int esc = 0;
      //  for (int i=0;i<no_visitados.Count;i++){
      //    int emp = no_visitados[i];
      //    if (min > costo_asignacion[emp, 4]){
      //      min = costo_asignacion[emp, 4];
      //      esc = emp;
      //    }
      //  }
      //  empleados_asignados[4].Add(esc);
      //  no_visitados.Remove(esc);
      //}

      //for (;j<155;j++){
      //  int min = 10000;
      //  int esc = 0;
      //  for (int i=0;i<no_visitados.Count;i++){
      //    int emp = no_visitados[i];
      //    if (min > costo_asignacion[emp, 5]){
      //      min = costo_asignacion[emp, 5];
      //      esc = emp;
      //    }
      //  }
      //  empleados_asignados[5].Add(esc);
      //  no_visitados.Remove(esc);
      //}

      //for (;j<400;j++){
      //  int min = 10000;
      //  int esc = 0;
      //  for (int i=0;i<no_visitados.Count;i++){
      //    int emp = no_visitados[i];
      //    if (min > costo_asignacion[emp, 1]){
      //      min = costo_asignacion[emp, 1];
      //      esc = emp;
      //    }
      //  }
      //  empleados_asignados[1].Add(esc);
      //  no_visitados.Remove(esc);
      //}

      //38

      ////39
      //for (;j<77;j++){
      //  empleados_asignados[2].Add(j);
      //}

      ////40
      //for (;j<117;j++){
      //  empleados_asignados[3].Add(j);
      //}

      ////18
      //for (;j<135;j++){
      //  empleados_asignados[4].Add(j);
      //}

      ////20
      //for (;j<155;j++){
      //  empleados_asignados[5].Add(j);
      //}

      //for (;j<400;j++){
      //  empleados_asignados[1].Add(j);
      //}
    }

    public void AsignacionSuperRandom(){
      empleados_asignados = new List<List<int>>();

      for (int i=0;i<num_puestos;i++){
        empleados_asignados.Add(new List<int>());
      }

      for (int i=0;i<num_empleados;i++){
        int rnd = rand.Next(0, num_puestos);
        empleados_asignados[rnd].Add(i);
      }
    }

    public void AsignacionRandom(){
      empleados_asignados = new List<List<int>>();
      List<List<int>> posibles_puestos = new List<List<int>>();
      List<int> no_visitados = new List<int>();

      for (int i=0;i<num_puestos;i++){
        empleados_asignados.Add(new List<int>());
      }

      for (int i=0;i<num_empleados;i++){
        no_visitados.Add(i);
      }

      for (int empleado=0;empleado<num_empleados;empleado++){
        posibles_puestos.Add(new List<int>());

        for (int puesto=0;puesto<num_puestos;puesto++){
          posibles_puestos[empleado].Add(puesto);
        }
      }

      while( no_visitados.Count != 0 ){
        int rnd_idx = rand.Next(0, no_visitados.Count);
        int empleado = no_visitados[rnd_idx];
        Dictionary<int, double> prob_asignacion = CalcularProbabilidadesAsignacion(empleado, posibles_puestos[empleado]);

        double rnd = rand.NextDouble();
        double cur_sum = 0;
        int puesto_escogido = 1;
        for (int j=0;j<posibles_puestos[empleado].Count;j++){
          int puesto = posibles_puestos[empleado][j];
          cur_sum += prob_asignacion[puesto];
          if (cur_sum>=rnd){
            puesto_escogido = puesto;
            break;
          }
        }

        empleados_asignados[puesto_escogido].Add(empleado);
        no_visitados.Remove(empleado);

        double total_asignado = 0;
        for (int i=0;i<empleados_asignados[puesto_escogido].Count;i++){
          int emp = empleados_asignados[puesto_escogido][i];
          total_asignado += tareas[emp, puesto_escogido];
        }

        if (total_asignado > ordenes[puesto_escogido] ){
          for (int i=0;i<posibles_puestos.Count;i++){
            posibles_puestos[i].Remove(puesto_escogido);
          }
        }
      }
    }

    public void ImprimirSolucion(){
      for (int i=0;i<empleados_asignados.Count;i++){
        double sum_tareas = 0;
        for (int j=0;j<empleados_asignados[i].Count;j++){
          int emp = empleados_asignados[i][j];
          sum_tareas += tareas[emp, i];
        }
        double falta = Math.Max(0,ordenes[i] - sum_tareas);

        Console.WriteLine("Puesto: {0} Falta: {1}", i, falta);
        Console.Write(" Empleados: ");
        for (int j=0;j<empleados_asignados[i].Count;j++){
          Console.Write("{0} ", empleados_asignados[i][j]);
        }
        Console.WriteLine();
      }
      Console.WriteLine("Fitness: {0}", fitness);
      Console.WriteLine();
    }

    public void CalcularFitness(){
      fitness = Fitness(empleados_asignados);
    }

    public double Penalidad(){
      double penalidad = 0;
      for (int puesto=0;puesto<empleados_asignados.Count;puesto++){
        double sum_tareas = 0;
        for (int j=0;j<empleados_asignados[puesto].Count;j++){
          int empleado = empleados_asignados[puesto][j];
          sum_tareas += tareas[empleado, puesto];
        }

        penalidad += Math.Max(0, ordenes[puesto] - sum_tareas);
      }
      penalidad *= coeficiente_penalidad;
      return penalidad;
    }

    public double Fitness(List<List<int>> empleados_asignados){
      double fitness = 0;

      for (int puesto =0;puesto<empleados_asignados.Count;puesto++){
        for (int j=0;j<empleados_asignados[puesto].Count;j++){
          int empleado = empleados_asignados[puesto][j];
          fitness += costo_asignacion[empleado, puesto];
        }
      }

      double penalidad = 0;
      for (int puesto=0;puesto<empleados_asignados.Count;puesto++){
        double sum_tareas = 0;
        for (int j=0;j<empleados_asignados[puesto].Count;j++){
          int empleado = empleados_asignados[puesto][j];
          sum_tareas += tareas[empleado, puesto];
        }

        penalidad += Math.Max(0, ordenes[puesto] - sum_tareas);
      }
      penalidad *= coeficiente_penalidad;
      fitness += penalidad;

      return fitness;
    }

    public Abeja Vecino(){

      Abeja vecino = new Abeja();
      vecino.AsignarSolucion(empleados_asignados);

      int puesto = -1;
      while(puesto==-1){
        puesto = rand.Next(0, vecino.empleados_asignados.Count);
        if (vecino.empleados_asignados[puesto].Count == 0){
          puesto = -1;
        }
      }
      int rnd = rand.Next(0, vecino.empleados_asignados[puesto].Count);
      int empleado = vecino.empleados_asignados[puesto][rnd];

      vecino.empleados_asignados[puesto].Remove(empleado);

      int puesto_escogido = puesto;
      double min_costo = 10000;
      for (int nuevo_puesto=0;nuevo_puesto<vecino.empleados_asignados.Count;nuevo_puesto++){
        if (nuevo_puesto!=puesto){
          double nuevo_costo = costo_asignacion[empleado, nuevo_puesto];

          double penalidad = 0;
          double sum_tareas = 0;
          for (int i=0;i<vecino.empleados_asignados[nuevo_puesto].Count;i++){
            int emp = vecino.empleados_asignados[nuevo_puesto][i];
            sum_tareas += tareas[emp, nuevo_puesto];
          }
          sum_tareas += tareas[empleado, nuevo_puesto];
          penalidad += Math.Max(0, ordenes[puesto] - sum_tareas);
          penalidad *= coeficiente_penalidad;
          nuevo_costo += penalidad;

          if (min_costo > nuevo_costo){
            min_costo = nuevo_costo;
            puesto_escogido = nuevo_puesto;
          }
        }
      }

      vecino.empleados_asignados[puesto_escogido].Add(empleado);
      vecino.CadenaDeReemplazos(puesto);

      vecino.CalcularFitness();
      return vecino;
    }

    public void CompararConVecino(){
      Abeja vecino = Vecino();
      if (vecino.fitness < this.fitness){
        this.AsignarSolucion(vecino.empleados_asignados);
        this.fitness = vecino.fitness;
      }
    }

    public void CompararConVecindario(bool doble_shift ){
      List<int> visitados = new List<int>();

      for (int i=0;i<empleados_asignados.Count;i++){
        for (int j=0;j<empleados_asignados[i].Count;j++){
          int empleado = empleados_asignados[i][j];
          if (!(visitados.Contains(empleado))){
            visitados.Add(empleado);
            Abeja vecino = Vecino(i, empleado, doble_shift);
            if (vecino.fitness < this.fitness){
              this.AsignarSolucion(vecino.empleados_asignados);
              this.fitness = vecino.fitness;
            }
          }
        }
      }

    }

    public Abeja Vecino(int puesto, int empleado, bool doble_shift){
      Abeja vecino = new Abeja();
      vecino.AsignarSolucion(empleados_asignados);

      vecino.empleados_asignados[puesto].Remove(empleado);
      int puesto_escogido = puesto;
      double min_costo = 100000;
      for (int nuevo_puesto=0;nuevo_puesto<vecino.empleados_asignados.Count;nuevo_puesto++){
        if (nuevo_puesto!=puesto){
          double nuevo_costo = costo_asignacion[empleado, nuevo_puesto];

          double penalidad = 0;
          double sum_tareas = 0;
          for (int i=0;i<vecino.empleados_asignados[nuevo_puesto].Count;i++){
            int emp = vecino.empleados_asignados[nuevo_puesto][i];
            sum_tareas += tareas[emp, nuevo_puesto];
          }
          sum_tareas += tareas[empleado, nuevo_puesto];
          penalidad += Math.Max(0, ordenes[puesto] - sum_tareas);
          penalidad *= coeficiente_penalidad;
          nuevo_costo += penalidad;

          if (min_costo > nuevo_costo){
            min_costo = nuevo_costo;
            puesto_escogido = nuevo_puesto;
          }
        }
      }

      if (doble_shift){
        vecino.Reemplazo(puesto);
      }


      vecino.empleados_asignados[puesto_escogido].Add(empleado);
      vecino.CalcularFitness();
      return vecino;

    }

    public int Reemplazo(int puesto){

      double falta_puesto = ordenes[puesto] - CantidadAsignada(puesto);
      if (falta_puesto > 0){

        double min_costo = 1000;
        int nuevo_emp = -1;
        int puesto_nuevo_emp = -1;

        for (int i=0;i<empleados_asignados.Count;i++){
          if (i!= puesto){
            for (int j=0;j<empleados_asignados[i].Count;j++){
              int emp = empleados_asignados[i][j];
              double nuevo_falta = 0;

              if (CantidadAsignada(i) > ordenes[i]){
                  empleados_asignados[i].Remove(emp);
                  nuevo_falta = ordenes[i] - CantidadAsignada(i);
                  empleados_asignados[i].Add(emp);
              }else{
                  nuevo_falta = tareas[emp, i];
              }


              double costo = falta_puesto - tareas[emp, puesto] + costo_asignacion[emp, puesto] + nuevo_falta - costo_asignacion[emp, i];
              if (min_costo >= costo){
                min_costo = costo;
                nuevo_emp = emp;
                puesto_nuevo_emp = i;
              }
            }
          }
        }

        empleados_asignados[puesto_nuevo_emp].Remove(nuevo_emp);
        empleados_asignados[puesto].Add(nuevo_emp);
        return puesto_nuevo_emp;

      }
      return -1;
    }

    public double CantidadAsignada(int puesto){
      double suma = 0;
      for (int i=0;i<empleados_asignados[puesto].Count;i++){
        int emp = empleados_asignados[puesto][i];
        suma += tareas[emp, puesto];
      }
      return suma;
    }

    public void CadenaDeReemplazos(int puesto){
      int puesto_modificado = puesto;
      for (int i=0;i<tam_cadena_reemplazos;i++){
        if (puesto_modificado != -1){
          puesto_modificado = Reemplazo(puesto_modificado);
        }else{
          break;
        }
      }
    }


  }
}
