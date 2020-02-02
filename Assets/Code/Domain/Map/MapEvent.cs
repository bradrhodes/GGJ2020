public abstract class MapEvent
{
    public class Initialized : MapEvent
    {
        public MapCell[,] MapCells { get; }
        public MapCoordinate StartCell { get; }
        public MapCoordinate GoalCell { get; }

        public Initialized(MapCell[,] mapCells, MapCoordinate startCell, MapCoordinate goalCell)
        {
            MapCells = mapCells;
            StartCell = startCell;
            GoalCell = goalCell;
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