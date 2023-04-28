using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FisherMan
{
    internal class Fisherman
    {
        public string Name { get; set; }
        public Stack<Fish> Basket { get; set; }
        public Queue<Fish> Net { get; set; }
        public List<Fish> Junk { get;set; }
        public double fishingScore { get; set; }
        public double nettingScore { get; set; }   

        public Fisherman(string name) 
        {
            Name = name;
            Basket = new Stack<Fish>();
            Net = new Queue<Fish>();
            Junk = new List<Fish>();
            initJunk();
        }
        
        public Fisherman(string name, double fishingScore, double nettingScore)
        {
            Name = name;
            Basket = new Stack<Fish>();
            Net = new Queue<Fish>();
            Junk = new List<Fish>();
            this.fishingScore = fishingScore;
            this.nettingScore = nettingScore;
            initJunk();
        }

        private void initJunk()
        {
            Random rnd = new Random();
            int random = 0;
            Dictionary<string,double> junkItems = new Dictionary<string,double>();
            junkItems.Add("Boot", -0.5);
            junkItems.Add("Seaweed", -0.1);
            junkItems.Add("Bottle", -0.3);
            junkItems.Add("Wheel", -0.9);
            junkItems.Add("Tin Can", -0.2);
            junkItems.Add("Plastic Bag", -0.4);
            Dictionary<string, double>.KeyCollection keyColl = junkItems.Keys;
            Dictionary<string,double>.ValueCollection valueColl = junkItems.Values;


            
            for (int i = 0; i < 100; i++)
            {
               random = rnd.Next(0, junkItems.Count);
               Junk.Add(new Fish(junkItems.Keys.ElementAt(random),junkItems.Values.ElementAt(random), 0));
               
            }

        }

        private Fish getJunk()
        {
            Random rnd = new Random();
            int random = 0;

            random = rnd.Next(0, Junk.Count);

            return Junk[random];
        }


        public static double GetPseudoDoubleWithinRange(double lowerBound, double upperBound)
        {
            var random = new Random();
            var rDouble = random.NextDouble();
            var rRangeDouble = rDouble * (upperBound - lowerBound) + lowerBound;
            return rRangeDouble;
        }
        public double fishing(Fish f)
        {
            Random rnd = new Random();
            double luck = 0;

            luck = GetPseudoDoubleWithinRange(0.5, 5.5);

            if (luck >= f.Difficulty)
            {
                Basket.Push(f);
                fishingScore = f.getTotalPoints();
                return f.getTotalPoints();
            }
            else if (luck < f.Difficulty)
            {
                if (rnd.Next(0, 1) > 0)
                {

                    Basket.Push(getJunk());
                    fishingScore = getJunk().getTotalPoints();
                    return getJunk().getTotalPoints();

                }
            }
            return 0.0;
            
        }

        public void netting(List<Fish> waters)
        {
            int times = 0;
            times = (int)GetPseudoDoubleWithinRange(0.1, 49);

            for (int i = 0; i < times; i++) 
            {
                if (fishing(waters[i]) > 0)
                {
                    Net.Enqueue(Basket.Pop());
                    nettingScore = waters[i].getTotalPoints();  
                }else if (fishing(waters[i]) < 0)
                {
                    Net.Enqueue(Junk[i]);
                    nettingScore = waters[i].getTotalPoints();  
                }
            }
        } 

        public void printBasket()
        {
            if (Basket != null)
            {
                foreach (Fish f1 in Basket) 
                {
                    Console.WriteLine($"{Name}'s basket contains.");
                    f1.print();
                }
            }
        }

        public void printNet()
        {
            if (Net != null)
            {
                foreach(Fish f2 in Net)
                {
                    Console.WriteLine($"{Name}'s net contains.");
                    f2.print();
                }
            }
        }

        public void initReset()
        {
            Name = " ";
            Junk.Clear();
            Basket.Clear();
            Net.Clear();
            fishingScore = 0;
            nettingScore = 0;   
        }
    }
}
