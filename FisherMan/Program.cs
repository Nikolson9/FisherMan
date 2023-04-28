using FisherMan;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks.Sources;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

public enum Weather
{
    Stormy,
    Rainy,
    Cloudy,
    Sunny
}



class Program
{
    public static double GetPseudoDoubleWithinRange(double lowerBound, double upperBound)
    {
        var random = new Random();
        var rDouble = random.NextDouble();
        var rRangeDouble = rDouble * (upperBound - lowerBound) + lowerBound;
        return rRangeDouble;
    }

    public static Weather randomWeather()
    {
        Random rnd = new Random();
        Type type = typeof(Weather);
        Array values = type.GetEnumValues();
        int index = rnd.Next(values.Length);
        return (Weather)values.GetValue(index);

    }

    public static List<string> readFishFromFile(string value)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath, value);
        List<string> list = new List<string>();
        string line;
        if (File.Exists(filePath))
        {
            using(StreamReader sr = new StreamReader(filePath)) 
            {
                while((line = sr.ReadLine()) != null) 
                {
                    list.Add(line);
                }
            }
        }
        else
        {
            Console.WriteLine("Text document Fish.txt does not exist in your Desktop.");
        }

        return list.ToList();
    }

    public static void addWinnersToTheFile(List<Fisherman> fishingWinners)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath, "Winners.txt");
        
        using(StreamWriter sw = new StreamWriter(filePath)) 
        {
            sw.WriteLine("FISHING CONTEST WINNER!");
            sw.WriteLine("Winner is " + fishingWinners.Last().Name + " with score " + fishingWinners.Last().fishingScore);
            foreach (Fisherman f in fishingWinners)
            {
                if (fishingWinners.Count == 1)
                {
                    sw.WriteLine("Winner is " + fishingWinners.Last().Name + " with score " + fishingWinners.Last().fishingScore);
                }
                else
                {
                    if (fishingWinners.Last() == fishingWinners[fishingWinners.Count - 1]
                        && fishingWinners.Last().Name != fishingWinners[fishingWinners.Count - 1].Name)
                    {
                        sw.WriteLine("Winners are "+ fishingWinners.Last().Name + ", " +
                            fishingWinners[fishingWinners.Count - 1].Name + " with score " 
                            + fishingWinners.Last().fishingScore + 
                            ", " + fishingWinners[fishingWinners.Count - 1].fishingScore);
                    }
                    else if (fishingWinners.Count > 1 && fishingWinners.Last() == fishingWinners[fishingWinners.Count - 1]
                             && fishingWinners.Last() == fishingWinners[fishingWinners.Count - 2])
                    {
                        sw.WriteLine("Winners are {0},{1},{2}", fishingWinners.Last().Name,
                                                                     fishingWinners[fishingWinners.Count - 1].Name,
                                                                     fishingWinners[fishingWinners.Count - 2].Name);
                    }
                }
            }
                

         

        }

            
        
    }

    public static void appendWinnersToFile(List<Fisherman> fishermen)
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath, "Winners.txt");

        using (StreamWriter sw = File.AppendText(filePath))
        {
            sw.WriteLine("NETTING CONTEST WINNER!");
            sw.WriteLine("Winner is " + fishermen.Last().Name + " with score " + fishermen.Last().nettingScore);
            foreach (Fisherman f in fishermen)
            {
                if (fishermen.Count == 1)
                {
                    sw.WriteLine("Winner is " + fishermen.Last().Name + " with score " + fishermen.Last().nettingScore);
                }
                else
                {
                    if (fishermen.Last() == fishermen[fishermen.Count - 1]
                        && fishermen.Last().Name != fishermen[fishermen.Count - 1].Name)
                    {
                        sw.WriteLine("Winners are " + fishermen.Last().Name + ", " +
                            fishermen[fishermen.Count - 1].Name + " with score "
                            + fishermen.Last().nettingScore +
                            ", " + fishermen[fishermen.Count - 1].nettingScore);
                    }
                    else if (fishermen.Count > 1 && fishermen.Last() == fishermen[fishermen.Count - 1]
                             && fishermen.Last() == fishermen[fishermen.Count - 2])
                    {
                        sw.WriteLine("Winners are {0},{1},{2}", fishermen.Last().Name,
                                                                     fishermen[fishermen.Count - 1].Name,
                                                                     fishermen[fishermen.Count - 2].Name);
                    }
                }
            }
        }
    }
    public static double getFishDifficulty(Weather value, double diff)
    {
        double newDiff = 0;
        if (value == Weather.Stormy)
        {
            newDiff = diff + (diff / 2);
            if (newDiff > 5.5)
            {
                newDiff = 5.5;
            }

        }
        else if (value == Weather.Rainy)
        {
            newDiff = diff + (diff + diff / 0.15);
            if (newDiff > 5.5)
            {
                newDiff = 5.5;
            }
        }
        else if (value == Weather.Cloudy)
        {
            newDiff = diff - (diff + diff / 0.25);
            if (newDiff < 0.5)
            {
                newDiff = 0.5;  
            }
        }else if (value == Weather.Sunny)
        {
            newDiff = diff;
        }

        return newDiff;
    }

    public static void fillBoat(Boat b, List<string> fishermen)
    {
       
        for (int i = 0; i< fishermen.Count; i++) 
        {
            b.addFisherman(new Fisherman(fishermen[i]));
        }

    }
    private static void Main(string[] args)
    {
        Random rnd = new Random();
        int random = 0;
        Weather value = 0;
        double fishWeight = 0, fishDiff = 0 , newFishDiff = 0;
        
        List<Fish> waters = new List<Fish>();
        List<string> fish = new List<string>();
        List<string> fishermen = new List<string>();

        Boat b1 = new Boat("Galini");
        List<Fisherman> fishingWinners = new List<Fisherman>();
        List<Fisherman> nettingWinners = new List<Fisherman>();

        string filePath = @"C:\Users\kosta\OneDrive\Υπολογιστής\Winners";

        fillBoat(b1, fishermen);


       

        fish = readFishFromFile("Fish.txt");
        fishermen = readFishFromFile("Fishermen.txt");
        Console.WriteLine(fish[1]);
        for (int i = 0; i < 1000; i++)
        {

            random = rnd.Next(0, 9);
            fishWeight = GetPseudoDoubleWithinRange(0.1, 50.0);
            fishDiff = GetPseudoDoubleWithinRange(0.5, 5.5);
            value = randomWeather();

            newFishDiff = getFishDifficulty(value, fishDiff);
            waters.Add(new Fish(fish[random], fishWeight, newFishDiff));
        }

        // print all fishes
        
        for(int i = 0; i < waters.Count; i++)
        {
            Console.WriteLine();
            waters[i].print();
        }


        //Filling the boad Galini.


        fillBoat(b1, fishermen);
        Console.WriteLine();
        Console.WriteLine("*******FISHING CONTEST IS GETTING STARTED.*******");
        Console.WriteLine();
        Console.WriteLine();
        do
        {
            Fisherman f = b1.Crew.Dequeue();
            Console.WriteLine(f.Name);     
            Console.WriteLine("-------------------------------");
            f.fishing(waters[rnd.Next(0, waters.Count)]);
            f.printBasket();
            Console.WriteLine($"{f.Name} has {f.fishingScore} points.");
            Console.WriteLine("-------------------------------");
            if(f.fishingScore > 0)
            {
                fishingWinners.Add(f);  
            }
            fishingWinners = fishingWinners.OrderBy(x => x.fishingScore).ToList();
           

            
            Console.WriteLine();
            if (b1.Crew.Count == 0 && fishingWinners.Count != 0) // Crew == 0 means that the fishing contest ended.
            {
                Console.WriteLine();
                Console.WriteLine("*******FISHING CONTEST ENDED.*******");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Winner is " + fishingWinners.Last().Name + " with score " + fishingWinners.Last().fishingScore);
                if (fishingWinners.Last() == fishingWinners[fishingWinners.Count - 1] 
                     && fishingWinners.Last().Name != fishingWinners[fishingWinners.Count - 1].Name)
                {
                    Console.WriteLine("Winners are {0}, {1}", fishingWinners.Last().Name, 
                                                    fishingWinners[fishingWinners.Count - 1].Name);
                }
                else if (fishingWinners.Count > 1 && fishingWinners.Last() == fishingWinners[fishingWinners.Count - 1] 
                         && fishingWinners.Last() == fishingWinners[fishingWinners.Count - 2])
                {
                    Console.WriteLine("Winners are {0},{1},{2}", fishingWinners.Last().Name,
                                                                 fishingWinners[fishingWinners.Count - 1].Name,
                                                                 fishingWinners[fishingWinners.Count - 2].Name);
                }

            }  
            
            
        } while (b1.Crew.Count != 0);

        addWinnersToTheFile(fishingWinners);
        Console.WriteLine();
        Console.WriteLine("------------------------------");
        fillBoat(b1, fishermen);
        

        int y = 0;
        Console.WriteLine($"Weather is {value} today.");
        Console.WriteLine();
        Console.WriteLine("*******NETTING CONTEST IS GETTING STARTED.*******");
        Console.WriteLine();
        Console.WriteLine();
        do
        {
            

            Fisherman f = b1.Crew.Dequeue();
            f.netting(waters);
            f.printNet();
            Console.WriteLine($"{f.Name} has {f.nettingScore} points.");
            Console.WriteLine("-------------------------------");
            if(f.nettingScore > 0)
            {
                nettingWinners.Add(f);
            }
            nettingWinners = nettingWinners.OrderBy(x => x.nettingScore).ToList();


            if (b1.Crew.Count == 0 && fishingWinners.Count != 0)
            {
                Console.WriteLine();
                Console.WriteLine("*******NETTING CONTEST ENDED.*******");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Winner is " + nettingWinners.Last().Name + " with score " + nettingWinners.Last().fishingScore);
                if (nettingWinners.Last() == nettingWinners[nettingWinners.Count - 1]
                     && nettingWinners.Last().Name != nettingWinners[nettingWinners.Count - 1].Name)
                {
                    Console.WriteLine("Winners are {0}, {1}", nettingWinners.Last().Name,
                                                    nettingWinners[nettingWinners.Count - 1].Name);
                }
                else if (nettingWinners.Count > 1 && nettingWinners.Last() == nettingWinners[nettingWinners.Count - 1]
                         && nettingWinners.Last() == nettingWinners[nettingWinners.Count - 2])
                {
                    Console.WriteLine("Winners are {0},{1},{2}", nettingWinners.Last().Name,
                                                                 nettingWinners[nettingWinners.Count - 1].Name,
                                                                 nettingWinners[nettingWinners.Count - 2].Name);
                }
            }

            y++;
        } while (b1.Crew.Count != 0 && y < 15);

        appendWinnersToFile(nettingWinners);

        
       
            
        
    }
}