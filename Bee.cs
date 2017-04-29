using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAlgoritmo
{
  class Abeja
  {
    public int num_agents;
    public int num_tasks;

    public int[, ] agent_resources;
    public int[, ] task_resources;
    public int[, ] task_cost;

    public int[,] assignment;

    public Abeja(int num_agents, int num_tasks)
    {
      this.num_agents = num_agents;
      this.num_tasks = num_tasks;
      Console.WriteLine("Abeja created");
    }

    public Abeja(){
      Console.WriteLine("Abeja created");
    }
  }
}
