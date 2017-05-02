using System;
using System.Collections.Generic;

namespace TestAlgoritmo{

  class AlgoritmoAbejas{

      public static Random rand;

      public int num_abejas_ocupadas;
      public int num_abejas_espera;
      public int num_abejas_explo;
      public int max_iteraciones;
      List<Abeja> abejas_ocupadas;

      public AlgoritmoAbejas(int num_abejas_ocupadas,
                             int num_abejas_espera,
                             int num_abejas_explo,
                             int max_iteraciones){

          this.num_abejas_ocupadas = num_abejas_ocupadas;
          this.num_abejas_espera = num_abejas_espera;
          this.num_abejas_explo = num_abejas_explo; this.max_iteraciones = max_iteraciones;
      }

      public void InicializarAbejasOcupadas(){
        abejas_ocupadas = new List<Abeja>();

        for (int i=0;i<num_abejas_ocupadas;i++){
          Abeja abeja = new Abeja();
          abeja.AsignacionSuperRandom();
          abeja.CalcularFitness();
          //Console.WriteLine(abeja.fitness);
          //abeja.ImprimirSolucion();

          abejas_ocupadas.Add(abeja);
        }

      }

      public Dictionary<int, double> CalcularProbabilidadesEspera(){
        Dictionary<int, double> prob_espera = new Dictionary<int, double>();

        double total = 0;
        for (int i=0;i<abejas_ocupadas.Count;i++){
          double fitness = abejas_ocupadas[i].fitness;
          total += 1/fitness;
        }

        for (int i=0;i<abejas_ocupadas.Count;i++){
          double fitness = abejas_ocupadas[i].fitness;
          prob_espera[i] = (1/fitness)/total;
          //Console.WriteLine("Fitness: {0}, Prob: {1}", fitness, prob_espera[i]);
        }
        return prob_espera;
      }

      public void CompararConEspera(Dictionary<int, double> prob_espera){
        //Asignacion de abejas en espera
        int[] cnt_espera = new int[abejas_ocupadas.Count];
        for (int i=0;i<num_abejas_espera;i++){
          double rnd = rand.NextDouble();
          double cur_sum = 0;
          int abeja_escogida = 0;
          for (int j=0;j<abejas_ocupadas.Count;j++){
            cur_sum += prob_espera[j];
            if (cur_sum >= rnd){
              abeja_escogida = j;
              break;
            }
          }
          cnt_espera[abeja_escogida]++;
        }

        for (int i=0;i<abejas_ocupadas.Count;i++){
          for (int j=0;j<cnt_espera[i];j++){
            abejas_ocupadas[i].CompararConVecino();
          }
        }

      }

      public Abeja MejorAbeja(Abeja ex_mejor){
        Abeja mejor_abeja = abejas_ocupadas[0];
        for (int i=0;i<abejas_ocupadas.Count;i++){
          //abejas_ocupadas[i].ImprimirSolucion();
          if (abejas_ocupadas[i].fitness < mejor_abeja.fitness){
            mejor_abeja = abejas_ocupadas[i];
          }
        }

        if (mejor_abeja.fitness > ex_mejor.fitness){
            mejor_abeja = ex_mejor;
        }

        return mejor_abeja;
      }


      public void CompararConExploradoras(){
        List<int> lista_negra = new List<int>();

        for (int i=0;i<num_abejas_explo;i++){
          int peor_abeja = -1;
          for (int j=0;j<abejas_ocupadas.Count;j++){
            if (!lista_negra.Contains(j)){
              if (peor_abeja == -1) peor_abeja = j;
              if (abejas_ocupadas[j].fitness > abejas_ocupadas[peor_abeja].fitness){
                peor_abeja = j;
              }
            }
          }
          lista_negra.Add(peor_abeja);
        }

        for (int i=0;i<lista_negra.Count;i++){
          Abeja ocupada = abejas_ocupadas[lista_negra[i]];

          Abeja exploradora = new Abeja();
          exploradora.AsignacionSuperRandom();
          exploradora.CalcularFitness();
          exploradora.CompararConVecindario(false);

          for (int j=0;j<5;j++){
            exploradora.CompararConVecino();
          }

          //Console.WriteLine("Ex3: {0}", exploradora.fitness);

          if (exploradora.fitness - ocupada.fitness < 4){
            //Console.WriteLine("La exploradora encontro algo xD");
            ocupada.AsignarSolucion(exploradora.empleados_asignados);
            ocupada.fitness = exploradora.fitness;
          }else{
            exploradora = null;
          }
        }
      }

      public Abeja Asignacion(){
        InicializarAbejasOcupadas();
        Abeja mejor_abeja = new Abeja();
        mejor_abeja.fitness = 100000;

        for (int i=0;i<max_iteraciones;i++){
          //Console.WriteLine("Iteracion {0}:", i+1);
          for (int j=0;j<abejas_ocupadas.Count;j++){
            //Console.WriteLine("Fit {0}:", abejas_ocupadas[j].fitness);
            abejas_ocupadas[j].CompararConVecindario(false);
            Dictionary<int, double> prob_espera = CalcularProbabilidadesEspera();
            CompararConEspera(prob_espera);

          }
          mejor_abeja = MejorAbeja(mejor_abeja);
          //mejor_abeja.ImprimirSolucion();
          //Console.WriteLine("Mejor Fitness: {0}", mejor_abeja.fitness);

          CompararConExploradoras();
        }
        Console.WriteLine("Mejor Solucion: {0}", mejor_abeja.fitness);
        //mejor_abeja.ImprimirSolucion();
        return mejor_abeja;
      }
  }
}
