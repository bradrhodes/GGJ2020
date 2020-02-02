using System.Linq;
using UniRx;
using RoyT.AStar;

public class TilesAggregate
{
    public Subject<TileEvent> _events = new Subject<TileEvent>();
    private RoyT.AStar.Grid _grid = new Grid(0, 0);

    public void Initialize(int xDimension, int yDimension)
    {
        _grid = new Grid(xDimension, yDimension);
    }

    public void SetTileAsOccupied(MapCoordinate coordinate)
    {
        _grid.BlockCell(coordinate.ToPosition());
        Emit(new TileEvent.TileOccupied(coordinate));
    }

    public void FindPath(MapCoordinate currentLocation, MapCoordinate goalLocation)
    {
        var path = _grid.GetPath(currentLocation.ToPosition(), goalLocation.ToPosition(), MovementPatterns.LateralOnly)
            .Select(position => position.ToMapCoordinate()).ToArray();

        Emit(new TileEvent.PathCalculated(path));
    }

    private void Emit(TileEvent @event)
		=> _events.OnNext(@event);
}


public abstract class TileEvent
{
    public class TilesInitialized : TileEvent
    {
        public bool[,] OccupiedTiles { get; }

        public TilesInitialized(bool[,] occupiedTiles)
        {
            OccupiedTiles = occupiedTiles;
        }
    }

    public class TileOccupied : TileEvent
    {
        public MapCoordinate Coordinate { get; }

        public TileOccupied(MapCoordinate coordinate)
        {
            Coordinate = coordinate;
        }
    }

    public class PathCalculated : TileEvent
    {
        private readonly MapCoordinate[] _path;

        public PathCalculated(MapCoordinate[] path)
        {
            _path = path;
        }
    }

}

public static class TileExtensions
{
    public static Position ToPosition(this MapCoordinate source)
    {
        return new Position(source.X, source.Y);
    }

    public static MapCoordinate ToMapCoordinate(this Position source)
    {
        return new MapCoordinate(source.X, source.Y);
    }
}