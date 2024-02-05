namespace AlienVPredator.src.models
{
    public abstract class Item : MapObject
    {
        //Class Attributes
        public enum ItemTypes {
            Buff,
            Debuff
        }
        public ItemTypes Type;

        public Item(int Row, int Column, ItemTypes Type)
        : base(Row, Column)
        {
            this.Type = Type;
        }

        //Abstract method ApplyEffect
        public abstract void ApplyEffect(Character Entity);
    }
    public class HealthItem : Item
    {
        public HealthItem(int Row, int Column, ItemTypes Type)
        : base(Row, Column, Type)
        {}
        //Override of ApplyEffect for HealthItems
        //Applies effect in function of item type
        public override void ApplyEffect(Character Entity)
        {
            switch (Type)
            {
                case ItemTypes.Buff:
                    Entity.Health += 5;
                    break;
                case ItemTypes.Debuff:
                    Entity.Health -= 5;
                    break;
            }
        }
    }
    public class StrengthItem : Item
    {
        public StrengthItem(int Row, int Column, ItemTypes Type)
        : base(Row, Column, Type)
        {}
        //Override of ApplyEffect for StrengthItems
        //Applies effect in function of item type
        public override void ApplyEffect(Character Entity)
        {
            switch (Type)
            {
                case ItemTypes.Buff:
                    Entity.Strength += 5;
                    break;
                case ItemTypes.Debuff:
                    Entity.Strength -= 5;
                    break;
            }
        }
    }
}