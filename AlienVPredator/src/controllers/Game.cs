using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using AlienVPredator.src.models;
using avpGame.src.controllers;
namespace AlienVPredator.src.controllers
{
    public class Game
    {
        //Game runtime objects and controllers
        private Map map;
        private Character player, bot;
        private CharacterController<Character> characterController;
        private BotController botController;
        private ItemController<Item> itemController;
        private MenuHandler menu;
        private int[] StartupData;

        Random rnd = new Random();

        public Game()
        {
            //Initialize game object controllers
            characterController = new CharacterController<Character>();
            botController = new BotController();
            itemController = new ItemController<Item>();

            //Menu declaration and request for startup data
            menu = new MenuHandler();
            StartupData = menu.GetStartupData();

            //Initialize map
            map = new Map(StartupData[0], StartupData[1]);

            //Initialize characters
            player = StartupData[2] == 1? new Alien(0, 0) : new Predator(0, 0);
            bot = StartupData[2] == 2? new Alien(StartupData[0] - 1, StartupData[1] - 1) : new Predator(StartupData[0] - 1, StartupData[1] - 1);
        }
        public void InitializeGameObjects()
        {
            map.InsertObject(player);
            map.InsertObject(bot);

            int ItemQuantity = (StartupData[0] * StartupData[1]) / 2;
            for(int i = 0; i <= ItemQuantity; i++)
            {
                while(true)
                {
                    int RandomRow = rnd.Next(0, StartupData[0]);
                    int RandomColumn = rnd.Next(0, StartupData[1]);

                    if(map.GetContent(RandomRow, RandomColumn) is Empty)
                    {
                        itemController.CreateRandomItem(RandomRow, RandomColumn);
                        map.InsertObject(itemController.ItemService[i]);
                        break;
                    }
                }
            }
        }
        public void HandleTurn()
        {
            Console.WriteLine("Player status: H(" + player.Health + ") || S(" + player.Strength +")");
            Console.WriteLine("Bot status: H(" + bot.Health + ") || S(" + bot.Strength +")");

            map.DisplayMatrix();

            List<MapObject> AvailableMovements = characterController.GetAvailableMovements(player, map.GetAdjacentPositions(player));
            MapObject turn = menu.GetTurnData(AvailableMovements);
            if(turn is not null)
            {
                characterController.MoveCharacter(player, turn);
                if(turn is Character)
                {
                    map.UpdatePosition(bot);
                }
                if(turn is Item)
                {
                    itemController.Delete((Item)turn);
                }
                map.UpdatePosition(player);
            }
            else
            {
                if(player is Alien)
                {
                    MapObject blockade = characterController.TriggerHability(player);
                    map.InsertObject(blockade);
                }
                else if(player is Predator)
                {
                    characterController.TriggerHability(player, map.GetRow(player.Row));
                }
            }
            if(bot is Alien)
            {
                MapObject botBlockade = botController.GeneratePlay(bot, map.GetAdjacentPositions(bot));
                if(botBlockade is not null)
                {
                    map.InsertObject(botBlockade);
                }
                else
                {
                    map.UpdatePosition(bot);
                }
                map.UpdatePosition(player);
            }
            else if(bot is Predator)
            {
                botController.GeneratePlay(bot, map.GetAdjacentPositions(bot), map.GetRow(bot.Row));
                map.UpdatePosition(bot);
                map.UpdatePosition(player);
            }
            Console.WriteLine();
        }
        public void Run()
        {
            InitializeGameObjects();

            while(true)
            {
                HandleTurn();
                if(player.Health <= 0 || bot.Health <= 0)
                {
                    Console.WriteLine("Game Over");
                    Console.WriteLine("Player status: H(" + player.Health + ") || S(" + player.Strength +")");
                    Console.WriteLine("Bot status: H(" + bot.Health + ") || S(" + bot.Strength +")");
                    map.DisplayMatrix();
                    break;
                }
            }
        }
    }
}