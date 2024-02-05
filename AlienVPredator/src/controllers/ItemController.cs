using AlienVPredator.src.models;

namespace AlienVPredator.src.controllers
{
    public class ItemController<T> where T : Item
    {
        //Controller attribute ItemService (all game items list)
        public List<Item> ItemService;
        //Object constructor
        public ItemController()
        {
            ItemService = new List<Item> {};
        }
        //generic Create item
        public void Create(T Entity)
        {
            ItemService.Add(Entity);
        }
        //generic Delete item
        public void Delete(T Entity)
        {
            ItemService.Remove(Entity);
        }
        //Create random item
        public void CreateRandomItem(int Row, int Column)
        {
            Item Entity;
            Random rnd = new Random();

            Item.ItemTypes Type = (Item.ItemTypes)rnd.Next(0, 2);

            int RandomItemAttribute = rnd.Next(0, 2);
            if(RandomItemAttribute == 0)
            {
                Entity = new HealthItem(Row, Column, Type);
            }
            else
            {
                Entity = new StrengthItem(Row, Column, Type);
            }
            Create((T)Entity);
        }
    }
}