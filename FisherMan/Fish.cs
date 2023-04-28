using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FisherMan
{
    internal class Fish
    {
        private string Type { get; set; }
        private double Weight { get; set; }
        public double Difficulty { get; set; }

        public Fish()
        {

        }

        public Fish(string type, double weight, double difficulty)
        {
            Type = type;
            Weight = weight;
            Difficulty = difficulty;
        }

        public double getTotalPoints()
        {
            if(Weight >= 0)
            {
                return Weight * Difficulty;
            }
            else
            {
                return Weight;
            }
        }

        public void print()
        {
            Console.WriteLine("Fish type: {0}\nWeight: {1}\nCatch difficulty: {2}\n", Type,Weight,Difficulty);

        }
    }
}
