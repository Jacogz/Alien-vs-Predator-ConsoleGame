using AlienVPredator.src.models;
namespace avpGame.src.controllers
{
    public class MenuHandler
    {
        public MenuHandler(){}
        public int[] GetStartupData()
        {
            Console.WriteLine("Input map Width: ");
            string Width = Console.ReadLine();
            Console.WriteLine("Input map Height: ");
            string Height = Console.ReadLine();
            Console.WriteLine("Choose your character: (1)Alien      (2)Predator");
            string Player = Console.ReadLine();

            return new int[3] { int.Parse(Width), int.Parse(Height), int.Parse(Player)};
        }

        public MapObject GetTurnData(List<MapObject> AvailableMovements)
        {
            
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("(1) Use special hability     (2) Move");
            string instruction = Console.ReadLine();
            if(instruction == "1")
            {
                return null;
            }
            else
            {
                Console.WriteLine("These are the available places to move");
                for(int i = 0; i < AvailableMovements.Count; i++)
                {

                    if(AvailableMovements[i] is not Empty)
                    {
                        if(AvailableMovements[i] is Character)
                        {
                            Console.WriteLine(i + ") Row: " + AvailableMovements[i].Row + ", Column: " + AvailableMovements[i].Column + " || Enemy");
                        }
                        else
                        {
                            Console.WriteLine(i + ") Row: " + AvailableMovements[i].Row + ", Column: " + AvailableMovements[i].Column + " || Item");
                        }
                    }
                    else
                    {
                        Console.WriteLine(i + ") Row: " + AvailableMovements[i].Row + ", Column: " + AvailableMovements[i].Column + " || Nothing");
                    }
                }
                Console.WriteLine("Enter option id :");
                string _ = Console.ReadLine();
                int TargetMovement = int.Parse(_);
                return AvailableMovements[TargetMovement];
            }
        }
    }
}