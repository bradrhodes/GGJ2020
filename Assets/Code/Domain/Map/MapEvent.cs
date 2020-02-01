public abstract class MapEvent
{
    public class Initialized : MapEvent
    {
        public MapCell[,] MapCells { get; }

        public Initialized(MapCell[,] mapCells)
        {
            MapCells = mapCells;
        }
    }

    public class MapCellStateChanged : MapEvent
    {
        public MapCellStateChanged(MapCell mapCell)
        {
        }
    }

    public class MapCellClicked : MapEvent
    {
        public MapCoordinate Coordinate { get; }

        public MapCellClicked(MapCoordinate coordinate)
        {
            Coordinate = coordinate;
        }
    }
}