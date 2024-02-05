namespace AlienVPredator.src.models
{
    public abstract class Character : MapObject
    {
        //Class Attributes
        public int Health;
        public int Strength;
        public int PreviousRow;
        public int PreviousColumn;
        //Class constructor
        public Character(int Row, int Column, int Health = 15, int Strength = 15) : base(Row, Column)
        {
            this.Health = Health;
            this.Strength = Strength;
            PreviousColumn = Column;
            PreviousRow = Row;
        }

        //Non-Abstrat methods
        //Move: Updates character position and returns previous position, applies faceoff and item consumption if required
        public void Move(MapObject Target)
        {
            PreviousColumn = Column;
            PreviousRow = Row;

            Column = Target.Column;
            Row = Target.Row;

            if(Target is Item)
            {
                ConsumeItem((Item)Target);
            }
        }
        //Returns character to its previous position
        public void KnockBack()
        {
            Column = PreviousColumn;
            Row = PreviousRow;
        }
        //ConsumeItem calls item.ApplyEffect for this character
        public void ConsumeItem(Item item)
        {
            item.ApplyEffect(this);
        }
        //Attack: reduces enemy health in function of the difference in strength
        public void Attack(Character character)
        {
            if(Strength > character.Strength)
            {
                int Strength_diff = Strength - character.Strength;
                character.Health -= Strength_diff;
            }
        }
        //Challenge: When characters clash, decides which character inflicts damage
        public Character Challenge(Character character)
        {   
            if(Strength != character.Strength)
            {
                Character strongest = Strength > character.Strength? this : character;
                Character weakest = strongest == this? character : this;
                strongest.Attack(weakest);
                return weakest;
            }
            return null;
        }

        //Abstract methods UseHability and GetAvailableMovements
        public abstract MapObject UseHability(MapObject[] Row);
        //public abstract List<MapObject> GetAvailableMovements(List<MapObject> AdjacentPositions);
        public List<MapObject> GetAvailableMovements(List<MapObject> AdjacentPositions)
        {
            return AdjacentPositions.FindAll(x => x is not Blockade);
        }
    }

    public class Alien : Character
    {
        public Alien(int Row, int Column, int Health = 15, int Strength = 15)
        : base(Row, Column, Health, Strength)
        {}

        // defines behaviour of alien specific UseHability (place blockade)
        public override MapObject UseHability(MapObject[] _ = null)
        {
            Console.WriteLine("Alien placed a blockade");
            return new Blockade(Row, Column);
        }

        //Defines behaviour of alien specific GetAvailableMovements
        //(returns all adjacent positions since there are no mobility constraints for Alien objects)
        /*public override List<MapObject> GetAvailableMovements(List<MapObject> AdjacentPositions)
        {
            return AdjacentPositions;
        }*/
    }

    public class Predator : Character
    {
        public Predator(int Row, int Column, int Health = 15, int Strength = 15)
        : base(Row, Column, Health, Strength)
        {}
        //Defines behaviour of predator specific UseHability (row attack)
        public override MapObject UseHability(MapObject[] Row)
        {
            Console.WriteLine("Predator attacked a row");
            Alien Enemy = (Alien)Array.Find(Row, character => character is Alien);
            if(Enemy is not null)
            {
                List<Blockade> blockades = new List<Blockade>{};
                foreach (var item in Row)
                {
                    if(item.GetType() == typeof(Blockade))
                    {
                        Blockade _ = (Blockade)item;
                        blockades.Add(_);
                    }
                }
                if(blockades.Count > 0)
                {
                    foreach (var block in blockades)
                    {
                        if((block.Column > Enemy.Column && block.Column < Column) || (block.Column < Enemy.Column && block.Column > Column))
                        {
                            //break;
                        }
                        else
                        {
                            Attack(Enemy);
                            break;
                        }
                    }
                }
            }
            return null;
        }  
        //Defines behaviour of predator specific GetAvailableMovements applying blockage logic
        /*public override List<MapObject> GetAvailableMovements(List<MapObject> AdjacentPositions)
        {
            return AdjacentPositions.FindAll(x => x is not Blockade);
        }*/
    }
}