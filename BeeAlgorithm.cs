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
        double max = 0;
        double min = 10000;
        for (int i=0;i<abejas_ocupadas.Count;i++){
          max = Math.Max(max, 1/abejas_ocupadas[i].fitness);
          min = Math.Min(min, 1/abejas_ocupadas[i].fitness);
        }

        double total = 0;
        for (int i=0;i<abejas_ocupadas.Count;i++){
          double fitness = abejas_ocupadas[i].fitness;
          total += (1/fitness - min)/(max-min);
        }

        for (int i=0;i<abejas_ocupadas.Count;i++){
          double fitness = abejas_ocupadas[i].fitness;
          prob_espera[i] = ((1/fitness - min)/(max-min))/total;
          //Console.WriteLine("Fitness: {0}, Prob: {1}", fitness, prob_espera[i]);
        }
        return prob_espera;
      }

      public void CompararConEspera(Dictionary<int, double> prob_espera){
        //Asignacion de abejas en espera
        int[] cnt_espera = new int[abejas_ocupadas.Count];

        for (int i=0;i<abejas_ocupadas.Count;i++){
          prob_espera[i] = 1;
          cnt_espera[i] = (int)(num_abejas_espera*prob_espera[i]);
        }

        for (int i=0;i<abejas_ocupadas.Count;i++){
          Console.WriteLine(abejas_ocupadas[i].fitness);
          Console.WriteLine(prob_espera[i]);
          Console.WriteLine(cnt_espera[i]);
          for (int j=0;j<cnt_espera[i];j++){
            abejas_ocupadas[i].CompararConVecino();
          }
        }

      }

      public Abeja MejorAbeja(){
        Abeja mejor_abeja = abejas_ocupadas[0];
        for (int i=0;i<abejas_ocupadas.Count;i++){
          //abejas_ocupadas[i].ImprimirSolucion();
          if (abejas_ocupadas[i].fitness < mejor_abeja.fitness){
            mejor_abeja = abejas_ocupadas[i];
          }
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

        int k=0;
        for (int i=0;i<num_abejas_explo;i++){
          Abeja exploradora = new Abeja();
          exploradora.AsignacionRandom();
          exploradora.CalcularFitness();
          exploradora.CompararConVecindario(0);
          exploradora.CompararConVecindario(1);

          Abeja ocupada = abejas_ocupadas[lista_negra[k]];
          if (exploradora.fitness < ocupada.fitness){
            Console.WriteLine("La exploradora encontro algo xD");
            ocupada.AsignarSolucion(exploradora.empleados_asignados);
            ocupada.fitness = exploradora.fitness;
            k++;
          }
        }
      }

      public Abeja Asignacion(){
        InicializarAbejasOcupadas();
        Abeja mejor_abeja = new Abeja();

        for (int i=0;i<max_iteraciones;i++){
          //Console.WriteLine("Iteracion {0}:", i+1);
          for (int j=0;j<abejas_ocupadas.Count;j++){
            //Console.WriteLine("Fit {0}:", abejas_ocupadas[j].fitness);
            abejas_ocupadas[j].CompararConVecindario(0);
            abejas_ocupadas[j].CompararConVecindario(1);
          }

          Dictionary<int, double> prob_espera = CalcularProbabilidadesEspera();
          CompararConEspera(prob_espera);


          mejor_abeja = MejorAbeja();
          mejor_abeja.ImprimirSolucion();
          //Console.WriteLine("Mejor Fitness: {0}", mejor_abeja.fitness);

          CompararConExploradoras();
        }
        Console.WriteLine("Mejor Solucion: {0}", mejor_abeja.fitness);
        mejor_abeja.ImprimirSolucion();
        return mejor_abeja;
      }
  }
}
