using System;

namespace AlgorithmTest
{
    class BeeAlgorithm
    {
        private int num_employer_bees;
        private int num_onlooker_bees;
        private int max_iterations;
        private int penalty_coefficient;
        private int ejection_chain_length;
        public Bee Assign(Test t1)
        {
            Bee bee = new Bee();
            Console.WriteLine("Assign done");
            return bee;
        }

        public BeeAlgorithm(int num_employer_bees, int num_onlooker_bees, int max_iterations, int penalty_coefficient, int ejection_chain_length)
        {
            this.num_employer_bees = num_employer_bees;
            this.num_onlooker_bees = num_onlooker_bees;
            this.max_iterations = max_iterations;
            this.penalty_coefficient = penalty_coefficient;
            this.ejection_chain_length = ejection_chain_length;
        }































    }
}
