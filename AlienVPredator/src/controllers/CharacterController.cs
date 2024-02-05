using AlienVPredator.src.models;

namespace AlienVPredator.src.controllers
{
    public class CharacterController<T> where T : Character
    {
        public CharacterController()
        {}
        //Executes Move for a given character and position
        public void MoveCharacter(T Entity, MapObject Target)
        {
            Entity.Move(Target);
            if(Target is Character)
            {
                Character Loser = Entity.Challenge((Character)Target);
                if(Loser is not null)
                {
                    Loser.KnockBack();
                }
                else
                {
                    Entity.KnockBack();
                }
            }
        }
        //Executes UseHability for a given character
        public MapObject TriggerHability(T Entity, MapObject[] row = null)
        {
            return Entity.UseHability(row);
        }
        //Executes GetAvailableMovements for a given character
        public List<MapObject> GetAvailableMovements(T Entity, List<MapObject> AdjacentPositions)
        {
            return Entity.GetAvailableMovements(AdjacentPositions);
        }
    }

    public class BotController : CharacterController<Character>
    {
        public BotController() : base()
        {}

        public MapObject GeneratePlay(Character Bot, List<MapObject> AdjacentPositions, MapObject[] row = null)
        {
            Random rnd = new Random();
            int Play = rnd.Next(0, 2);
            if(Play == 0)
            {
                if(row is not null)
                {
                    TriggerHability(Bot, row);
                }
                else
                {
                    return TriggerHability(Bot);
                }
                return null;
            }
            else
            {
                List<MapObject> AvailableMovements = GetAvailableMovements(Bot, AdjacentPositions);
                int _ = rnd.Next(0, AvailableMovements.Count);
                MapObject Target = AvailableMovements[_];
                MoveCharacter(Bot, Target);
                return null;
            }
        }
    }
}