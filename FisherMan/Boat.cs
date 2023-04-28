using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FisherMan
{
    internal class Boat
    {
        private string Name { get; set; }
        public Queue<Fisherman> Crew;

        public  Boat()
        {
            Crew = new Queue<Fisherman>();

        }

        public Boat(string name)
        {
            this.Name = name;
            Crew = new Queue<Fisherman>();
        }

        public void addFisherman(Fisherman f)
        {
            Crew.Enqueue(f);
        }   

        
        public void startFishingContest(List<Fish> waters, int times)
        {
            Fisherman f;

            if (Crew.Count >= 2)
            {
                for(int i =0; i< times; i++) 
                {
                    Crew.Peek().fishing(waters[i]);
                    f = Crew.Dequeue();
                    waters.Remove(waters[i]);
                }
            }
        }
        
        public void startNettingContest(List<Fish> waters)
        {
            if (Crew.Count >= 2)
            {
                for(int i =0; i< Crew.Count; i++)
                {
                    Crew.Peek().netting(waters);
                    Crew.Dequeue();
                    waters.Remove(waters[i]);
                }
                
            }
        }

        public List<Fisherman> getFishingWinner()
        {
            List <Fisherman> winners = new List<Fisherman>();

            for (int i =0; i< Crew.Count;i++)
            {
                
                winners.Add(Crew.Peek());
                Crew.Dequeue();
            }
            List <Fisherman> sortedWinners = winners.OrderBy(x => x.fishingScore).ToList();

            return sortedWinners;

        }

        public List<Fisherman> getNettingWinner()
        {
            List<Fisherman> winners = new List<Fisherman>();
           
            for(int i = 0; i < Crew.Count; i++) 
            {
                
                winners.Add(Crew.Peek());   
                Crew.Dequeue();
            }
            List<Fisherman> sortedWinners = winners.OrderBy(x => x.nettingScore).ToList();

            for (int i = 0;i < sortedWinners.Count; i++)
            {
                Console.WriteLine("Netting winners " + sortedWinners[i]);
            }
            
            Console.WriteLine(sortedWinners.Count);
            Console.WriteLine(Crew.Count);

            return sortedWinners;
        }

        public void initReset()
        {
            Name = " ";
            Crew.Clear();
        }
    }
}
