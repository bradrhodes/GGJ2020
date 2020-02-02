using UniRx;

public class TilesAggregate
{
    public Subject<TileEvent> _events = new Subject<TileEvent>();
    private bool[,] _occupiedTiles = new bool[0,0];

    public TilesAggregate()
    {
        
    }

    public void Initialize(int xDimension, int yDimension)
    {
        _occupiedTiles = new bool[xDimension, yDimension];
        var outArray = new bool[xDimension, yDimension];
        for (var x = 0; x < xDimension; x++)
        for (var y = 0; y < yDimension; y++)
        {
            _occupiedTiles[x, y] = false;
            outArray[x, y] = false;
        }

        Emit(new TileEvent.TilesInitialized(outArray));
    }

    public void SetTileAsOccupied(MapCoordinate coordinate)
    {
        _occupiedTiles[coordinate.X, coordinate.Y] = true;
        Emit(new TileEvent.TileOccupied(coordinate));
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
}