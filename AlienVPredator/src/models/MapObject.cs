namespace AlienVPredator.src.models
{
    public class MapObject
    {
        public int Column;
        public int Row;
        public MapObject(int Row, int Column)
        {
            this.Column = Column;
            this.Row = Row;
        }
    }
    //Empty represents a map space that doesnt contain any objects, used as reference for empty positions
    public class Empty : MapObject
    {
        public Empty(int Row, int Column): base(Row, Column)
        {}
    }
    //Blockade represents a map space that is blocked by an alien, used as reference for blocked positions
    public class Blockade : MapObject
    {
        public Blockade(int Row, int Column): base(Row, Column)
        {}
    }
}