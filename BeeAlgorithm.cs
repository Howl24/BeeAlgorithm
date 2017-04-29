using System;
using System.Collections.Generic;

namespace AlgorithmTest{

  class BeeAlgorithm{

      public int num_employer_bees;
      public int num_onlooker_bees;
      public int max_iterations;
      public int penalty_coefficient;
      public int ejection_chain_length;

      public int num_agents;
      public int num_tasks;

      public int[] agent_resources;
      public int[, ] task_resources;
      public int[, ] task_costs;

      public BeeAlgorithm(int num_employer_bees,
                          int num_onlooker_bees,
                          int max_iterations,
                          int penalty_coefficient,
                          int ejection_chain_length,
                          Test test){

          this.num_employer_bees = num_employer_bees;
          this.num_onlooker_bees = num_onlooker_bees;
          this.max_iterations = max_iterations;
          this.penalty_coefficient = penalty_coefficient;
          this.ejection_chain_length = ejection_chain_length;

          this.num_agents = test.num_workplaces;
          this.num_tasks = test.num_workers;
          this.agent_resources = test.orders;
          this.task_resources = test.tasks;
          this.task_costs = CalculateCosts(test.tasks, test.breaks);
      }

      public int[, ] CalculateCosts(int[, ] tasks, int[, ] breaks){
        int[, ] costs = new int[num_tasks, num_agents];
        for (int i=0;i<num_tasks;i++){
          for (int j=0;j<num_agents;j++){
            costs[i, j] = (1+breaks[i, j])/(1+tasks[i, j]);
          }
        }
        return costs;
      }

      public List<Bee> InitializeEmployerBees(){
        List<Bee> employer_bees = new List<Bee>();

        for (int i=0;i<num_employer_bees;i++){
          Bee bee = new Bee();
          employer_bees.Add(bee);
        }
        return employer_bees;
      }

      public Bee Assign(Test t1){
        List<Bee> employer_bees = InitializeEmployerBees();
        Console.WriteLine("Assign done");
        return employer_bees[0]; // Best bee returned, 0 just for now
      }
  }
}
