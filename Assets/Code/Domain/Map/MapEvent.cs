public abstract class MapEvent
{
    public class Initialized : MapEvent
    {
        public Initialized(MapCell[,] mapCells)
        {

        }
    }

    public class MapCellStateChanged : MapEvent
    {
        public MapCellStateChanged(MapCell mapCell)
        {
        }
    }
}