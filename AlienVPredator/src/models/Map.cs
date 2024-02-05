namespace AlienVPredator.src.models
{
    public class Map
    {
        //Class Attributes
        private int Columns;
        private int Rows;
        public MapObject[,] Matrix;

        public Map(int Rows, int Columns)
        {
            this.Columns = Columns;
            this.Rows = Rows;
            Matrix = new MapObject[Rows, Columns];
            //Initialization of Empty objects in every Matrix position
            for(int i = 0; i< Rows; i++)
            {
                for(int j = 0; j < Columns; j++)
                {
                    Matrix[i, j] = new Empty(i, j);
                }
            }
        }
        //Method GetAdjacentPositions, returns objects adjacent to a given object
        public List<MapObject> GetAdjacentPositions(MapObject Entity)
        {
            List<MapObject> AdjacentPositions = new List<MapObject>{};
            if(Entity.Column + 1 < Columns)
            {
                AdjacentPositions.Add(Matrix[Entity.Row, Entity.Column + 1]);
            }
            if(Entity.Column - 1 >= 0)
            {
                AdjacentPositions.Add(Matrix[Entity.Row, Entity.Column - 1]);
            }
            if(Entity.Row + 1 < Rows)
            {
                AdjacentPositions.Add(Matrix[Entity.Row + 1, Entity.Column]);
            }
            if(Entity.Row - 1 >= 0)
            {
                AdjacentPositions.Add(Matrix[Entity.Row - 1, Entity.Column]);
            }
            return AdjacentPositions;
        }
        //GetRow Returns an array containing all game objects in a given row
        public MapObject[] GetRow(int row)
        {
            MapObject[] Row = new MapObject[Rows];
            for(int i = 0; i < Columns; i++)
            {
                Row[i] = Matrix[row, i];
            }
            return Row;
        }
        //Updates game object positions
        public void UpdatePosition(Character Obj)
        {
            if(Matrix[Obj.PreviousRow, Obj.PreviousColumn] is not Blockade)
            {
                Matrix[Obj.PreviousRow, Obj.PreviousColumn] = new Empty(Obj.PreviousRow, Obj.PreviousColumn);
            }
            Matrix[Obj.Row, Obj.Column] = Obj;
        }

        //Insert game object
        public void InsertObject(MapObject Entity)
        {
            Matrix[Entity.Row, Entity.Column] = Entity;
        }

        //Returns the content of a given position
        public MapObject GetContent(int row, int column)
        {
            return Matrix[row, column];
        }

        //Displays map through console
        public void DisplayMatrix()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Console.Write(" |");
                    if (Matrix[i, j] is not null)
                    {
                        if(Matrix[i, j] is Item)
                        {
                            Console.Write("o");
                        }
                        else if(Matrix[i, j] is Alien)
                        {
                            Console.Write("A");
                        }
                        else if(Matrix[i, j] is Predator)
                        {
                            Console.Write("P");
                        }
                        else if(Matrix[i, j] is Blockade)
                        {
                            Console.Write("#");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }
    }
}